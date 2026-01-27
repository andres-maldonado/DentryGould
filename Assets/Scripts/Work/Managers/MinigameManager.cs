using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
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
        Debug.Log("Added "+minigame.name); // TESTING ONLY
    }
    public void GenerateTasks()
    {
        Debug.Log("Tasks to Generate: " + gameCount);
        games.Clear();
        foreach (Minigame m in minigames)
        {
            m.isComplete = true;
        }
        for(int i = 0; i < gameCount; i++)
        {
            rg = Random.Range(0, minigames.Count - 1);
            while (games.Contains(rg))
            {
                rg = Random.Range(0, minigames.Count - 1); //rg = Random Game)
            }
            games.Add(rg);
            minigames[rg].isComplete = false;
            minigames[rg].Randomize(answers[i]);
            Debug.Log("Minigame "+rg+" Activated");
        }
        finalButton.IsActive(true);
    }
    int gamesCompleted;
    public void CheckCompletion()
    {
        foreach (Minigame m in minigames)
        {
            if (m.isComplete)
            {
                gamesCompleted++;
            }
        }
        if (gamesCompleted == minigames.Count)
        {
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
        }
        StartCoroutine(BeginNewWave());
    }
    IEnumerator BeginNewWave()
    {
        GenerateTasks();
        yield return new WaitForSeconds(pause);
        answerSheet.SetActive(true);
    }
}
