using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Knob : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isHovering;
    private bool isDragging;
    private float lastFrame;
    private float thisFrame;
    [SerializeField] float scale;
    [SerializeField] KnobGame game;
    [SerializeField] InputActionAsset input;
    private EventReference knobStartSound;
    private EventReference knobStopSound;
    private InputActionMap map;
    private InputAction look;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        map = input.FindActionMap("Player");
        look = map.FindAction("Look");
        knobStartSound = RuntimeManager.PathToEventReference("event:/SFX/Work/KnobStart");
        knobStopSound = RuntimeManager.PathToEventReference("event:/SFX/Work/KnobStop");

    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            ChangeAngle();
        }
    }
    void ChangeAngle()
    {
        gameObject.transform.eulerAngles += new Vector3(0, 0, scale * (look.ReadValue<Vector2>().x + look.ReadValue<Vector2>().y));
        if(gameObject.transform.localEulerAngles.z % 360 < 240 && gameObject.transform.localEulerAngles.z % 360 > 180)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, -120);
        }
        if (gameObject.transform.localEulerAngles.z % 360 > 120 && gameObject.transform.localEulerAngles.z % 360 < 180)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, 120);
        }
    }
    public void FixAngle()
    {
        float angle = gameObject.transform.eulerAngles.z % 360;
        Debug.Log(angle);
        AudioManager.ins.PlayOneShot(knobStopSound, transform.position);
        if (angle >= 239 && angle < 270)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -120);
            game.UpdateKnobs(this, 1);
        }
        else if (angle >= 270 && angle < 330)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -60);
            game.UpdateKnobs(this, 2);
        }
        else if (angle >= 330 || angle < 30)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            game.UpdateKnobs(this, 3);
        }
        else if (angle < 90)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 60);
            game.UpdateKnobs(this, 4);
        }
        else
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 120);
            game.UpdateKnobs(this, 5);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("down");
        if (isHovering && eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = true;
            AudioManager.ins.PlayOneShot(knobStartSound, transform.position);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Up");
        if (isDragging)
        {
            isDragging = false;
            FixAngle();
        }
    }
}
