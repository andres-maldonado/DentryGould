using FMOD.Studio;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] GameObject menu;
    [SerializeField] bool canHide = true;

    private InputActionMap inputActionMap;
    private InputAction key;
    private WorkCamControl controls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActionMap = inputActionAsset.FindActionMap("Player");
        key = inputActionMap.FindAction("Tab");
        key.performed += MenuButton;
        controls = GameObject.Find("CameraRotate").GetComponent<WorkCamControl>();
    }
    private void OnDestroy()
    {
        key.performed -= MenuButton;
    }
    void MenuButton(InputAction.CallbackContext context)
    {
        TriggerMenu();
    }
    public void TriggerMenu()
    {
        if (canHide)
        {
            menu.active = !menu.active;
            PauseGame(menu.active);
        }
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
        foreach (EventInstance e in AudioManager.ins.eventInstances)
        {
            e.getDescription(out EventDescription description);
            description.getPath(out string result);
            //UnityEngine.Debug.Log(result);
            if (result != null && !result.EndsWith("Music"))
            {
                e.setPaused(paused);
            }
        }
        AudioManager.ins.musicEventInstance.setPaused(false);
        //text.SetActive(!paused);]
        Cursor.visible = paused; if (controls != null)
        {
            controls.LockControls(paused);
            Cursor.visible = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
