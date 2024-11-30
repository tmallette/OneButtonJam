using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Sound sound;

    public void OnDeselect(BaseEventData eventData) {}

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData) {}

    public void OnSelect(BaseEventData eventData)
    {
        if (sound != null)
        {
            AudioManager.Instance.PlaySFXClip(sound);
        }
    }
}