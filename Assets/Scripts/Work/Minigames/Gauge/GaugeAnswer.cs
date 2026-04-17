using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GaugeAnswer : Answer
{
    [SerializeField] Transform pivot;
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
        float angle = float.Parse(answer);
        pivot.localEulerAngles = new Vector3(0, 0, angle-30);
    }
}
