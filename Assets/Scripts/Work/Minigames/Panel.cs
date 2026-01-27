using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Color color;
    public SpriteRenderer icon;
    public List<Minigame> minigames = new List<Minigame>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        icon.color = color;
        foreach (Minigame minigame in minigames)
        {
            minigame.ColorIcon(color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
