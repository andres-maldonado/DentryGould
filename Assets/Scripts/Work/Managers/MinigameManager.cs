using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MinigameManager : MonoBehaviour, ISerializationCallbackReceiver
{
    [Header("Testing")]
    public bool testingGame;
    public Minigame testGame;

    [Header("Stats")]
    public int gameCount;
    public int successes;
    public int quota;
    public float shiftTimer;

    [Header("Difficulty Ramp")]
    public List<int> completeCount = new List<int>();
    public List<int> taskCount = new List<int>();
    public Dictionary<int, int> countChange = new Dictionary<int, int>();

    [Header("Objects")]
    [SerializeField] FinalButton finalButton;
    [SerializeField] float pause;
    [SerializeField] TowerIntensity tower;

    [SerializeField] GameObject answerSheet;
    [SerializeField] List<Transform> answers = new List<Transform>();
    [SerializeField] List<Minigame> minigames = new List<Minigame>();
    public List<int> games = new List<int>();
    private int rg;
    private float counter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < completeCount.Count; i++)
        {
            countChange.Add(completeCount[i], taskCount[i]);
        }
    }

    public void BeginShift()
    {
        counter = shiftTimer;
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
        tower.UpdateIntensity(gamesCompleted);
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
        /*
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
        */
        foreach (KeyValuePair<int, int> i in countChange)
        {
            if (successes == i.Key)
            {
                gameCount = i.Value;
                break;
            }
        }
        tower.UpdateIntensity(0);
        finalButton.Deactivate();
        StartCoroutine(BeginNewWave());
    }
    IEnumerator BeginNewWave()
    {
        GenerateTasks();
        yield return new WaitForSeconds(pause);
        answerSheet.SetActive(true);
    }
    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {
        
    }
}
