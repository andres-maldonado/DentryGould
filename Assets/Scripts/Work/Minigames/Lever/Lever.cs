using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Lever : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform pivotPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] float angleMin, angleMax;
    [SerializeField] InputActionAsset input;
    private InputActionMap map;
    private InputAction look;

    private bool isDragging;
    private bool isHovering;
    private float moveDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        map = input.FindActionMap("Player");
        look = map.FindAction("Look");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDragging && !isHovering)
        {
            DragLever();
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        //Debug.Log("Dragging: "+isDragging+", Hovering: "+isHovering);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if(isDragging )
        {
            moveDir = -moveSpeed * look.ReadValue<Vector2>().y;
        }
    }
    public void DragLever()
    {
        if (isDragging && !isHovering)
        {
            //pivotPoint.localEulerAngles += new Vector3(moveDir, 0, 0);
            if ((pivotPoint.localEulerAngles.x <= angleMax && moveDir < 0) || (pivotPoint.localEulerAngles.x >= angleMin && moveDir > 0))
            {
                pivotPoint.localEulerAngles += new Vector3(moveDir, 0, 0);
                Debug.Log(pivotPoint.localEulerAngles);
            }
        }
        if(pivotPoint.localEulerAngles.x <= angleMin)
        {
            pivotPoint.localEulerAngles = new Vector3(angleMin, 0, 0);
            isDragging = false;
        }
        if (pivotPoint.localEulerAngles.x >= angleMax || pivotPoint.localEulerAngles.y == 180)
        {
            pivotPoint.localEulerAngles = new Vector3(angleMax, 0, 0);
            isDragging = false;
        }
    }
}
