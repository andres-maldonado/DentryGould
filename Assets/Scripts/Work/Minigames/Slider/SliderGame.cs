using UnityEngine;
using UnityEngine.UI;

public class SliderGame : Minigame
{
    [Header("Slider Game Stuff")]
    public Transform vSliderCube;
    public Transform hSliderCube, dot;
    [SerializeField] Slider vSlider, hSlider;
    [SerializeField] int max, interval;
    [SerializeField] float maxAnswerDist;

    private Vector2 entryVect, answerVect;
    private string answer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDot();
        answerVect = new Vector2(-1, -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Disable()
    {
        active = false;
        vSlider.enabled = false;
        hSlider.enabled = false;
    }
    public override void Enable()
    {
        active = true;
        vSlider.enabled = true;
        hSlider.enabled = true;
    }

    public override void Randomize(Transform p)
    {
        answerVect = new Vector2((float)(Random.Range(0, max/interval + 1)) / max * interval, (float)(Random.Range(0, max/interval + 1)) / max * interval);
        UpdateDot();
        while (Vector2.Distance(answerVect, entryVect) < maxAnswerDist)
        {
            answerVect = new Vector2((float)(Random.Range(0, max / interval + 1)) / max * interval, (float)(Random.Range(0, max / interval + 1)) / max * interval);
        }
        answer = "("+answerVect.y * max + ", "+answerVect.x * max+")";
        thisAnswer = Instantiate(answerTemplate, p);
        thisAnswer.GetComponent<Answer>().DisplayAnswer(minigameIcon.sprite, panel.number, minigameColor, answer);
        taskLight.SetCompletion(false);

    }
    public override void ResetGame()
    {
        Disable();
        vSlider.enabled = true;
        hSlider.enabled = true;
    }
    public void UpdateDot()
    {
        dot.localPosition = new Vector3(vSliderCube.localPosition.x, hSliderCube.localPosition.x-hSliderCube.parent.parent.position.x-1, dot.localPosition.z);
        entryVect = new Vector2(hSlider.value, 1 - vSlider.value);
        if (Vector2.Distance(answerVect, entryVect) < maxAnswerDist && active)
        {
            Complete();
            hSlider.value = answerVect.x;
            vSlider.value = 1 - answerVect.y;
        }
    }
}
