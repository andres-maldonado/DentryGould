using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer fadeSprite, UIFadeSprite;
    [SerializeField] WorkCamControl workCamControl;
    [SerializeField] float fadeInTime;

    public bool isFadingIn, isFadingOut;
    private float currentFadeTime;
    private float currentAlpha;
    private int sceneToLoad;


    public delegate void StartScene();
    public StartScene startScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        fadeSprite = GameObject.Find("FadeSprite").GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (workCamControl != null)
        {
            workCamControl.LockControls(true);
        }
        FadeIntoScene();
    }
    public void FadeIntoScene()
    {
        isFadingIn = true;
        fadeSprite.color = new Color32(0, 0, 0, 255);
        currentFadeTime = 255 / fadeInTime;
        currentAlpha = 255;
    }
    public void FadeOutOfScene(float fadeTime, int scene)
    {
        if (workCamControl != null)
        {
            workCamControl.LockControls(true);
        }
        isFadingOut = true;
        fadeSprite.color = new Color32(0, 0, 0, 0);
        UIFadeSprite.color = new Color32(0, 0, 0, 0);
        currentFadeTime = 255 / fadeTime;
        currentAlpha = 0;
        sceneToLoad = scene;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFadingIn)
        {
            if (fadeSprite.color.a > 0)
            {
                currentAlpha -= currentFadeTime * Time.deltaTime;
                fadeSprite.color = new Color32(0, 0, 0, (byte)currentAlpha);
                UIFadeSprite.color = new Color32(0, 0, 0, (byte)currentAlpha);
            }
            else
            {
                isFadingIn = false;
                if (workCamControl != null)
                {
                    workCamControl.LockControls(false);
                }
                startScene.Invoke();
            }
        }
        if (isFadingOut)
        {
            if (fadeSprite.color.a < 1)
            {
                currentAlpha += currentFadeTime * Time.deltaTime;
                fadeSprite.color = new Color32(0, 0, 0, (byte)(255 - Mathf.Pow(currentAlpha - 255, 2)/255));
                UIFadeSprite.color = new Color32(0, 0, 0, (byte)(255 - Mathf.Pow(currentAlpha - 255, 2) / 255));
                if (currentAlpha >= 255)
                {
                    fadeSprite.color = new Color32(0, 0, 0, 255);
                    UIFadeSprite.color = new Color32(0, 0, 0, 255);
                }
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
                isFadingOut=false;
            }
        }
    }
}
