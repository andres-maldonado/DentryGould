using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLamp : MonoBehaviour
{
    public enum eColor
    {
            Red,
            Yellow,
            Green,
            Blue,
    }

    public bool on;
    public Transform lamp;
    public eColor lightColor;
    public float fadeAmount;

    MeshRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = lamp.GetComponent<MeshRenderer>();
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetColor()
    {
        if (on)
        {
            switch (lightColor)
            {
                case eColor.Red:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0f, 0.02f, 1f));
                    break;
                case eColor.Yellow:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0.65f, 0f, 1f));
                    break;
                case eColor.Green:
                    rend.material.SetColor("_EmissionColor", new Color(0.15f, 1f, 0f, 1f));
                    break;
                case eColor.Blue:
                    rend.material.SetColor("_EmissionColor", new Color(0f, 0.33f, 1f, 1f));
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (lightColor)
            {
                case eColor.Red:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0f, 0.02f, 1f) * fadeAmount);
                    break;
                case eColor.Yellow:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0.65f, 0f, 1f) * fadeAmount);
                    break;
                case eColor.Green:
                    rend.material.SetColor("_EmissionColor", new Color(0.15f, 1f, 0f, 1f) * fadeAmount);
                    break;
                case eColor.Blue:
                    rend.material.SetColor("_EmissionColor", new Color(0f, 0.33f, 1f, 1f) * fadeAmount);
                    break;
                default:
                    break;
            }
        }
    }
    public void Run()
    {
        int color = Random.Range(0, 4);
        switch (color)
        {
            case 0:
                lightColor = eColor.Red;
                break;
            case 1:
                lightColor = eColor.Yellow;
                break;
            case 2:
                lightColor = eColor.Green;
                break;
            case 3:
                lightColor = eColor.Blue;
                break;
        }
        SetColor();
    }
}
