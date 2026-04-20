using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class ColorButtonAnswer : Answer
{
    [Header("Color Button Answer")]
    public List<SpriteRenderer> buttonSprites = new List<SpriteRenderer>();
    public Sprite red, yellow, green, blue;
    [SerializeField] GameObject cbNumber;

    private int index;
    private byte answerByte;

    public void Awake()
    {

    }
    public override void DisplayAnswer(Sprite sprite, int number, Color color, string answer)
    {
        icon.sprite = sprite;
        icon.GetComponentInChildren<TextMeshPro>().text = number.ToString();
        icon.color = color;
        index = 0;
        answerByte = (byte)int.Parse(answer);
        //Debug.Log("Answer: " + answerByte);
        if (answerByte > 7)
        {
            buttonSprites[index].sprite = red;
            AddNumber(1, buttonSprites[index].transform);
            index++;
            //Debug.Log("Added Red");
        }
        if (answerByte % 8 > 3)
        {
            buttonSprites[index].sprite = yellow;
            AddNumber(2, buttonSprites[index].transform);
            index++;
            //Debug.Log("Added Yellow");
        }
        if (answerByte % 4 > 1)
        {
            buttonSprites[index].sprite = green;
            AddNumber(3, buttonSprites[index].transform);
            index++;
            //Debug.Log("Added Green");
        }
        if (answerByte % 2 == 1)
        {
            buttonSprites[index].sprite = blue;
            AddNumber(4, buttonSprites[index].transform);
            index++;
            //Debug.Log("Added Blue");
        }
    }
    private void AddNumber(int n, Transform t)
    {
        GameObject number = Instantiate(cbNumber, t);
        number.GetComponent<TextMeshPro>().text = n.ToString();
    }
}
