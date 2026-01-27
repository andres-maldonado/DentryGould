using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NumberButtonGame : Minigame
{
    [Header("Keypad Game Stuff")]
    public int maxLength;
    public TextMeshPro display;
    public string answer;
    public string entry;
    public List<NumberButton> keys = new List<NumberButton>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public void AddNumber(char c)
    {
        Debug.Log("added " + c);
        entry.Append(c);
        UpdateDisplay();
        if (entry == answer)
        {
            Complete();
        }
    }
    public void Backspace()
    {
        entry.Remove(entry.Length - 1);
        UpdateDisplay();
        if (entry == answer)
        {
            Debug.Log("you did it!");
            Complete();
        }
    }
    public void UpdateDisplay()
    {
        display.text = entry;
    }

    public override void Disable()
    {
        active = false;
        foreach (NumberButton button in keys)
        {
            button.IsActive(false);
        }
    }
    public override void Enable()
    {
        active = true;
        foreach (NumberButton button in keys)
        {
            button.IsActive(true);
        }
    }
    public override void Randomize(Transform t)
    {
        int password = Random.Range(0, 10^(maxLength+1));
        answer = password.ToString();
        while (answer.Length < maxLength)
        {
            answer.Insert(0,"0");
        }
    }
}
