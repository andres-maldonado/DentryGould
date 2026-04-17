using TMPro;
using UnityEngine;

public class ColorblindText : MonoBehaviour
{
    private MeshRenderer text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        text = GetComponent<MeshRenderer>();
        ColorblindPersist.ins.colorblindOn += TurnOn;
        ColorblindPersist.ins.colorblindOff += TurnOff;
        text.enabled = ColorblindPersist.ins.cbOn;
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
        ColorblindPersist.ins.colorblindOff -= TurnOff;
        ColorblindPersist.ins.colorblindOn -= TurnOn;
    }
}
