using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Sound music;
    [SerializeField] private Checkpoint[] checkPoints;
    [SerializeField] private Transform startPosition;
    private int respawnPoint = -1;
    private bool startOfLevel = true;

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
        if (startOfLevel)
        {
            startOfLevel = false;
            Player.Instance.SetRespawnPoint(new Vector2(startPosition.position.x,startPosition.position.y), true);
            Player.Instance.Respawn();
        }
        else
        {
            if (respawnPoint > -1)
            {
                Debug.Log("checkPoints.Length " + checkPoints.Length);
                Debug.Log("respawnPoint " + respawnPoint);
                bool facing = checkPoints[respawnPoint].GetFacing();
                Vector2 position = checkPoints[respawnPoint].GetPosition();

                Player.Instance.SetRespawnPoint(position, facing);
                Player.Instance.Respawn();
            }
        }
    }
}