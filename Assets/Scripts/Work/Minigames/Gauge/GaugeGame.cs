using NUnit.Framework.Constraints;
using UnityEngine;

public class GaugeGame : Minigame
{
    [SerializeField] float moveAmount, moveSpeed, moveAccel, minAngle, maxAngle;
    [SerializeField] Transform pivot;

    private float targetValue, actualValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        if (isUp)
        {
            targetValue += moveAmount;
        }
        else
        {
            targetValue -= moveAmount;
        }
    }
    void UpdateGauge()
    {
        if(pivot.rotation.z <= targetValue)
        {
            pivot.Rotate(Vector3.forward, Mathf.Min(Mathf.Max(moveSpeed, Mathf.Lerp(pivot.rotation.z, targetValue, moveAccel)), targetValue - pivot.rotation.z));
        }
        else if (pivot.rotation.z > targetValue)
        {
            pivot.Rotate(Vector3.forward, -Mathf.Min(Mathf.Max(moveSpeed, Mathf.Lerp(pivot.rotation.z, targetValue, moveAccel)), pivot.rotation.z - targetValue));
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
        throw new System.NotImplementedException();
    }
    public override void Randomize(Transform t)
    {
        throw new System.NotImplementedException();
    }
}
