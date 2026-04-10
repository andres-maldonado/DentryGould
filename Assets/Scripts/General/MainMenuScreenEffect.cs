using UnityEngine;

public class MainMenuScreenEffect : MonoBehaviour
{
    [SerializeField] float vRange, hRange, fadeMin, fadeMax, changeRate;
    [SerializeField] SpriteRenderer glowControl;

    private float counter;
    private Vector3 startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < 0)
        {
            gameObject.transform.localPosition = startPos + new Vector3(Random.Range(-hRange, hRange), Random.Range(-vRange, vRange), 0);
            glowControl.color = new Color32(0, 0, 0, (byte)Random.Range(fadeMin, fadeMax));
            counter = changeRate;
        }
        else
        {
            counter -= Time.deltaTime;
        }
    }
}
