using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class ColorButton : WorkButton
{
    public bool isOn;
    public byte code;

    private ButtonLamp buttonLamp;
    private ColorButtonGame game;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonLamp = GetComponent<ButtonLamp>();
        game = GetComponentInParent<ColorButtonGame>();
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
        if (game.active)
        {
            isOn = !isOn;
            UpdateLamp();
            game.UpdateColorButtonGame(isOn, code);
        }
    }
}
