using UnityEngine;

public class TowerIntensity : MonoBehaviour
{
    [SerializeField] Roller roller;
    [SerializeField] ControlAnimateLights flashingLights;
    [SerializeField] float rollerFactor, lightsFactor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateIntensity(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateIntensity(int completedTasks)
    {
        roller.speed = rollerFactor * (completedTasks+1);
        flashingLights.interval = lightsFactor / (completedTasks+1);
        //whirr sound
    }
}
