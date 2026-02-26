using UnityEngine;

public class TowerIntensity : MonoBehaviour
{
    [SerializeField] Roller roller;
    [SerializeField] ControlAnimateLights flashingLights;
    [SerializeField] float rollerFactor, lightsFactor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PowerDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateIntensity(int completedTasks)
    {
        roller.UpdateSpeed(rollerFactor * (completedTasks + 1));
        flashingLights.on = true;
        flashingLights.interval = lightsFactor / (completedTasks+1);
        Debug.Log(lightsFactor + " / (" + completedTasks + " + 1 = " + lightsFactor / (completedTasks + 1));
        //whirr sound
    }
    public void PowerDown()
    {
        roller.UpdateSpeed(0);
        flashingLights.PowerDown();
    }
}
