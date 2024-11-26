using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Sound music;

    private void Start()
    {
        UIManager.Instance.ToggleStartMenu(true);

        if(music != null)
        {
            AudioManager.Instance.PlayMusic(music);
        }
    }
}
