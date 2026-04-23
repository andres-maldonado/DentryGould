using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorButton : MonoBehaviour, IPointerDownHandler
{
    public Animator doorAnim;
    [SerializeField] int eveningScene;
    [SerializeField] EventReference lockSound;
    [SerializeField] EventReference exitSound;

    public bool canExit, isExiting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!canExit)
            {
                doorAnim.Play("DoorLocked", 0, 0);
                AudioManager.ins.PlayOneShot(lockSound, this.transform.position);
            }
            if (canExit && !isExiting)
            {
                doorAnim.Play("DoorExit");
                AudioManager.ins.musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                AudioManager.ins.PlayOneShot(exitSound, this.transform.position);
                GameObject.Find("CameraRotate").GetComponent<WorkCamControl>().ShowUI(false);
                isExiting = true;
            }

        }
    }
}
