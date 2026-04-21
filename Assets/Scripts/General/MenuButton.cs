using UnityEngine;
using UnityEngine.InputSystem;

public class MenuButton : MonoBehaviour
{
    [SerializeField] SceneManager sceneManager;
    [SerializeField] int sceneToLoad;
    [SerializeField] float fadeTime, flashRate;
    [SerializeField] int glowMin, glowMax;
    [SerializeField] SpriteRenderer glowControl;
    [SerializeField] bool spareSound;

    private float counter;
    private bool isFlashing, isCurrentlyMin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlashing)
        {
            FlashButton();
        }   
    }
    public void LoadScene()
    {
        if (!sceneManager.isFadingOut)
        {
            isFlashing = true;
            sceneManager.FadeOutOfScene(fadeTime, sceneToLoad, !spareSound);
        }
    }
    void FlashButton()
    {
        counter -= Time.deltaTime;
        if (counter < 0)
        {
            if (!isCurrentlyMin)
            {
                glowControl.color = new Color32(0, 0, 0, (byte)(glowMin));
                isCurrentlyMin = true;
            }
            else if (isCurrentlyMin)
            {
                glowControl.color = new Color32(0, 0, 0, (byte)(glowMax));
                isCurrentlyMin = false;
            }
            counter = flashRate;
        }
    }
}
