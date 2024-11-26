using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public int respawnPoint = -1;

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

        respawnPoint = SaveSystem.LoadData();
    }

    public void SaveGame(int _repsawnPoint)
    {
        respawnPoint = _repsawnPoint;

        SaveData sd = new SaveData() {
            respawnPoint = respawnPoint
        };

        SaveSystem.SaveData(sd);
    }
}
