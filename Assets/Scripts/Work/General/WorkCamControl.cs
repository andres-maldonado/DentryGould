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

    private InputActionMap inputActionMap;
    private InputAction turnLeft, turnRight, zoomIn, zoomOut, rClick;
    private Animator thisAnim;
    private EventReference ticketUpSound, ticketDownSound;
    private bool zoomedIn;
    public bool isTurning;
    private bool atTicket;
    private float newPosition;
    private float nextPosition;
    private int currentPanel = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActionMap = inputActionAsset.FindActionMap("Player");
        turnLeft = inputActionMap.FindAction("Left");
        turnLeft.performed += _ => TurnLeft();
        turnRight = inputActionMap.FindAction("Right");
        turnRight.performed += _ => TurnRight();
        zoomIn = inputActionMap.FindAction("Up");
        zoomIn.performed += _ => ZoomIn();
        zoomOut = inputActionMap.FindAction("Down");
        zoomOut.performed += _ => ZoomOut();
        rClick = inputActionMap.FindAction("RightClick");
        rClick.performed += _ => LookAtTicket();
        rClick.canceled += _ => PutTicketDown();
        thisAnim = GetComponent<Animator>();
        thisAnim. enabled = false;
        ticketUpSound = RuntimeManager.PathToEventReference("event:/SFX/Work/TicketUp");
        ticketDownSound = RuntimeManager.PathToEventReference("event:/SFX/Work/TicketDown");
    }
    private void TurnLeft()
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
            }
        }        
    }
    private void TurnRight()
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
            }
        }
    }
    private void ZoomIn()
    {
        if (!zoomedIn && currentPanel != 0)
        {
            if (atTicket)
            {
                PutTicketDown();
            }
            zoomAnim.Play("PanelZoomIn");
            zoomedIn = true;
        }
    }
    private void ZoomOut()
    {
        if (zoomedIn && !atTicket)
        {
            zoomAnim.Play("PanelZoomOut");
            zoomedIn = false;
        }
    }
    private void LookAtTicket()
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
        }
    }
    private void PutTicketDown()
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
        }
        else
        {
            turnLeft.Enable();
            turnRight.Enable();
            zoomIn.Enable();
            zoomOut.Enable();
            rClick.Enable();
        }
    }
}
