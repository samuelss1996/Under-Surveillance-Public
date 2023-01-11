using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatformBehaviour : MonoBehaviour
{
    // Editor parameters
    public float[] heights;
    public int initialPos;
    public float speed;

    public AudioSource moveAudio;

    public ElevatorButtonReact downPanel;
    public ElevatorButtonReact upPanel;
    public ElevatorButtonReact[] extraDownPanels;

    // Values
    private Vector3[] positions;

    // State
    private Transform oldPlayerParent;
    private Vector3 velocity;
    private int currentPos;
    private int speedMult;


    private void Awake()
    {
        currentPos = initialPos;
        positions = new Vector3[heights.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = transform.position;
            positions[i].y = heights[i];
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, positions[currentPos], speed * speedMult * Time.deltaTime);
        UpdateTerminals();
    }

    public void MoveElevator(int steps)
    {
        int previousPos = currentPos;
        currentPos = Mathf.Clamp(currentPos + steps, 0, positions.Length - 1);

        speedMult = Mathf.Abs(currentPos - previousPos);

        if(currentPos != previousPos)
        {
            moveAudio.Play();
        }
    }

    private void UpdateTerminals()
    {
        downPanel.turnedOn = !moveAudio.isPlaying && currentPos > 0;
        upPanel.turnedOn = !moveAudio.isPlaying && currentPos < heights.Length - 1;

        foreach(ElevatorButtonReact panel in extraDownPanels)
        {
            panel.turnedOn = !moveAudio.isPlaying && currentPos > 0;
        }
    }
}
