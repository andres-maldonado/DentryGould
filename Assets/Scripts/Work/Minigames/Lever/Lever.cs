using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Lever : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform pivotPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] float leverMin, leverMax;
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
        DragLever();
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
        isHovering = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
    public void DragLever()
    {
        if(look.ReadValue<Vector2>().y > 0)
        {
            moveDir = moveSpeed;
        }
        else if(look.ReadValue<Vector2>().y < 0)
        {
            moveDir = -moveSpeed;
        }
        if (isDragging && !isHovering)
        {
            pivotPoint.localEulerAngles += new Vector3(moveDir, 0, 0);
        }
    }
}
