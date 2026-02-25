using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class ControlAnimateLights : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ButtonLamp> controls;
    public float interval = 1f;
    public float startTime = 0;
    private int result;
    private float counter;
    void Start()
    {
        Invoke("Run", 0);
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0 )
        {
            Run();
            counter = interval;
        }
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
