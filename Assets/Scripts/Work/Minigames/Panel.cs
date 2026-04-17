using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Color color;
    public int number;
    public SpriteRenderer icon;
    public TextMeshPro text;
    public List<Minigame> minigames = new List<Minigame>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.color = color;
        text = icon.GetComponentInChildren<TextMeshPro>();
        text.text = number.ToString();
        foreach (Minigame minigame in minigames)
        {
            minigame.ColorIcon(color, number);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
