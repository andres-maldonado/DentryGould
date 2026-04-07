using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class FinalButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] MinigameManager manager;
    [SerializeField] WorkCamControl camParent;
    [SerializeField] EventReference workMusic;
    public Animator anim;
    private bool canClick = true;
    public bool ready;
    public bool firstHit;

    private EventReference stepsSound;
    private ButtonLamp lamp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        manager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
        camParent = GameObject.Find("CameraRotate").GetComponent<WorkCamControl>();
        anim = GetComponent<Animator>();
        stepsSound = RuntimeManager.PathToEventReference("event:/SFX/Work/FinalSteps");
        lamp = GetComponent<ButtonLamp>();
        GameObject.Find("LoudspeakerDialogue").GetComponent<LoudspeakerDialogue>().endWriting += ShiftStart;
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
            if (firstHit)
            {
                manager.BeginShift();
                GameObject.Find("MusicManager").GetComponent<MusicManager>().ReplaceMusic(workMusic);
                GameObject.Find("Tower").GetComponent<TowerIntensity>().StartWhirr();
            }
            camParent.FinalButtonSequence();
            AudioManager.ins.PlayOneShot(stepsSound, transform.position);
        }
    }
    public void ShiftStart()
    {
        firstHit = true;
        Activate();
    }
    public void Activate()
    {
        ready = true;
        lamp.on = true;
        lamp.SetColor();
        //make it flash later
    }
    public void Deactivate()
    {
        ready = false;
        lamp.on = false;
        lamp.SetColor();
    }
    public void IsActive(bool s)
    {
        canClick = s;
    }
}
