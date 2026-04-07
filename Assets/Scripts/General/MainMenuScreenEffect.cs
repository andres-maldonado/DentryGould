using UnityEngine;

public class MainMenuScreenEffect : MonoBehaviour
{
    [SerializeField] float vRange, hRange, fadeMin, fadeMax, changeRate;
    [SerializeField] SpriteRenderer glowControl;

    private float counter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < 0)
        {
            gameObject.transform.localPosition = new Vector3(Random.Range(-hRange, hRange), Random.Range(-vRange, vRange));
            glowControl.color = new Color32(0, 0, 0, (byte)Random.Range(fadeMin, fadeMax));
            counter = changeRate;
        }
        else
        {
            counter -= Time.deltaTime;
        }
    }
}
