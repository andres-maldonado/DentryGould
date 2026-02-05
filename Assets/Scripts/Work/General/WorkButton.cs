using UnityEngine;
using UnityEngine.EventSystems;

public abstract class WorkButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Animator anim;
    private bool canClick = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
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
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            anim.Play("ButtonClick", -1, 0);
            if (canClick)
            {
                OnClick();
            }
        }
    }
    public abstract void OnClick();
    public void IsActive(bool s)
    {
        canClick = s;
    }
}
