using System.Collections;
using UnityEngine;

public class WorkLightFlicker : MonoBehaviour
{
    [SerializeField] float floor, flashRate, differential, downTime, smoothPercent;
    public bool isLive;

    private Light light;
    private float normalIntensity, counter, currentTime;
    private bool isSmooth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light = GetComponent<Light>();
        normalIntensity = light.intensity;
    }
    public void Flicker(bool isSmoothNow, float time, float intensity)
    {
        light.intensity /= Random.Range(1, intensity);
        if (isSmoothNow)
        {
            isSmooth = true;
            currentTime = time;
            Debug.Log("smooth FLASH!");
        }
        else
        {
            StartCoroutine(FlickerAnim(time));
            Debug.Log("FLASH!");
        }
    }
    IEnumerator FlickerAnim(float time)
    {
        yield return new WaitForSeconds(time);
        light.intensity = normalIntensity;
    }
    void SmoothFlickerAnim()
    {
        if (light.intensity < normalIntensity)
        {
            light.intensity += (normalIntensity * (1 - 1 / differential)) / 2 / currentTime * Time.deltaTime;
        }
        else
        {
            light.intensity = normalIntensity;
            isSmooth = false;
        }
    }
    private void CountUpToFlicker()
    {
        float random = Random.Range(0f, 1f);
        counter += Time.deltaTime / flashRate;
        if (counter > random)
        {
            if (Random.Range(0f, 1f) > smoothPercent)
            {
                Flicker(false, downTime, differential);
            }
            else
            {
                Flicker(true, downTime, differential);
            }
            counter = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            CountUpToFlicker();
        }
        if (isSmooth)
        {
            SmoothFlickerAnim();
        }
        if (light.intensity < floor)
        {
            light.intensity = floor + Random.Range(0f, .03f);
        }
    }
}
