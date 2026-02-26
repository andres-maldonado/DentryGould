using UnityEngine;

public class Roller : MonoBehaviour
{
    public float rollerSpeed, accelerateRate;
    private float currentSpeed;

    private bool isAccelerating;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //towerintensity controls
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localEulerAngles += new Vector3(0, 0, currentSpeed * Time.deltaTime);
        if (isAccelerating)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, rollerSpeed, accelerateRate * Time.deltaTime);
            if (rollerSpeed + .1f > currentSpeed && rollerSpeed - .1f < currentSpeed)
            {
                rollerSpeed = currentSpeed;
                isAccelerating = false;
            }
        }
    }
    public void UpdateSpeed(float speed)
    {
        rollerSpeed = speed;
        isAccelerating = true;
    }
}
