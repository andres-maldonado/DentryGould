using TMPro;
using UnityEngine;

public class SliderAnswer : Answer
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
        icon.sprite = sprite;
        icon.color = color;
        answerDisplay.text = answer;
    }
}
