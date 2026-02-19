using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using FMOD.Studio;

public abstract class WorkButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Animator anim;
    private bool canClick = true;
    [SerializeField] EventReference buttonSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        buttonSound = RuntimeManager.PathToEventReference("event:/SFX/Work/ButtonClick");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //blank

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //blank
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            anim.Play("ButtonClick", -1, 0);
            AudioManager.ins.PlayOneShot(buttonSound, transform.position);
            //Debug.Log(buttonSound.Path);
            if (canClick)
            {
                OnClick();
            }
        }
    }
    public abstract void OnClick();
    public void IsActive(bool s)
    {
        canClick = s;
    }
}
