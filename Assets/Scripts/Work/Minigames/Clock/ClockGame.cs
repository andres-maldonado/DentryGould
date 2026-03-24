using UnityEngine;

public class ClockGame : Minigame
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Disable()
    {
        active = false;
    }
    public override void Enable() 
    {
        active = true;
    }
    public override void Randomize(Transform p)
    {

    }
    public override void ResetGame()
    {

    }
}
