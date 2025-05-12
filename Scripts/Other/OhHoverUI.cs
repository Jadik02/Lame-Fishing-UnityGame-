using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OhHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI ItemText;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse Enter");
        ItemText.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse Exit");
        ItemText.enabled = false;
    }
    
}
