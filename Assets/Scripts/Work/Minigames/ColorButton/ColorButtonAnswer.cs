using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ColorButtonAnswer : Answer
{
    [Header("Color Button Answer")]
    public List<SpriteRenderer> buttonSprites = new List<SpriteRenderer>();
    public Sprite red, yellow, green, blue;

    private int index;
    private byte answerByte;

    public void Awake()
    {

    }
    public override void DisplayAnswer(Sprite sprite, Color color, string answer)
    {
        icon.sprite = sprite;
        icon.color = color;
        index = 0;
        answerByte = (byte)int.Parse(answer);
        Debug.Log("Answer: " + answerByte);
        if (answerByte > 7)
        {
            buttonSprites[index].sprite = red;
            index++;
            Debug.Log("Added Red");
        }
        if (answerByte % 8 > 3)
        {
            buttonSprites[index].sprite = yellow;
            index++;
            Debug.Log("Added Yellow");
        }
        if (answerByte % 4 > 1)
        {
            buttonSprites[index].sprite = green;
            index++;
            Debug.Log("Added Green");
        }
        if (answerByte % 2 == 1)
        {
            buttonSprites[index].sprite = blue;
            index++;
            Debug.Log("Added Blue");
        }
    }
}
