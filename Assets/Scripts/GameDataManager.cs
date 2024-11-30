using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private int[] saveData = null;
    public int respawnPoint = -1;
    public int scene = 1;

    public static GameDataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        saveData = SaveSystem.LoadData();
        respawnPoint = saveData[0];
        scene = saveData[1];
}

    public void SaveGame(int _repsawnPoint, int _scene)
    {
        respawnPoint = _repsawnPoint;

        SaveData sd = new SaveData() {
            respawnPoint = respawnPoint,
            scene = _scene
        };

        SaveSystem.SaveData(sd);
    }
}
