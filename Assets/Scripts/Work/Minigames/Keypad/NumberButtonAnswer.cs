using UnityEngine;
using TMPro;

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
    public override void DisplayAnswer(Sprite sprite, Color color, string answer)
    {
        Debug.Log("displaying...");
        icon.sprite = sprite;
        icon.color = color;
        answerDisplay.text = answer;
    }
}
