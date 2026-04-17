using NUnit.Framework.Constraints;
using UnityEngine;

public class GaugeGame : Minigame
{
    [SerializeField] float moveAmount, moveSpeed, moveAccel, minAngle, maxAngle, depleteRate, holdTime, precision;
    [SerializeField] Transform pivot;

    private float targetValue, answerMin, answerMax, counter;
    private int answer;
    private bool isInRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetValue = 0;
        answer = 90;
        Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (pivot.rotation.z != targetValue)
        {
            UpdateGauge();
            //Debug.Log("UpdateGauge()");
        }
        if (isInRange)
        {
            WithinRange();
            //Debug.Log("WithinRange()");
        }
        Deplete();
        //Debug.Log(pivot.localEulerAngles.z);
        if (pivot.localEulerAngles.z < 0 || pivot.localEulerAngles.z > 350)
        {
            pivot.localEulerAngles = new Vector3(pivot.localEulerAngles.x, pivot.localEulerAngles.y, 0f);
        }
        //Debug.Log(isInRange + ", " + pivot.localEulerAngles.z);
    }
    private void Deplete()
    {
        if (targetValue > 0)
        {
            targetValue -= depleteRate * Time.deltaTime;
        }
    }
    public void UpdateTarget(bool isUp)
    {
        if (targetValue == answer)
        {

        }
        else if (isUp && targetValue < maxAngle + precision)
        {
            targetValue += moveAmount;
            //pivot.Rotate(Vector3.forward, moveAmount);
        }
        else if (targetValue > minAngle - precision)
        {
            targetValue -= moveAmount;
            //pivot.Rotate(Vector3.forward, -moveAmount);
        }
        Debug.Log(pivot.localEulerAngles.z + ", " + targetValue);
    }
    void UpdateGauge()
    {
        if(pivot.localEulerAngles.z <= targetValue - precision)
        {
            pivot.Rotate(Vector3.forward, Mathf.Min(moveSpeed, Mathf.Lerp(pivot.localEulerAngles.z, targetValue, moveAccel)-pivot.localEulerAngles.z, targetValue - pivot.localEulerAngles.z));
        }
        else if (pivot.localEulerAngles.z > targetValue + precision)
        {
            pivot.Rotate(Vector3.forward, -Mathf.Min(moveSpeed, Mathf.Lerp(pivot.localEulerAngles.z, targetValue, moveAccel)-targetValue, pivot.localEulerAngles.z - targetValue));
        }
        if (pivot.localEulerAngles.z <= answerMax && pivot.localEulerAngles.z >= answerMin && !isInRange && active)
        {
            counter = holdTime;
            isInRange = true;
        }
        else if ((pivot.localEulerAngles.z > answerMax || pivot.localEulerAngles.z < answerMin) && isInRange)
        {
            isInRange = false;
        }
        /*if (Mathf.Abs(pivot.localEulerAngles.z - (float)answer) < precision && active)
        {
            Complete();
        }*/
    }
    void WithinRange()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            Complete();
            isInRange = false;
        }
    }
    public override void Enable()
    {
        active = true;
    }
    public override void Disable()
    {
        active = false;
    }
    public override void ResetGame()
    {
        Disable();
        taskLight.SetCompletion(false);
    }
    public override void Randomize(Transform t)
    {
        answer = (Random.Range(0, 3) + 1) * 20;
        answerMin = answer;
        answerMax = answer + 20;
        thisAnswer = Instantiate(answerTemplate, t);
        thisAnswer.GetComponent<Answer>().DisplayAnswer(minigameIcon.sprite, panel.number, minigameColor, answer.ToString());
    }
}
