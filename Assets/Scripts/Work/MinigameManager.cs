using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] Dictionary<Minigame, bool> minigames = new Dictionary<Minigame, bool>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToDict(Minigame minigame)
    {
        minigames.Add(minigame, true);
        Debug.Log("Added "+minigame.name);
    }
}
