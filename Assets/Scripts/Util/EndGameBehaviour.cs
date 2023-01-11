using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameBehaviour : MonoBehaviour
{
    // Editor parameters
    public CanvasGroup blackOverlay;
    public float fadeOutTime;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(EndGameCR());
        }
    }

    private IEnumerator EndGameCR()
    {
        float elapsed = 0;
        blackOverlay.gameObject.SetActive(true);

        while(elapsed <= fadeOutTime)
        {
            float opacity = Mathf.SmoothStep(0, 1, elapsed / fadeOutTime);
            blackOverlay.alpha = opacity;

            elapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
