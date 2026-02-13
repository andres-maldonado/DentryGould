using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    [SerializeField] Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        Vector3 targetPosition = hit.point;
        Vector3 direction = (hit.point - gameObject.transform.position).normalized;
        gameObject.transform.rotation = Quaternion.LookRotation(direction);
    }
}
