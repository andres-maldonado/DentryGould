using UnityEngine;

public class ClockGame : Minigame
{
    private float hour, minute;
    private Vector2 entry, answer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void UpdateValue(string hand, float value)
    {
        if (hand == "HourHand")
        {
            entry.x = value;
        }
        if (hand == "MinuteHand")
        {
            entry.y = value;
        }
        if (entry == answer)
        {
            Complete();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Disable()
    {
        active = false;
    }
    public override void Enable() 
    {
        active = true;
    }
    public override void Randomize(Transform p)
    {
        answer.x = Random.Range(1, 13);
        answer.y = Random.Range(1, 13);
        thisAnswer = Instantiate(answerTemplate, p);
        string displayAnswer = answer.x.ToString()+":"+(answer.y*5).ToString();
        thisAnswer.GetComponent<Answer>().DisplayAnswer(minigameIcon.sprite, minigameColor, displayAnswer);
        taskLight.SetCompletion(false);
    }
    public override void ResetGame()
    {

    }
}
