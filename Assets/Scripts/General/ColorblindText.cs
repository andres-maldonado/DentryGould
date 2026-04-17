using TMPro;
using UnityEngine;

public class ColorblindText : MonoBehaviour
{
    private MeshRenderer text;
    private ColorblindPersist persist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<MeshRenderer>();
        persist = GameObject.Find("ColorblindToggle").GetComponent<ColorblindPersist>();
        persist.colorblindOn += TurnOn;
        persist.colorblindOff += TurnOff;
        text.enabled = persist.cbOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TurnOn()
    {
        text.enabled = true;
    }
    private void TurnOff()
    {
        text.enabled = false;
    }
    private void OnDestroy()
    {
        GameObject.Find("ColorblindToggle").GetComponent<ColorblindPersist>().colorblindOff -= TurnOff;
        GameObject.Find("ColorblindToggle").GetComponent<ColorblindPersist>().colorblindOn -= TurnOn;
    }
}
