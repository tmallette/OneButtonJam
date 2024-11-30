using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Sound music;
    [SerializeField] private Checkpoint[] checkPoints;
    private int respawnPoint = -1;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (music != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(music);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ToggleStartMenu(false);
            UIManager.Instance.ToggleRespawnButton();
        }

        if (GameDataManager.Instance != null)
        {
            respawnPoint = GameDataManager.Instance.respawnPoint;
        }

        if(respawnPoint > -1)
        {
            bool facing = checkPoints[respawnPoint].GetFacing();
            Vector2 position = checkPoints[respawnPoint].GetPosition();

            Player.Instance.SetRespawnPoint(position, facing);
            Player.Instance.Respawn();
        }
    }
}