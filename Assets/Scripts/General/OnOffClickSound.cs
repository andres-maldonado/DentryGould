using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OnOffClickSound : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] SliderGame game;
    [SerializeField] EventReference click, release;

    private bool isDragging;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (game.active && isDragging && !Mouse.current.leftButton.isPressed)
        {
            AudioManager.ins.PlayOneShot(release, this.transform.position);
            isDragging = false;
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (game.active && eventData.button == PointerEventData.InputButton.Left)
        {
            AudioManager.ins.PlayOneShot(click, this.transform.position);
            isDragging = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
