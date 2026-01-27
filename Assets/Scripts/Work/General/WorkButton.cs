using UnityEngine;
using UnityEngine.EventSystems;

public abstract class WorkButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private bool canClick = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //blank

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //blank
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (canClick)
        {
            OnClick();
        }
    }
    public abstract void OnClick();
    public void IsActive(bool s)
    {
        canClick = s;
    }
}
