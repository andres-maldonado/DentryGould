using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;
using FMODUnity;

public class SceneManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer fadeSprite, UIFadeSprite;
    [SerializeField] float fadeInTime;
    [SerializeField] bool isWorkScene;

    public bool isFadingIn, isFadingOut;
    private float currentFadeTime;
    private float currentAlpha;
    private int sceneToLoad;
    private bool isFadingSound;
    private WorkCamControl workCamControl;
    private PARAMETER_ID paramId;
    [SerializeField] PARAMETER_DESCRIPTION paramDesc;


    public delegate void StartScene();
    public StartScene startScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        fadeSprite = GameObject.Find("FadeSprite").GetComponent<SpriteRenderer>();
        UIFadeSprite = GameObject.Find("UIFadeSprite").GetComponent<SpriteRenderer>();
        FMODUnity.RuntimeManager.StudioSystem.getParameterDescriptionByName("GameFader", out paramDesc);
        paramId = paramDesc.id;
        if (isWorkScene)
        {
            workCamControl = GameObject.Find("CameraRotate").GetComponent<WorkCamControl>();
        }
    }
    void Start()
    {
        if (workCamControl != null)
        {
            workCamControl.LockControls(true);
            workCamControl.SetTicketEnabled(false);
        }
        float gameFaderValue;
        RuntimeManager.StudioSystem.getParameterByID(paramId, out gameFaderValue);
        if (gameFaderValue != 1)
        {
            gameFaderValue = 1;
        }
        FadeIntoScene();
    }
    public void FadeIntoScene()
    {
        isFadingIn = true;
        fadeSprite.color = new Color32(0, 0, 0, 255);
        currentFadeTime = 255 / fadeInTime;
        currentAlpha = 255;
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(paramId, 1.0f);
    }
    public void FadeOutOfScene(float fadeTime, int scene, bool fadeSound)
    {
        if (workCamControl != null)
        {
            workCamControl.LockControls(true);
        }
        isFadingOut = true;
        fadeSprite.color = new Color32(0, 0, 0, 0);
        UIFadeSprite.color = new Color32(0, 0, 0, 0);
        currentFadeTime = 255 / fadeTime;
        isFadingSound = fadeSound;
        currentAlpha = 0;
        sceneToLoad = scene;
        Debug.Log("Told to fade, sound = " + fadeSound);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFadingIn)
        {
            if (fadeSprite.color.a > 0)
            {
                currentAlpha -= currentFadeTime * Time.deltaTime;
                fadeSprite.color = new Color32(0, 0, 0, (byte)(255 * Mathf.Pow(currentAlpha / 255, 1)));
                UIFadeSprite.color = new Color32(0, 0, 0, (byte)(255*Mathf.Pow(currentAlpha/255, 1)));
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
                if (isFadingSound)
                {
                    RuntimeManager.StudioSystem.setParameterByID(paramId, 1 - currentAlpha/255);
                    //Debug.Log(1 - currentAlpha/255);
                }
                if (currentAlpha >= 255)
                {
                    fadeSprite.color = new Color32(0, 0, 0, 255);
                    UIFadeSprite.color = new Color32(0, 0, 0, 255);
                }
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
                if (isFadingSound)
                {
                    AudioManager.ins.CleanUp();
                }
                isFadingOut=false;
            }
        }
    }
}
