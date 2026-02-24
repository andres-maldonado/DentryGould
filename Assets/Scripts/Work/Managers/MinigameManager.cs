using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [Header("Testing")]
    public bool testingGame;
    public Minigame testGame;

    [Header("Stats")]
    public int gameCount;
    public int successes;

    [Header("Objects")]
    [SerializeField] FinalButton finalButton;
    [SerializeField] float pause;

    [SerializeField] GameObject answerSheet;
    [SerializeField] List<Transform> answers = new List<Transform>();
    [SerializeField] List<Minigame> minigames = new List<Minigame>();
    public List<int> games = new List<int>();
    private int rg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateTasks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToList(Minigame minigame)
    {
        minigames.Add(minigame);
        //Debug.Log("Added "+minigame.name); // TESTING ONLY
    }
    public void GenerateTasks()
    {
        //Debug.Log("Tasks to Generate: " + gameCount);
        games.Clear();
        foreach (Minigame m in minigames)
        {
            m.isComplete = true;
            m.Disable();
        }
        for(int i = 0; i < gameCount; i++)
        {
            rg = Random.Range(0, minigames.Count);
            while (games.Contains(rg))
            {
                rg = Random.Range(0, minigames.Count); //rg = Random Game)
            }
            games.Add(rg);
            minigames[rg].isComplete = false;
            minigames[rg].Randomize(answers[i]);
            minigames[rg].Enable();
            Debug.Log("Minigame "+rg+" Activated");
        }
        finalButton.IsActive(true);
    }
    int gamesCompleted;
    public void CheckCompletion()
    {
        gamesCompleted = 0;
        foreach (Minigame m in minigames)
        {
            if (m.isComplete)
            {
                gamesCompleted++;
            }
        }
        Debug.Log("Games Completed: " + gamesCompleted + ", Minigame Count: " + minigames.Count);
        if (gamesCompleted == minigames.Count)
        {
            Debug.Log("Final Button Activated");
            finalButton.Activate();
        }
    }
    public void WaveCompletion()
    {
        answerSheet.SetActive(false);
        finalButton.IsActive(false);
        for(int i = 0; i < gameCount; i++)
        {
            Destroy(answers[i].GetChild(0).gameObject);
        }
        successes++;
        switch (successes)
        {
            case 2:
                gameCount = 2;
                break;
            case 4:
                gameCount = 3;
                break;
            case 6:
                gameCount = 4;
                break;
        }
        finalButton.Deactivate();
        StartCoroutine(BeginNewWave());
    }
    IEnumerator BeginNewWave()
    {
        GenerateTasks();
        yield return new WaitForSeconds(pause);
        answerSheet.SetActive(true);
    }
}
