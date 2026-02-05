using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimateLights : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ButtonLamp> controls;
    public float interval = 1f;
    public float startTime = 0;
    private int result;
    void Start()
    {
        InvokeRepeating("Run", startTime, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Run()
    {
        foreach (ButtonLamp item in controls)
        {
            item.Run();
        }
    }

    public static bool intToBool(int Number)
    {
        return (Number == 0 ? false : true);
    }
}
