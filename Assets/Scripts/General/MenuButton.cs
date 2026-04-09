using UnityEngine;
using UnityEngine.InputSystem.Android;

public class MenuButton : MonoBehaviour
{
    [SerializeField] SceneManager sceneManager;
    [SerializeField] int sceneToLoad;
    [SerializeField] float fadeTime, flashRate;
    [SerializeField] int glowMin, glowMax;
    [SerializeField] SpriteRenderer glowControl;

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
        isFlashing = true;
        sceneManager.FadeOutOfScene(fadeTime, sceneToLoad, true);
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
