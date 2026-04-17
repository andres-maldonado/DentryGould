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
        Disable();
    }
    public void AddNumber(char c)
    {
        if (entry == null || entry.Length < maxLength)
        {
            entry += c.ToString();
            UpdateDisplay();
        }
        if (entry == answer && active)
        {
            Complete();
        }
    }
    public void Backspace()
    {
        entry = entry.Substring(0, entry.Length-1);
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
    public override void ResetGame()
    {
        active = false;
        taskLight.SetCompletion(false);
        entry = null;
        UpdateDisplay();
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
        int password = Random.Range(0, (int)Mathf.Pow(10, maxLength));
        answer = password.ToString();
        Debug.Log("Answer Length: " + answer.Length + ", Max Length: " + maxLength);
        while (answer.Length < maxLength)
        {
            answer = "0" + answer;
        }
        Debug.Log("Answer Length: " + answer.Length + ", Max Length: " + maxLength);
        entry = null;
        UpdateDisplay();
        thisAnswer = Instantiate(answerTemplate, t);
        thisAnswer.GetComponent<Answer>().DisplayAnswer(minigameIcon.sprite, panel.number, minigameColor, answer);
        taskLight.SetCompletion(false);
    }
}
