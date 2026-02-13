using FMODUnity;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TaskLight : MonoBehaviour
{
    public enum eColor
    {
        Red,
        Yellow,
        Green,
        Blue,
    }

    public bool on;
    public float fadeAmount;

    private EventReference completionSound;
    private float flashRate;
    private float counter;
    private bool isComplete;

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        flashRate = Random.value * .4f + .6f;
        completionSound = RuntimeManager.PathToEventReference("event:/SFX/Work/TaskComplete");
    }
    public void SetCompletion(bool state)
    {
        isComplete = state;
        if (!isComplete)
        {
            flashRate = Random.value * .4f + .6f;
            counter = flashRate;
        }
        else if (isComplete)
        {
            SetColor(eColor.Green);
            AudioManager.ins.PlayOneShot(completionSound, transform.position);
        }
    }
    void FlashRed()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            on = !on;
            SetColor(eColor.Red);
            counter = flashRate;
        }
    }
    void FixedUpdate()
    {
        if (!isComplete)
        {
            FlashRed();
        }
        else if (isComplete)
        {
            SetColor(eColor.Green);
            on = true;
        }
    }
    public void SetColor(eColor lightColor)
    {
        if (on)
        {
            switch (lightColor)
            {
                case eColor.Green:
                    rend.material.SetColor("_EmissionColor", new Color(0.15f, 1f, 0f, 1f));
                    break;
                case eColor.Red:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0f, 0.02f, 1f));
                    break;
                case eColor.Yellow:
                    rend.material.SetColor("_EmissionColor", new Color(1f, 0.65f, 0f, 1f));
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
}
