
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private Button newGame;
    [SerializeField] private Button continueGame;
    [SerializeField] private Button credits;
    [SerializeField] private Slider volume;

    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private Button back;


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
        SetVolumeSlider(3f);
        SetupButtons();
        SetupListeners();
    }

    private void SetupButtons()
    {
        if(GameDataManager.Instance.respawnPoint>0)
        {
            continueGame.gameObject.SetActive(true);
        }
    }

    private void SetupListeners()
    {
        newGame.onClick.AddListener(NewGame);
        continueGame.onClick.AddListener(ContinueGame);
        volume.onValueChanged.AddListener(SetVolumeSlider);
        credits.onClick.AddListener(()=>ToggleCreditsMenu(true));
        back.onClick.AddListener(()=> ToggleStartMenu(true));
    }

    public void ToggleStartMenu(bool _toggle)
    {
        ResetMenus();
        startMenu.SetActive(_toggle);
    }

    public void ToggleCreditsMenu(bool _toggle)
    {
        ResetMenus();
        creditsMenu.SetActive(_toggle);
    }

    private void ResetMenus()
    {
        startMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    private void SetVolumeSlider(float _volume)
    {
        SetSlider(_volume);
        volume.SetValueWithoutNotify(AudioManager.Instance.volume * 10f);
    }

    private void SetSlider(float _volume)
    {
        float _v = 0.01f;

        if (_volume != 0f)
        {
            _v = _volume / 10f;
        }

        AudioManager.Instance.SetMixer(_v);
    }

    private void NewGame()
    {
        GameDataManager.Instance.respawnPoint = 0;

        SaveData sd = new SaveData() {
            respawnPoint = 0,
        };

        SaveSystem.SaveData(sd);
        SceneManager.LoadScene(1);
    }

    private void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }
}
