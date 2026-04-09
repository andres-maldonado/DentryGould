using UnityEngine;
using UnityEngine.InputSystem;

public class SceneMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] GameObject menu, text;

    private InputActionMap inputActionMap;
    private InputAction key;
    private WorkCamControl controls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActionMap = inputActionAsset.FindActionMap("Player");
        key = inputActionMap.FindAction("Tab");
        key.performed += _ => TriggerMenu();
        controls = GameObject.Find("CameraRotate").GetComponent<WorkCamControl>();
    }
    void TriggerMenu()
    {
        menu.active = !menu.active;
        PauseGame(menu.active);
    }
    void PauseGame(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        text.SetActive(!paused);
        if (controls != null)
        {
            controls.LockControls(paused);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
