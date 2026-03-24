using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ClockHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Transform pivot, clockCircle;
    [SerializeField] float maxTurnSpeed, clockValue;


    private bool isDragging;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.transform == clockCircle)
                {
                    Vector3 mousePosition = Vector3.ProjectOnPlane(hitInfo.point, pivot.forward);
                    //Debug.Log(Vector3.Angle(pivot.up, mousePosition - pivot.position));
                    //Debug.Log(transform.up);
                    float signedAngle = Vector3.Angle(transform.up, mousePosition - pivot.position) * Mathf.Sign(Vector3.Dot(-transform.right, mousePosition-pivot.position));
                    if (signedAngle > maxTurnSpeed)
                    {
                        signedAngle = maxTurnSpeed;
                    }
                    else if (signedAngle < -maxTurnSpeed)
                    {
                        signedAngle = -maxTurnSpeed;
                    }
                    pivot.Rotate(Vector3.forward, signedAngle);
                    /*if (pivot.localEulerAngles.z < 0)
                    {
                        clockValue = pivot.localEulerAngles.z / -30;
                    }
                    else if (pivot.localEulerAngles.z < 0)
                    {
                        clockValue = 12 - pivot.localEulerAngles.z / 30;
                    }*/
                    clockValue = 12 - pivot.localEulerAngles.z / 30;
                    Debug.Log(clockValue);
                    //pivot.localEulerAngles = new Vector3(0, 0, Vector3.Angle(pivot.position, hitInfo.point));

                }
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }

}
