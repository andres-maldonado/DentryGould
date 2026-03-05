using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class TowerIntensity : MonoBehaviour
{
    [SerializeField] Roller roller;
    [SerializeField] ControlAnimateLights flashingLights;
    [SerializeField] float rollerFactor, lightsFactor;
    [SerializeField] EventReference machineWhirr;

    private EventInstance whirrInstance;
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
    public void PowerDown()
    {
        roller.UpdateSpeed(0);
        flashingLights.PowerDown();
        whirrInstance.setParameterByName("ShiftEnding", 1);
    }
}
