using UnityEngine;

public class FinalButton : WorkButton
{
    [SerializeField] MinigameManager manager;
    [SerializeField] WorkCamControl camParent;
    public bool ready;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }
    public override void OnClick()
    {
        Debug.Log("Ready: " + ready);
        if (ready)
        {
            camParent.FinalButtonSequence();
        }
    }
    public void Activate()
    {
        ready = true;
        //make it flash later
    }
    public void Deactivate()
    {
        ready = false;
    }
}
