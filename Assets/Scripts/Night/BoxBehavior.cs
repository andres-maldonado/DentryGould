using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BoxBehavior : MonoBehaviour
{
    private DialogueManager manager;
    [SerializeField] Animator portraitAnim;
    private float newHeight;
    private float moveDistance;
    private float moveSpeed;
    private bool isMoving;
    private bool isMain = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FindPortrait());
        manager = GameObject.FindWithTag("DialogueManager").GetComponent<DialogueManager>();
        manager.moveBoxesUp += MoveUp;
        moveDistance = manager.moveDistance;
        moveSpeed = manager.moveSpeed;
    }

    IEnumerator FindPortrait()
    {
        yield return new WaitForSeconds(.25f);
        portraitAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("isMoving: " + isMoving);
        if (isMoving)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, newHeight, moveSpeed));
            if (newHeight - transform.position.y < 1)
            {
                transform.localPosition = new Vector2 (transform.localPosition.x, newHeight);
                //Debug.Log("done moving");
                isMoving = false;
            }
            
        }
    }

    public void MoveUp()
    {
        if (isMain && portraitAnim != null)
        {
            portraitAnim.SetBool("isMain", false);
            isMain = false;
        }
        newHeight = transform.localPosition.y + moveDistance;
        isMoving = true;
    }
}
