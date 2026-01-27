using UnityEngine;

public class FinalButton : WorkButton
{
    private MinigameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        manager = GetComponentInParent<MinigameManager>();
    }
    public override void OnClick()
    {
        manager.WaveCompletion();
    }
    public void Activate()
    {
        IsActive(true);
        //make it flash later
    }
}
