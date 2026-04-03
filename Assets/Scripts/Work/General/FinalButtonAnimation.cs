using FMODUnity;
using UnityEngine;

public class FinalButtonAnimation : MonoBehaviour
{
    [SerializeField] Animator handAnim;
    [SerializeField] Animator rotateAnim;
    [SerializeField] Animator camAnim;
    [SerializeField] Animator ticketAnim;
    [SerializeField] WorkCamControl workCamControl;
    [SerializeField] FinalButton button;
    [SerializeField] int eveningScene;
    private MinigameManager minigameManager;
    private EventReference thudSound;
    private EventReference ticketSound;
    private EventReference stepsBackSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handAnim = GameObject.Find("WhiteHand").GetComponent<Animator>();
        rotateAnim = GameObject.Find("CameraRotate").GetComponent<Animator>();
        camAnim = GameObject.Find("CameraZoomIn").GetComponent<Animator>();
        ticketAnim = GameObject.Find("TicketAnim").GetComponent<Animator>();
        workCamControl = GameObject.Find("CameraRotate").GetComponent<WorkCamControl>();
        button = GameObject.Find("FINALBUTTON").GetComponent<FinalButton>();
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
        if (!minigameManager.isEnding)
        {
            ticketAnim.Play("PrintTicket", -1, 0);
            AudioManager.ins.PlayOneShot(ticketSound, transform.position);
        }
        else
        {
            Invoke("BackUp", 1);
        }
    }
    void NewTask()
    {
        Debug.Log(minigameManager.isEnding);
        if (!minigameManager.isEnding)
        {
            minigameManager.WaveCompletion();
            if (!button.firstHit)
            {
                GameObject.Find("Tower").GetComponent<TowerIntensity>().LightFlickerUp();
            }
        }
        else
        {
            minigameManager.successes++;
            minigameManager.EndShift();
        }
        if (button.firstHit)
        {
            GameObject.Find("Tower").GetComponent<TowerIntensity>().StartWhirr();
            button.firstHit = false;
        }
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

    void DoorLeave()
    {
        rotateAnim.enabled = true;
        rotateAnim.Play("DoorLeave");
    }
    void ContinueToEvening()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().FadeOutOfScene(2, eveningScene);
    }
}
