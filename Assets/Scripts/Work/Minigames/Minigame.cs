using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [Header("General Minigame Stuff")]
    public SpriteRenderer minigameIcon;
    public Color minigameColor;
    public GameObject answerTemplate;
    public bool active;
    public bool isComplete = true;
    public GameObject thisAnswer;
    public TaskLight taskLight;

    private MinigameManager manager;
    private Panel panel;
    void Awake()
    {
        Debug.Log("ok we up");
        panel = GetComponentInParent<Panel>();
        minigameColor = panel.color;
        active = true; //TESTING
        manager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
        manager.AddToList(this);
        Enable();
    }
    
    public abstract void Randomize(Transform parent);
    public abstract void Enable();
    public abstract void Disable();

    public void Complete()
    {
        isComplete = true;
        taskLight.SetCompletion(true);
        thisAnswer.GetComponent<Answer>().Complete();
        Disable();
        manager.CheckCompletion();
    }
    public void ColorIcon(Color color)
    {
        minigameIcon.color = color;
    }
}
