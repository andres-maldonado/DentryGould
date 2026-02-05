using UnityEngine;

public class Roller : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localEulerAngles += new Vector3(0, 0, speed);
    }
}
