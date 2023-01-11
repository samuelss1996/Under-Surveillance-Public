using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistMovementController : MonoBehaviour
{
    // Editor parameters
    public float rotationTime;
    public float velocityMultiplier;

    // References
    private Rigidbody rb;
    private ProtagonistController controller;

    // State
    [HideInInspector]
    public Vector3 movementVector;
    private Quaternion initial;
    private float timeRotating;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float axis = GetComponent<ProtagonistController>().axis;

        if(Mathf.Abs(axis) < 0.1)
        {
            initial = transform.rotation;
            timeRotating = 0;
        }
        else if(axis > 0.1)
        {
            transform.rotation = Quaternion.Slerp(initial, Quaternion.Euler(0, 90, 0), timeRotating / rotationTime);
        }
        else if(axis < -0.1)
        {
            transform.rotation = Quaternion.Slerp(initial, Quaternion.Euler(0, 270, 0), timeRotating / rotationTime);
        }

        timeRotating += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float axis = GetComponent<ProtagonistController>().axis;

        Vector3 velocity = rb.velocity;
        velocity.x = velocityMultiplier * axis;

        rb.velocity = velocity;
    }

    private void OnDisable()
    {
        Vector3 velocity = rb.velocity;
        velocity.x = 0;

        rb.velocity = velocity;
    }
}
