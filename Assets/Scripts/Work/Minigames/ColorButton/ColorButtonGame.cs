using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ColorButtonGame : Minigame
{
    [Header("Color Button Minigame Stuff")]
    [SerializeField] ColorButton red;
    [SerializeField] ColorButton yellow, green, blue;
    public List<ColorButton> colorButtons = new List<ColorButton>();
    public byte answer;
    public byte entry;
    
    public void UpdateGame(bool isOn, byte code)
    {
        if (isOn)
        {
            entry += code;
        }
        else
        {
            entry -= code;
        }
        if (entry == answer) //Here's where you win
        {
            print("you win!");
            Complete();
        }
    }
    public override void Randomize(Transform p)
    {
        answer = (byte)UnityEngine.Random.Range(1, 16);
        thisAnswer = Instantiate(answerTemplate, p);
        thisAnswer.GetComponent<Answer>().DisplayAnswer(minigameIcon.sprite, minigameColor, answer.ToString());
        colorButtons.ForEach(button => 
        { 
            button.isOn = false;
            button.UpdateLamp();
        });
        entry = 0;
        taskLight.SetCompletion(false);
    }
    public void Start()
    {
        colorButtons.Add(red);
        colorButtons.Add(yellow);
        colorButtons.Add(green);
        colorButtons.Add(blue);
        red.code = 8;
        yellow.code = 4;
        green.code = 2;
        blue.code = 1;
        entry = 0;
    }
    public override void Disable()
    {
        active = false;
        foreach (ColorButton button in colorButtons)
        {
            button.IsActive(false);
        }
    }
    public override void Enable()
    {
        active = true;
        foreach (ColorButton button in colorButtons)
        {
            button.IsActive(true);
        }
    }
}
