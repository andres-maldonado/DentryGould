using UnityEngine;
using UnityEngine.EventSystems;

public class DoorButton : MonoBehaviour, IPointerDownHandler
{
    public Animator doorAnim;

    public bool canExit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canExit)
        {
            doorAnim.Play("DoorLocked", 0, 0);
        }
        if (canExit)
        {
            doorAnim.Play("DoorExit");
        }
    }
}
