using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class KnobAnswer : Answer
{
    [SerializeField] List<Transform> knobs = new List<Transform>();
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
        char[] aChar = answer.ToCharArray();
        for (int i = 0; i < aChar.Length; i++)
        {
            int aInt = (int)aChar[i];
            knobs[i].localEulerAngles = new Vector3(knobs[i].localEulerAngles.x, knobs[i].localEulerAngles.y, 60*aInt-180);
        }
    }
}
