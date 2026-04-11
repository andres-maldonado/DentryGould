using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class KnobGame : Minigame
{
    [SerializeField] List<Knob> knobs = new List<Knob>();
    [SerializeField] List<int> knobValues = new List<int>();
    [SerializeField] string answer;
    [SerializeField] string entry;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateKnobs(Knob k, int v)
    {
        entry = null;
        for (int i = 0; i < knobs.Count; i++)
        {
            if (k == knobs[i])
            {
                knobValues[i] = v;
            }
            entry += knobValues[i];
        }
        Debug.Log(entry);
        if (entry == answer)
        {
            Debug.Log("you win!");
            Complete();
        }
    }

    public override void Disable()
    {
        active = false;
    }
    public override void Enable()
    {
        active = true;
    }
    public override void ResetGame()
    {
        active = false;
        taskLight.SetCompletion(false);
    }

    public override void Randomize(Transform t)
    {
        answer = null; 
        entry = null;
        for(int i = 0; i < knobs.Count; i++)
        {
            int k = Random.Range(1, 6);
            answer += k.ToString();
        }
        thisAnswer = Instantiate(answerTemplate, t);
        thisAnswer.GetComponent<Answer>().DisplayAnswer(minigameIcon.sprite, minigameColor, answer);
        for (int i = 0; i < knobs.Count; i++)
        {
            int j = Random.Range(-120, 120);
            knobs[i].transform.localEulerAngles = new Vector3(knobs[i].transform.localEulerAngles.x, knobs[i].transform.localEulerAngles.y, j);
            float knobAns = (((float)answer.ToCharArray()[i] * 60 - 180) % 360);
            float knobPos = (j % 360);
            while (Mathf.Abs(knobPos - knobAns) < 30 || (Mathf.Abs(knobPos - knobAns) > 330 && Mathf.Abs(knobPos - knobAns) < 390))
            {
                j = Random.Range(-120, 120);
                knobs[i].transform.localEulerAngles = new Vector3(knobs[i].transform.localEulerAngles.x, knobs[i].transform.localEulerAngles.y, j);
                knobPos = (j % 360);
                Debug.Log("REGENERATED ANSWER");
            }
        }
        taskLight.SetCompletion(false);
    }
}
