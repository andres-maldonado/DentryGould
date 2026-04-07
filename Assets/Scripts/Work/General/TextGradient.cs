using UnityEngine;

public class TextGradient : MonoBehaviour
{
    [SerializeField] float fadeTime, opacity;

    private bool isFadingIn, isFadingOut;
    private float currentOpacity;
    private SpriteRenderer sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingIn)
        {
            currentOpacity += opacity * Time.deltaTime / fadeTime;
        }
        else if (isFadingOut)
        {
            currentOpacity -= opacity * Time.deltaTime / fadeTime;
        }
        sprite.color = new Color32(0, 0, 0, (byte)currentOpacity);
        if (currentOpacity <= 0)
        {
            sprite.color = new Color32(0, 0, 0, 0);
            isFadingOut = false;
        }
        else if (currentOpacity >= opacity)
        {
            sprite.color = new Color32(0, 0, 0, (byte)opacity);
            isFadingIn = false;
        }
    }
    public void FadeIn()
    {
        sprite.color = new Color32(0, 0, 0, 0);
        isFadingIn = true;
    }
    public void FadeOut()
    {
        sprite.color = new Color32(0, 0, 0, (byte)opacity);
        isFadingOut = true;
    }
}
