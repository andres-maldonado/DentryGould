using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinalButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] MinigameManager manager;
    [SerializeField] WorkCamControl camParent;
    public Animator anim;
    private bool canClick = true;
    public bool ready;

    private EventReference stepsSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        anim = GetComponent<Animator>();
        stepsSound = RuntimeManager.PathToEventReference("event:/SFX/Work/FinalSteps");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (canClick)
            {
                OnClick();
            }
        }
    }
    public void OnClick()
    {
        Debug.Log("Ready: " + ready);
        if (ready)
        {
            camParent.FinalButtonSequence();
            AudioManager.ins.PlayOneShot(stepsSound, transform.position);
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
    public void IsActive(bool s)
    {
        canClick = s;
    }
}
