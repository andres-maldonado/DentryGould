using UnityEngine;

public abstract class Answer : MonoBehaviour
{
    [Header("General Answer")]
    public SpriteRenderer icon;
    public GameObject check;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        check.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void DisplayAnswer(Sprite sprite, int number, Color color, string answer);
    public void Complete()
    {
        check.SetActive(true);
    }
}
