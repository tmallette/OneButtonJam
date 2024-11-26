using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public static class SaveSystem
{
   public static int LoadData()
    {
        string save = PlayerPrefs.GetString(Application.productName + "_Save");

        if(save != null) {
            try
            {
                SaveData sd = JsonUtility.FromJson<SaveData>(save);
                return sd.respawnPoint;
            } 
            catch
            {
                return 0;
            }
        }

        return 0;
    }

    public static void SaveData(SaveData _sd)
    {
        string json = JsonUtility.ToJson(_sd);
        PlayerPrefs.SetString(Application.productName + "_Save", json);
        PlayerPrefs.Save();
    }
}

[Serializable]
public class SaveData
{
    public int respawnPoint;
}
