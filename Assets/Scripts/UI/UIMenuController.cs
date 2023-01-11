using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    // Editor parameters
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject blackOverlay;

    public ButtonController continueButton;

    public OptionsButtonController resolution;
    public OptionsButtonController fullscreen;
    public OptionsButtonController quality;

    public Slider volume;

    public ButtonController optionsApplyButton;
    public ButtonController optionsBackButton;

    public LightBlink jailLight;
    public GameObject waterPowerUp;

    // State
    private bool fromMainMenu;

    private void Start()
    {
        if(SaveSystem.ExistsSave())
        {
            continueButton.SetInteractable(true);
        }
    }

    public void NewGame()
    {
        jailLight.OnNewGame();
        mainMenu.SetActive(false);

        SaveSystem.DeleteSave();
    }

    public void Continue()
    {
        StartCoroutine(ContinueCR());
    }

    public void ShowOptions(bool _fromMainMenu)
    {
        LoadSettings();

        fromMainMenu = _fromMainMenu;

        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void HideOptions()
    {
        optionsMenu.SetActive(false);

        if (fromMainMenu)
        {
            mainMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    public void OnVolumeChanged(float vol)
    {
        if(optionsApplyButton.gameObject.activeInHierarchy)
        {
            optionsApplyButton.SetInteractable(true);
        }
    }

    public void ApplyOptions()
    {
        optionsApplyButton.SetInteractable(false);
        optionsBackButton.GetComponent<Button>().Select();

        int[] res = resolution.options[resolution.selected].Split('x').Select(i => int.Parse(i)).ToArray();
        Screen.SetResolution(res[0], res[1], fullscreen.selected == 0 ? FullScreenMode.Windowed : FullScreenMode.ExclusiveFullScreen);
        QualitySettings.SetQualityLevel(quality.selected, true);

        AudioListener.volume = volume.value;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        StartCoroutine(ExitToMainMenuCR());
    }

    private void Update()
    {
        bool paused = pauseMenu.activeInHierarchy;

        if (!IsMenuActive() && Input.GetKeyDown("escape")) {
            pauseMenu.SetActive(true);
        }

        if(paused && Input.GetKeyDown("escape"))
        {
            Resume();
        }
    }

    private void LoadSettings()
    {
        LoadResolution();
        LoadFullScreen();
        LoadQuality();

        volume.value = AudioListener.volume;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public bool IsMenuActive()
    {
        return mainMenu.activeInHierarchy || optionsMenu.activeInHierarchy || pauseMenu.activeInHierarchy || blackOverlay.activeInHierarchy;
    }

    private IEnumerator ContinueCR()
    {
        float elapsed = 0;
        mainMenu.SetActive(false);
        blackOverlay.SetActive(true);

        while(elapsed <= 1)
        {
            blackOverlay.GetComponent<CanvasGroup>().alpha = Mathf.SmoothStep(0, 1, elapsed);
            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        SaveSystem.Load(FindObjectOfType<ProtagonistController>(), waterPowerUp);
        yield return new WaitForSeconds(1.0f);

        elapsed = 0;
        while (elapsed <= 1)
        {
            blackOverlay.GetComponent<CanvasGroup>().alpha = Mathf.SmoothStep(1, 0, elapsed);
            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        blackOverlay.SetActive(false);
    }

    private void LoadResolution()
    {
        List<Resolution> resolutions = new List<Resolution>();

        Resolution r720 = new Resolution();
        Resolution r1080 = new Resolution();
        Resolution r2k = new Resolution();

        r720.width = 1280;
        r720.height = 720;
        r1080.width = 1920;
        r1080.height = 1080;
        r2k.width = 2560;
        r2k.height = 1440;

        resolutions.Add(Screen.currentResolution);
        resolutions.Add(r720);
        resolutions.Add(r1080);
        resolutions.Add(r2k);

        resolutions = resolutions.OrderBy(r => r.width).GroupBy(r => new { r.width, r.height }).Select(r => r.FirstOrDefault()).ToList();
        string[] resStr = new string[resolutions.Count];

        for(int i = 0; i < resStr.Length; i++)
        {
            resStr[i] = $"{resolutions[i].width}x{resolutions[i].height}";
        }

        resolution.options = resStr;
        resolution.selected = resolutions.IndexOf(Screen.currentResolution);
    }

    private void LoadFullScreen()
    {
        fullscreen.selected = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen ? 1 : 0;
    }

    private void LoadQuality()
    {
        quality.options = QualitySettings.names;
        quality.selected = QualitySettings.GetQualityLevel();
    }

    private IEnumerator ExitToMainMenuCR()
    {
        float elapsed = 0;
        mainMenu.SetActive(false);
        blackOverlay.SetActive(true);

        while (elapsed <= 1)
        {
            blackOverlay.GetComponent<CanvasGroup>().alpha = Mathf.SmoothStep(0, 1, elapsed);
            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
