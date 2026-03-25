using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ClockHand : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Transform pivot, clockCircle;
    [SerializeField] float maxTurnSpeed, clockValue, snapDistance;
    [SerializeField] ClockGame game;


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
                    clockValue = 12 - pivot.localEulerAngles.z / 30;
                }
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (game.active)
        {
            isDragging = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        if (clockValue % 1 <= 0 + snapDistance)
        {
            float snapAngle = clockValue % 1;
            Debug.Log(snapAngle);
            pivot.Rotate(Vector3.forward, snapAngle*30);
            clockValue = Mathf.Floor(clockValue);
        }
        else if (clockValue % 1 >= 1 - snapDistance)
        {
            float snapAngle = 1 - clockValue % 1;
            Debug.Log(snapAngle);
            pivot.Rotate(Vector3.forward, -snapAngle*30);
            clockValue = Mathf.Ceil(clockValue);
        }
        game.UpdateValue(this.transform.name, clockValue);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }

}
