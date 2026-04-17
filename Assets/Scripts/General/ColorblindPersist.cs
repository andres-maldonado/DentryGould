using FMOD.Studio;
using System.Collections.Generic;
using UnityEngine;

public class ColorblindPersist : MonoBehaviour
{
    public delegate void ColorblindOn();
    public event ColorblindOn colorblindOn;
    public delegate void ColorblindOff();
    public event ColorblindOff colorblindOff;
    public bool cbOn;

    // private singleton instance
    private static ColorblindPersist _instance;

    // public accessor of instance
    public static ColorblindPersist ins
    {
        get
        {
            // setup SoundManager as a singleton class
            if (_instance == null)
            {
                // create new game manager object
                GameObject newManager = new();
                newManager.name = "ColorblindPersist";
                newManager.AddComponent<ColorblindPersist>();
                DontDestroyOnLoad(newManager);
                _instance = newManager.GetComponent<ColorblindPersist>();
            }
            // return new/existing instance
            return _instance;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void On()
    {
        colorblindOn.Invoke();
        cbOn = true;
    }
    public void Off()
    {
        colorblindOff.Invoke();
        cbOn = false;
    }
}
