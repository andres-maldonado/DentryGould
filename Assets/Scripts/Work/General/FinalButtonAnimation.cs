using FMODUnity;
using UnityEngine;

public class FinalButtonAnimation : MonoBehaviour
{
    [SerializeField] Animator handAnim;
    [SerializeField] Animator rotateAnim;
    [SerializeField] Animator camAnim;
    [SerializeField] Animator ticketAnim;
    [SerializeField] WorkCamControl workCamControl;
    private MinigameManager minigameManager;
    private EventReference thudSound;
    private EventReference ticketSound;
    private EventReference stepsBackSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
        thudSound = RuntimeManager.PathToEventReference("event:/SFX/Work/FinalThud");
        ticketSound = RuntimeManager.PathToEventReference("event:/SFX/Work/TicketPrint");
        stepsBackSound = RuntimeManager.PathToEventReference("event:/SFX/Work/FinalStepsBack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandSmash()
    {
        handAnim.Play("FinalButtonHand");
    }
    void PrintTicket()
    {
        ticketAnim.Play("PrintTicket", -1, 0);
        AudioManager.ins.PlayOneShot(ticketSound, transform.position);

    }
    void NewTask()
    {
        minigameManager.WaveCompletion();
        AudioManager.ins.PlayOneShot(thudSound, transform.position);
    }
    void BackUp()
    {
        camAnim.Play("FinalButtonReturn");
        AudioManager.ins.PlayOneShot(stepsBackSound, transform.position);
    }
    void Return()
    {
        rotateAnim.Play("FinalButtonEnd");
    }
    void Finish()
    {
        rotateAnim.enabled = false;
        workCamControl.LockControls(false);
    }
}
