using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [Header("General Minigame Stuff")]
    [SerializeField] Sprite minigameIcon;

    private MinigameManager manager;
    void Start()
    {
        manager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
        manager.AddToDict(this);
    }

    public abstract void Randomize();

    public void Complete()
    {

    }
}
