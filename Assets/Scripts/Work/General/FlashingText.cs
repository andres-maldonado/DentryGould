using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    [SerializeField] float flashRate;
    [SerializeField] float maxBright;
    [SerializeField] float minBright;
    [SerializeField] float hShift, vShift;
    [SerializeField] SpriteRenderer glowControl;

    private TextMeshPro text;
    private float counter, hCenter, vCenter;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        counter = flashRate;
        hCenter = gameObject.transform.localPosition.x;
        vCenter = gameObject.transform.localPosition.y;
        originalColor = text.color;
    }

    private void Flash()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            float currentBright = Random.Range(minBright, maxBright);
            float horizontal = Random.Range(hCenter - hShift, hCenter + hShift);
            float vertical = Random.Range(vCenter - vShift, vCenter + vShift);
            glowControl.color = new Color32(0, 0, 0, (byte)(255-currentBright));
            text.ForceMeshUpdate(true);
            transform.localPosition = new Vector3(horizontal, vertical, 0);
            counter = flashRate;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Flash();
    }
}
