using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.data";

    public static void Save(ProtagonistController protagonist)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData();

        data.SetPlayerPosition(protagonist.transform.position);
        data.hasWater = protagonist.PowerCount() > 1;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Load(ProtagonistController protagonist, GameObject waterPowerUp)
    {
        if(ExistsSave())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;

            protagonist.transform.position = data.GetPlayerPosition();
            protagonist.GetComponent<Animator>().SetTrigger("Load");

            if(data.hasWater)
            {
                protagonist.AddWaterPower();
                waterPowerUp.SetActive(false);
            }

            Object.FindObjectOfType<CameraFollowPlayer>().horizontalOvertake = 1;
        }
    }

    public static bool ExistsSave()
    {
        return File.Exists(path);
    }

    public static void DeleteSave()
    {
        File.Delete(path);
    }
}
