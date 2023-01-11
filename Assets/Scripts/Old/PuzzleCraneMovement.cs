using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCraneMovement : MonoBehaviour
{
    // Editor parameters
    public Vector2 initialCoord;
    public Vector2 gridSize;

    public Vector2 physicalCellDistance;
    public float moveTime;

    public Transform rayOrigin;
    public LayerMask rayLayerMask;

    // State
    private Vector2 currentCoord;
    private Vector3 moveSource;
    private Vector3 moveTarget;
    private Vector3? nextTarget = null;

    private PuzzleState initialState;

    private float currentTime;
   
    void Awake()
    {
        initialState = PuzzleState.FromCurrentState(gameObject.tag);
        Init();
    }

    void Init()
    {
        currentCoord = initialCoord;
        moveSource = moveTarget = transform.position;
        nextTarget = null;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(moveSource, moveTarget, Mathf.SmoothStep(0, 1, currentTime / moveTime));

        if (currentTime / moveTime >= 1)
        {
            moveSource = moveTarget;

            if(nextTarget != null)
            {
                moveTarget = (Vector3) nextTarget;
                nextTarget = null;
                currentTime = 0;
            }
        }

        currentTime += Time.deltaTime;


        // Movement
        if (Input.GetKeyDown("j"))
        {
            Move(new Vector2(-1, 0));
        }

        if (Input.GetKeyDown("k"))
        {
            Move(new Vector2(0, -1));
        }

        if (Input.GetKeyDown("l"))
        {
            Move(new Vector2(1, 0));
        }

        if (Input.GetKeyDown("i"))
        {
            Move(new Vector2(0, 1));
        }

        // Restart
        if(Input.GetKeyDown("r"))
        {
            RestartPuzzle();
        }
    }

    private void Move(Vector2 direction)
    {
        if(IsValidMove(direction))
        {
            Vector2 tempCoord = currentCoord + direction;

            if (moveSource == moveTarget)
            {
                currentCoord = tempCoord;
                moveTarget += new Vector3(physicalCellDistance.x * direction.x, 0, physicalCellDistance.y * direction.y);
                currentTime = 0;
            }
            else if(nextTarget == null && currentTime / moveTime >= 0.5)
            {
                currentCoord = tempCoord;
                nextTarget = moveTarget + new Vector3(physicalCellDistance.x * direction.x, 0, physicalCellDistance.y * direction.y);
            }
        }
    }

    private bool IsValidMove(Vector2 direction)
    {
        Vector3 direction3d = new Vector3(direction.x, 0, direction.y);
        RaycastHit[] hits = Physics.RaycastAll(new Ray(rayOrigin.position, direction3d),
            gridSize.magnitude * physicalCellDistance.magnitude, rayLayerMask);

        Vector3 targetCoord = currentCoord + direction * (hits.Length + 1);

        return InRange(targetCoord, Vector2.zero, gridSize);
    }

    private bool InRange(Vector2 vector, Vector2 limit1, Vector2 limit2)
    {
        return Mathf.Min(limit1.x, limit2.x) <= vector.x && vector.x < Mathf.Max(limit1.x, limit2.x)
            && Mathf.Min(limit1.y, limit2.y) <= vector.y && vector.y < Mathf.Max(limit1.y, limit2.y);
    }

    private void RestartPuzzle()
    {
        initialState.RestoreState(gameObject.tag);
        Init();
    }

    private class PuzzleState
    {
        private Vector3[] puzzleElementsPos;

        private PuzzleState(Vector3[] _puzzleElementsPos)
        {
            puzzleElementsPos = _puzzleElementsPos;
        }

        public static PuzzleState FromCurrentState(string puzzleElementsTag)
        {
            GameObject[] puzzleElements = GameObject.FindGameObjectsWithTag(puzzleElementsTag);
            Vector3[] puzzleElementsPos = new Vector3[puzzleElements.Length];

            for(int i = 0; i < puzzleElements.Length; i++)
            {
                puzzleElementsPos[i] = puzzleElements[i].transform.position;
            }

            return new PuzzleState(puzzleElementsPos);
        }

        public void RestoreState(string puzzleElementsTag)
        {
            GameObject[] puzzleElements = GameObject.FindGameObjectsWithTag(puzzleElementsTag);

            for (int i = 0; i < puzzleElements.Length; i++)
            {
                puzzleElements[i].transform.position = puzzleElementsPos[i];
            }
        }
    }
}