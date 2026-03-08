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
    private float counter;
    public bool on = true;
    void Start()
    {
        Run();
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0 && on)
        {
            Run();
            counter = interval;
        }
        else if (counter <=0 && !on)
        {
            foreach (ButtonLamp item in controls)
            {
                item.on = !item.on;
                item.SetColor();
            }
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
    public void PowerDown()
    {
        on = false;
        interval = .6f;
    }
}
