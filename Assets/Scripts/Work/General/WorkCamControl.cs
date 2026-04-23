using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorkCamControl : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] Animator zoomAnim;
    [SerializeField] Animator handAnim;
    [SerializeField] float turnDegrees;
    [SerializeField] float turnSpeed;
    [SerializeField] float turnSnapPoint;
    [SerializeField] int maxPanelNum;
    [SerializeField] int minPanelNum;
    [SerializeField] bool circle;
    private GameObject aKey, dKey, wKey, sKey, mouseKey;

    public EventInstance machineSound;

    private InputActionMap inputActionMap;
    public InputAction turnLeft, turnRight, zoomIn, zoomOut, rClick;
    private Animator thisAnim;
    private EventReference ticketUpSound, ticketDownSound;
    private bool zoomedIn;
    public bool isTurning;
    public bool atTicket;
    private bool ticketOff;
    private float newPosition;
    private float nextPosition;
    private int currentPanel = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActionMap = inputActionAsset.FindActionMap("Player");
        turnLeft = inputActionMap.FindAction("Left");
        turnLeft.performed += TurnLeft;
        turnRight = inputActionMap.FindAction("Right");
        turnRight.performed += TurnRight;
        zoomIn = inputActionMap.FindAction("Up");
        zoomIn.performed += ZoomIn;
        zoomOut = inputActionMap.FindAction("Down");
        zoomOut.performed += ZoomOut;
        rClick = inputActionMap.FindAction("RightClick");
        rClick.performed += LookAtTicket;
        rClick.canceled += PutTicketDown;
        thisAnim = GetComponent<Animator>();
        thisAnim. enabled = false;
        ticketUpSound = RuntimeManager.PathToEventReference("event:/SFX/Work/TicketUp");
        ticketDownSound = RuntimeManager.PathToEventReference("event:/SFX/Work/TicketDown");
        wKey = GameObject.Find("W Key");
        aKey = GameObject.Find("A Key");
        sKey = GameObject.Find("S Key");
        dKey = GameObject.Find("D Key");
        mouseKey = GameObject.Find("Mouse Key");
    }
    private void TurnLeft(InputAction.CallbackContext context)
    {
        if (!zoomedIn)
        {
            if (currentPanel > minPanelNum)
            {
                newPosition = currentPanel * 60 - turnDegrees;
                while (transform.eulerAngles.y < newPosition)
                {
                    newPosition -= 360;
                }
                if (transform.eulerAngles.y - newPosition > 360)
                {
                    newPosition += 360;
                }
                /*if (newPosition < 0)
                {
                    newPosition += 360;
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 360, 0);
                }*/
                isTurning = true;
                currentPanel--;
                machineSound.setParameterByName("CamPosition", currentPanel, false);
            }
            else if (circle && currentPanel == minPanelNum)
            {
                newPosition = currentPanel * 60 - turnDegrees;
                while (transform.eulerAngles.y < newPosition)
                {
                    newPosition -= 360;
                }
                if (transform.eulerAngles.y - newPosition > 360)
                {
                    newPosition += 360;
                }
                /*if (newPosition >= 360)
                {
                    newPosition -= 360;
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 360, 0);
                }*/
                //Debug.Log("Euler Y: " + transform.eulerAngles.y + ", newPosition: " + newPosition);
                isTurning = true;
                currentPanel = maxPanelNum;
                machineSound.setParameterByName("CamPosition", currentPanel, true);
            }
            if (currentPanel == 0)
            {
                wKey.SetActive(false);
            }
            else
            {
                wKey.SetActive(true);
            }
        }        
    }
    private void TurnRight(InputAction.CallbackContext context)
    {
        if (!zoomedIn)
        {
            if (currentPanel < maxPanelNum)
            {
                newPosition = currentPanel * 60 + turnDegrees;
                while (transform.eulerAngles.y > newPosition)
                {
                    newPosition += 360;
                }
                if (newPosition - transform.eulerAngles.y > 360)
                {
                    newPosition -= 360;
                }
                /*if (newPosition >= 360)
                {
                    newPosition -= 360;
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 360, 0);
                }*/
                //Debug.Log("Euler Y: " + transform.eulerAngles.y + ", newPosition: " + newPosition);
                isTurning = true;
                currentPanel++;
                machineSound.setParameterByName("CamPosition", currentPanel, false);

            }
            else if (circle && currentPanel == maxPanelNum)
            {
                newPosition = currentPanel * 60 + turnDegrees;
                while (transform.eulerAngles.y > newPosition)
                {
                    newPosition += 360;
                }
                if (transform.eulerAngles.y - newPosition < -360)
                {
                    newPosition -= 360;
                }
                /*if (newPosition >= 360)
                {
                    newPosition -= 360;
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 360, 0);
                }*/
                //Debug.Log("Euler Y: " + transform.eulerAngles.y + ", newPosition: " + newPosition);
                isTurning = true;
                currentPanel = minPanelNum;
                machineSound.setParameterByName("CamPosition", currentPanel, true);
            }
            if (currentPanel == 0)
            {
                wKey.SetActive(false);
            }
            else
            {
                wKey.SetActive(true);
            }
        }
    }
    private void ZoomIn(InputAction.CallbackContext context)
    {
        if (!zoomedIn && currentPanel != 0)
        {
            if (atTicket)
            {
                PutTicketDownDetached();
            }
            zoomAnim.Play("PanelZoomIn");
            zoomedIn = true;
            ShowUI(false);
            sKey.SetActive(true);
        }
    }
    private void ZoomOut(InputAction.CallbackContext context)
    {
        if (zoomedIn && !atTicket)
        {
            zoomAnim.Play("PanelZoomOut");
            zoomedIn = false;
            ShowUI(true);
            sKey.SetActive(false);
        }
    }
    private void LookAtTicket(InputAction.CallbackContext context)
    {
        if (!atTicket)
        {
            if (zoomedIn)
            {
                zoomAnim.Play("InTicketLook");
                handAnim.Play("InHandLift");
            }
            if (!zoomedIn)
            {
                handAnim.Play("OutHandLift");
            }
            AudioManager.ins.PlayOneShot(ticketUpSound, transform.position);
            atTicket = true;
            mouseKey.SetActive(false);
        }
    }
    private void PutTicketDown(InputAction.CallbackContext context)
    {
        PutTicketDownDetached();
    }
    void PutTicketDownDetached()
    {
        if (atTicket)
        {
            if (zoomedIn)
            {
                zoomAnim.Play("InTicketDown");
                handAnim.Play("InHandLower");
            }
            if (!zoomedIn)
            {
                handAnim.Play("OutHandLower");
            }
            AudioManager.ins.PlayOneShot(ticketDownSound, transform.position);
            atTicket = false;
            mouseKey.SetActive(true);
        }
    }
    public void SetTicketEnabled(bool b)
    {
        if (!b)
        {
            PutTicketDownDetached();
            rClick.Disable();
            ticketOff = true;
            mouseKey.SetActive(false);
        }
        if (b)
        {
            rClick.Enable();
            ticketOff = false;
            //mouseKey.SetActive(true);
        }
    }
    private void Turn()
    {
        if (transform.eulerAngles.y - newPosition < turnSnapPoint && transform.eulerAngles.y - newPosition > -turnSnapPoint)
        {
            transform.eulerAngles = new Vector3(0, newPosition % 360, 0);
            isTurning = false;
            //Debug.Log("Rotation: " + transform.rotation);
        }
        else
        {
            nextPosition = Mathf.Lerp(transform.eulerAngles.y, newPosition, turnSpeed);
            //Debug.Log("NextPosition: " + nextPosition + ", NewPosition: " + newPosition);
            if (nextPosition < 0 || nextPosition >= 360)
            {
                nextPosition = (nextPosition + 360) % 360;
                newPosition = ((currentPanel * 60) + 360) % 360;
            }
            
            transform.eulerAngles = new Vector3(0, nextPosition, 0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isTurning)
        {
            Turn();
        }
    }

    public void FinalButtonSequence()
    {
        thisAnim.enabled = true;
        LockControls(true);
        GetComponent<Animator>().Play("FinalButtonRotate");
        zoomAnim.Play("FinalButtonHit");
    }

    public void LockControls(bool isLocking)
    {
        if (isLocking)
        {
            turnLeft.Disable();
            turnRight.Disable();
            zoomIn.Disable();
            zoomOut.Disable();
            rClick.Disable();
            ShowUI(false);
        }
        else
        {
            turnLeft.Enable();
            turnRight.Enable();
            zoomIn.Enable();
            zoomOut.Enable();
            ShowUI(true);
            if (!ticketOff)
            {
                rClick.Enable();
            }
        }
    }
    public void ShowUI(bool isOn)
    {
        if (!isOn)
        {
            wKey.SetActive(false);
            aKey.SetActive(false);
            sKey.SetActive(false);
            dKey.SetActive(false);
            mouseKey.SetActive(false);
        }
        else
        {
            if (!zoomedIn)
            {
                if (currentPanel != 0)
                {
                    wKey.SetActive(true);
                }
                aKey.SetActive(true);
                dKey.SetActive(true);
                if (!atTicket && !ticketOff)
                {
                    mouseKey.SetActive(true);
                }
            }
            else
            {
                sKey.SetActive(true);
                if (!atTicket && !ticketOff)
                {
                    mouseKey.SetActive(true);
                }
            }
        }
    }
    private void OnDestroy()
    {
        turnLeft.performed -= TurnLeft;
        turnRight.performed -= TurnRight;
        zoomIn.performed -= ZoomIn;
        zoomOut.performed -= ZoomOut;
        rClick.performed -= LookAtTicket;
        rClick.canceled -= PutTicketDown;
    }
}
