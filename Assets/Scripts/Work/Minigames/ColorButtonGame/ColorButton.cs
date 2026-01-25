using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class ColorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] InputActionAsset inputActionAsset;
    private InputAction clickAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //hitbox = GetComponent<Collider>();
        clickAction = inputActionAsset.FindActionMap("UI").FindAction("Click");
        clickAction.performed += _ => OnClick();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnClick()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("yurski");

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("yurski");

    }
    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
