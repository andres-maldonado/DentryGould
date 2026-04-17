using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NumberButtonAnswer : Answer
{
    [SerializeField] TextMeshPro answerDisplay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void DisplayAnswer(Sprite sprite, int number, Color color, string answer)
    {
        icon.sprite = sprite;
        icon.GetComponentInChildren<TextMeshPro>().text = number.ToString();
        icon.color = color;
        answerDisplay.text = answer;
    }
}
