using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using TMPro;

public class ColorButton : WorkButton
{
    public bool isOn;
    public byte code;

    private ButtonLamp buttonLamp;
    private ColorButtonGame game;
    private TextMeshPro number;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        buttonLamp = GetComponent<ButtonLamp>();
        game = transform.parent.GetComponentInParent<ColorButtonGame>();
        number = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateLamp()
    {
        buttonLamp.on = isOn;
        buttonLamp.SetColor();
    }
    public override void OnClick()
    {
        isOn = !isOn;
        UpdateLamp();
        game.UpdateGame(isOn, code);
    }
}
