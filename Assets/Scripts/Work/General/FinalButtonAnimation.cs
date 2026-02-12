using UnityEngine;

public class FinalButtonAnimation : MonoBehaviour
{
    [SerializeField] Animator handAnim;
    [SerializeField] Animator rotateAnim;
    [SerializeField] Animator camAnim;
    [SerializeField] Animator ticketAnim;
    [SerializeField] WorkCamControl workCamControl;
    private MinigameManager minigameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
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
    }
    void NewTask()
    {
        minigameManager.WaveCompletion();
    }
    void BackUp()
    {
        camAnim.Play("FinalButtonReturn");
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
