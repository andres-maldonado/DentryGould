using UnityEngine;

public class ColorFrame : MonoBehaviour
{
    [SerializeField] SpriteRenderer s;
    [SerializeField] float fadeAmount;
    [SerializeField] float fadeDuration;

    private float counter = 10;
    private Color currentColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void FrameColor(Color c)
    {
        s.color = c;
    }
    public void FadeFrame() 
    {
        counter = 0;
        currentColor = s.color;
    
    }
    // Update is called once per frame
    void Update()
    {
        if (counter < 1)
        {
            s.color = new Color (currentColor.r * Mathf.Lerp(1, fadeAmount, counter), currentColor.g * Mathf.Lerp(1, fadeAmount, counter), currentColor.b * Mathf.Lerp(1, fadeAmount, counter), 1);
            counter += fadeDuration / 60 * Time.deltaTime;
        }
    }
}
