using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Water2DTool;

public class ProtagonistDangerousPoolController : MonoBehaviour
{
    // Editor paramaters
    public GameObject sparks;
    public AudioSource sound;
    public CanvasGroup blackOverlay;

    public Transform playerRestore;

    public float fadeOutTime;
    public float fadeInTime;

    // References
    private Animator animator;

    // State
    private bool flag;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DangerousPool") && other.GetComponent<Water2D_Tool>().waterHeight > 0.8 && !flag)
        { 
            flag = true;
            StartCoroutine(ElectrocutedCR());
        }
    }

    private IEnumerator ElectrocutedCR()
    {
        animator.SetTrigger("Electrocuted");
        sparks.SetActive(true);
        sound.Play();

        blackOverlay.gameObject.SetActive(true);
        blackOverlay.alpha = 0;

        yield return new WaitForSeconds(3.0f);

        float elapsed = 0;
        while(elapsed <= fadeOutTime)
        {
            blackOverlay.alpha = Mathf.SmoothStep(0, 1, elapsed / fadeOutTime);
            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.position = playerRestore.position;
        transform.rotation = playerRestore.rotation;

        GetComponent<ProtagonistLadderClimb>().enabled = false;

        animator.SetTrigger("Restore");
        sparks.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        elapsed = 0;
        while (elapsed <= fadeInTime)
        {
            blackOverlay.alpha = Mathf.SmoothStep(1, 0, elapsed / fadeInTime);
            elapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        blackOverlay.gameObject.SetActive(false);
        flag = false;
    }
}