using System;
using UnityEngine;

public static class SaveSystem
{
   public static int[] LoadData()
    {
        string save = PlayerPrefs.GetString(Application.productName + "_Save");

        // Start at -1 respawn and scene 1.
        int[] saveData = new int[] { -1, 1};

        if(save != null) {
            try
            {
                SaveData sd = JsonUtility.FromJson<SaveData>(save);

                saveData[0] = sd.respawnPoint;
                saveData[1] = sd.scene;

                return saveData;
            } 
            catch
            {
               
                return saveData;
            }
        }

        return saveData;
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
    public int scene;
}
