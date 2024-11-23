
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private GameObject startMenu;

    public static UIManager Instance { get; private set; }

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
    }

    private void Start()
    {
        SetupListeners();
    }

    private void SetupListeners()
    {
        start.onClick.AddListener(() => SceneManager.LoadScene(1));
    }

    public void ToggleStartMenu(bool _toggle)
    {
        startMenu.SetActive(_toggle);
    }
}
