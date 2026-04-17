using System.Collections.Generic;
using TMPro;
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
    public Panel panel;
    void Awake()
    {
        //Debug.Log("ok we up");
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
    public abstract void ResetGame();

    public void Complete()
    {
        isComplete = true;
        taskLight.SetCompletion(true);
        thisAnswer.GetComponent<Answer>().Complete();
        Disable();
        manager.CheckCompletion();
    }
    public void ColorIcon(Color color, int number)
    {
        minigameIcon.color = color;
        minigameIcon.GetComponentInChildren<TextMeshPro>().text = number.ToString();
    }
}
