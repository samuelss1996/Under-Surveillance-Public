using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[] playerPosition;
    public bool hasWater;

    public SaveData()
    {
        playerPosition = new float[2];
    }

    public Vector3 GetPlayerPosition()
    {
        return new Vector3(playerPosition[0], playerPosition[1]);
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        playerPosition[0] = pos.x;
        playerPosition[1] = pos.y;
    }
}
