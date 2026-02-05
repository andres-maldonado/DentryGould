using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Lever : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform pivotPoint;
<<<<<<< Updated upstream
    [SerializeField] float moveSpeed;
    [SerializeField] float leverMin, leverMax;
=======
    [SerializeField] float moveSpeed, angleMin, angleMax;

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        DragLever();
=======
        if (isDragging && !isHovering)
        {
            DragLever();
        }
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    }
    public void DragLever()
    {
        if(look.ReadValue<Vector2>().y > 0)
        {
            moveDir = moveSpeed;
        }
        else if(look.ReadValue<Vector2>().y < 0)
=======
        if (look.ReadValue<Vector2>().y > 0)
>>>>>>> Stashed changes
        {
            moveDir = -moveSpeed;
        }
        else if (look.ReadValue<Vector2>().y < 0)
        {
            moveDir = moveSpeed;
        }
    }
    public void DragLever()
    {
        if (isDragging && !isHovering)
        {
<<<<<<< Updated upstream
            pivotPoint.localEulerAngles += new Vector3(moveDir, 0, 0);
=======
            if ((pivotPoint.localEulerAngles.x <= angleMax && moveDir < 0) || (pivotPoint.localEulerAngles.x >= angleMin && moveDir > 0))
            {
                pivotPoint.eulerAngles += new Vector3(moveDir, 0, 0);
                Debug.Log(pivotPoint.localRotation);
            }
        }
        if(pivotPoint.localEulerAngles.x <= angleMin)
        {
            pivotPoint.localEulerAngles = new Vector3(angleMin, 0, 0);
            isDragging = false;
        }
        if (pivotPoint.localEulerAngles.x >= angleMax)
        {
            pivotPoint.localEulerAngles = new Vector3(angleMax, 0, 0);
            isDragging = false;
>>>>>>> Stashed changes
        }
    }
}
