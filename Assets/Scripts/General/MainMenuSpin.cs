using UnityEngine;

public class MainMenuSpin : MonoBehaviour
{
    [SerializeField] float speed;

    Transform spin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spin = GetComponent<Transform>();
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        spin.Rotate(0, speed*Time.deltaTime, 0);
    }
}
