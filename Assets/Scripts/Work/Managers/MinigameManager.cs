using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using FMODUnity;

public class MinigameManager : MonoBehaviour, ISerializationCallbackReceiver
{
    [Header("Testing")]
    public bool testingGame;
    public Minigame testGame;

    [Header("Stats")]
    public int gameCount;
    public int successes;
    public int quota;
    public float shiftTimer, taskTimer;

    [Header("Difficulty Ramp")]
    public List<int> completeCount = new List<int>();
    public List<int> taskCount = new List<int>();
    public Dictionary<int, int> countChange = new Dictionary<int, int>();

    [Header("Objects")]
    [SerializeField] FinalButton finalButton;
    [SerializeField] float pause;
    [SerializeField] TowerIntensity tower;
    [SerializeField] LoudspeakerDialogue dialogue;
    [SerializeField] WorkCamControl controls;
    [SerializeField] GameObject answerSheet;
    [SerializeField] List<Transform> answers = new List<Transform>();
    [SerializeField] List<Minigame> minigames = new List<Minigame>();
    [SerializeField] EventReference endMusic;
    public List<int> games = new List<int>();
    private int rg;
    private float counter, taskTime;
    public bool isEnding;
    private bool shiftActive, taskFailed;
    private MusicManager musicManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < completeCount.Count; i++)
        {
            countChange.Add(completeCount[i], taskCount[i]);
        }
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    public void BeginShift()
    {
        counter = shiftTimer;
        taskTime = taskTimer;
        shiftActive = true;
        controls.SetTicketEnabled(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (shiftActive)
        {
            counter -= Time.deltaTime;
            taskTime -= Time.deltaTime;
            if (counter < 0 && !isEnding)
            {
                isEnding = true;
            }
            if (taskTime < 0)
            {
                taskFailed = false;
                EndShift();
            }
        }
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
            m.ResetGame();
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
            //Debug.Log("Minigame "+rg+" Activated");
        }
        tower.UpdateIntensity(0);
        taskTime = taskTimer;
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
        tower.UpdateIntensity(gameCount + gamesCompleted - minigames.Count);
        Debug.Log("Games Completed: " + gamesCompleted);
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
            if (answers[i].childCount != 0)
            {
                Destroy(answers[i].GetChild(0).gameObject);
            }
        }
        successes++;
        foreach (KeyValuePair<int, int> i in countChange)
        {
            if (successes == i.Key)
            {
                gameCount = i.Value;
                break;
            }
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
    public void EndShift()
    {
        //shut off lights
        shiftActive = false;
        musicManager.ReplaceMusic(endMusic);
        tower.PowerDown();
        controls.SetTicketEnabled(false);
        finalButton.Deactivate();
        finalButton.enabled = false;
        if (successes >= quota && !taskFailed)
        {
            //timer to delay printing
            dialogue.SuccessDialogue(3);
            dialogue.success = 1;
        }
        else
        {
            dialogue.FailDialogue(3);
            dialogue.success = 2;
        }
    }
    public void OnBeforeSerialize()
    {

    }
    public void OnAfterDeserialize()
    {
        
    }
}
