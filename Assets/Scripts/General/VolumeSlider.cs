using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] string paramName;
    [SerializeField] EventReference testSound;

    private Slider slider;
    private bool setByMenu = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        slider = GetComponent<Slider>();
        RuntimeManager.StudioSystem.getParameterByName(paramName, out float value);
        slider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayTestSound()
    {
        if (!setByMenu)
        {
            AudioManager.ins.PlayOneShot(testSound, this.transform.position);
        }
        else
        {
            setByMenu = false;
        }
    }
    public void SetVolume()
    {
        AudioManager.ins.SetGlobalParameter(paramName, slider.value);
    }
}
