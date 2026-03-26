using NUnit.Framework.Constraints;
using UnityEngine;

public class GaugeGame : Minigame
{
    [SerializeField] float moveAmount, moveSpeed, moveAccel, minAngle, maxAngle, precision;
    [SerializeField] Transform pivot;

    private float targetValue, actualValue;
    private int answer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetValue = 0;
        answer = -10;
    }

    // Update is called once per frame
    void Update()
    {
        if (pivot.rotation.z != targetValue)
        {
            UpdateGauge();
        }
    }
    public void UpdateTarget(bool isUp)
    {
        if (isUp && targetValue < maxAngle + precision)
        {
            targetValue += moveAmount;
            //pivot.Rotate(Vector3.forward, moveAmount);
        }
        else if (targetValue > minAngle - precision)
        {
            targetValue -= moveAmount;
            //pivot.Rotate(Vector3.forward, -moveAmount);
        }
        Debug.Log(pivot.eulerAngles.z + ", " + targetValue);
    }
    void UpdateGauge()
    {
        if(pivot.eulerAngles.z <= targetValue - precision)
        {
            pivot.Rotate(Vector3.forward, Mathf.Min(moveSpeed, Mathf.Lerp(pivot.eulerAngles.z, targetValue, moveAccel)-pivot.eulerAngles.z, targetValue - pivot.eulerAngles.z));
        }
        else if (pivot.eulerAngles.z > targetValue + precision)
        {
            pivot.Rotate(Vector3.forward, -Mathf.Min(moveSpeed, Mathf.Lerp(pivot.eulerAngles.z, targetValue, moveAccel)-targetValue, pivot.eulerAngles.z - targetValue));
        }
        if (Mathf.Abs(pivot.eulerAngles.z - (float)answer) < precision && active)
        {
            Complete();
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
    }
    public override void Randomize(Transform t)
    {
        answer = Random.Range(0, (int)maxAngle / (int)moveAmount + 1) * (int)moveAmount;
        thisAnswer = Instantiate(answerTemplate, t);
        thisAnswer.GetComponent<Answer>().DisplayAnswer(minigameIcon.sprite, minigameColor, answer.ToString());
    }
}
