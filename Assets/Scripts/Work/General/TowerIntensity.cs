using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class TowerIntensity : MonoBehaviour
{
    [SerializeField] Roller roller;
    [SerializeField] ControlAnimateLights flashingLights;
    [SerializeField] WorkLightFlicker lightFlicker;
    [SerializeField] float rollerFactor, lightsFactor, flickerFactor, flickerIntensity, flickerLength, flickerPeriod, flickerRate;
    [SerializeField] EventReference machineWhirr;

    private EventInstance whirrInstance;
    private bool isHardFlickering;
    private float counter, flickerCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PowerDown();
        whirrInstance = AudioManager.ins.CreateInstance(machineWhirr);
        GameObject.Find("CameraRotate").GetComponent<WorkCamControl>().machineSound = whirrInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHardFlickering)
        {
            flickerCounter -= Time.deltaTime;
            if (flickerCounter < 0)
            {
                if (Random.Range(0f, 1f) < flickerFactor)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        lightFlicker.Flicker(false, flickerLength, Mathf.Sqrt(flickerIntensity - (flickerIntensity - 1) * counter / flickerPeriod));
                    }
                    else
                    {
                        lightFlicker.Flicker(true, flickerLength, flickerIntensity - (flickerIntensity - 1) * counter / flickerPeriod);
                    }
                }
                flickerCounter = flickerRate;
            }
        }
        counter += Time.deltaTime;
        if (counter > flickerPeriod)
        {
            isHardFlickering = false;
        }
    }
    public void StartWhirr()
    {
        whirrInstance.start();
    }
    public void UpdateIntensity(int completedTasks)
    {
        roller.UpdateSpeed(rollerFactor * (completedTasks + 1));
        flashingLights.on = true;
        flashingLights.interval = lightsFactor / (completedTasks+1);
        whirrInstance.setParameterByName("TasksCompleted", completedTasks);
        //whirr sound
    }
    public void LightFlickerUp()
    {
        counter = 0;
        isHardFlickering = true;
    }
    public void PowerDown()
    {
        roller.UpdateSpeed(0);
        flashingLights.PowerDown();
        whirrInstance.setParameterByName("ShiftEnding", 1);
    }
}
