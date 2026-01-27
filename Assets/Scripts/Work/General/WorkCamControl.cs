using UnityEngine;
using UnityEngine.InputSystem;

public class WorkCamControl : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] float turnDegrees;
    [SerializeField] float turnSpeed;
    [SerializeField] float turnSnapPoint;
    [SerializeField] int maxPanelNum;
    [SerializeField] int minPanelNum;
    [SerializeField] bool circle;

    private InputActionMap inputActionMap;
    private InputAction turnLeft, turnRight, zoomIn, zoomOut;
    private Animator zoomAnim;
    private bool zoomedIn;
    private bool isTurning;
    private float newPosition;
    private float nextPosition;
    private int currentPanel = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
        zoomAnim = GetComponentInChildren<Animator>();
        Debug.Log("zoomIn = " + zoomIn);
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
                Debug.Log("Current Position: " + transform.eulerAngles.y + ", New Position: " + newPosition);
                Debug.Log(transform.eulerAngles.y - newPosition);
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
                Debug.Log("Current Position: " + transform.eulerAngles.y + ", New Position: " + newPosition);
                Debug.Log(transform.eulerAngles.y - newPosition);
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
            Debug.Log("Current Position: " + transform.eulerAngles.y + ", New Position: " + newPosition);
            Debug.Log(currentPanel);
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
                Debug.Log("Current Position: " + transform.eulerAngles.y + ", New Position: " + newPosition);
                Debug.Log(newPosition - transform.eulerAngles.y);
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
                Debug.Log("Current Position: " + transform.eulerAngles.y + ", New Position: " + newPosition);
                Debug.Log(transform.eulerAngles.y - newPosition);
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
            Debug.Log("Current Position: " + transform.eulerAngles.y + ", New Position: " + newPosition);
            Debug.Log(currentPanel);
        }
    }
    private void ZoomIn()
    {
        if (!zoomedIn)
        {
            zoomAnim.Play("PanelZoomIn");
            zoomedIn = true;
        }
    }
    private void ZoomOut()
    {
        if (zoomedIn)
        {
            zoomAnim.Play("PanelZoomOut");
            zoomedIn = false;
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
                Debug.Log("CHANGE: NextPosition: " + nextPosition + ", NewPosition: " + newPosition);
            }
            
            transform.eulerAngles = new Vector3(0, nextPosition, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isTurning)
        {
            Turn();
        }
    }

}
