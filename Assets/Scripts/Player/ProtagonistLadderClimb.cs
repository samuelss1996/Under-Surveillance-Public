using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistLadderClimb : MonoBehaviour
{
    // Editor
    public float speed;

    // References
    private ProtagonistController controller;
    private Rigidbody rb;

    // State
    [HideInInspector]
    public bool climb;

    void Awake()
    {
        controller = GetComponent<ProtagonistController>();
        rb = GetComponent<Rigidbody>();

        enabled = false;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = climb? speed * controller.direction * controller.axis : 0;

        rb.velocity = velocity;
    }

    private void OnEnable()
    {
        GetComponent<Animator>().SetBool("OnLadder", true);
        rb.useGravity = false;
        climb = true;
    }

    private void OnDisable()
    {
        GetComponent<Animator>().SetBool("OnLadder", false);
        rb.useGravity = true;

        controller.direction = 1;
    }

    public void RepositionOnTop()
    {
        StartCoroutine(RepositionOnTopCR(0.25f));
    }

    private IEnumerator RepositionOnTopCR(float transitionTime)
    {
        Vector3 initial = transform.position;
        Vector3 target = initial + new Vector3(controller.direction * 0.8f, 1.7f, 0.0f);
        float t = 0;

        while (t <= 1)
        {
            transform.position = Vector3.Lerp(initial, target, t);
            t += Time.deltaTime / transitionTime;

            yield return new WaitForEndOfFrame();
        }

        enabled = false;
    }
}
