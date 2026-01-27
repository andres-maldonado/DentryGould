using UnityEngine;
using TMPro;

public class NumberButton : WorkButton
{
    public bool backspace;

    private NumberButtonGame game;
    private char keyNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        game = transform.parent.GetComponentInParent<NumberButtonGame>();
        keyNum = GetComponentInChildren<TextMeshPro>().text.ToCharArray()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnClick()
    {
        if (!backspace)
        {
            game.AddNumber(keyNum);
        }
        else
        {
            game.Backspace();
        }
    }
}
