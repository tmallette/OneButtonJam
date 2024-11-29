using UnityEngine;

public class BossChat : MonoBehaviour
{    
   public void ToggleChat (bool toggle)
    {
        gameObject.SetActive(toggle);
    }
}
