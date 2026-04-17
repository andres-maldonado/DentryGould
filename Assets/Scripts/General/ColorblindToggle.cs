using UnityEngine;
using UnityEngine.UI;

public class ColorblindToggle : MonoBehaviour
{
    private Toggle toggle;
    private ColorblindPersist persist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        persist = GameObject.Find("ColorblindToggle").GetComponent<ColorblindPersist>();
    }
    void Start()
    {
        toggle = GetComponent<Toggle>();
        SetColorblind();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetColorblind()
    {
        if (toggle.isOn)
        {
            //Debug.Log("Ran On");
            persist.On();
        }
        else 
        {
            persist.Off();
            //Debug.Log("Ran Off");
        }
    }
    void Test()
    {
        //Debug.Log("ok I started");
    }
}
