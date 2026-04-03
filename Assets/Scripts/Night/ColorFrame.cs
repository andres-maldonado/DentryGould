using UnityEngine;

public class ColorFrame : MonoBehaviour
{
    [SerializeField] SpriteRenderer s;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void FrameColor(Color c)
    {
        s.color = c;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
