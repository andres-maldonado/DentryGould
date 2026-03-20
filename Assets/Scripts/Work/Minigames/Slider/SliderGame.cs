using UnityEngine;
using UnityEngine.UI;

public class SliderGame : Minigame
{
    [Header("Slider Game Stuff")]
    public Transform vSliderCube;
    public Transform hSliderCube, dot;
    [SerializeField] Slider vSlider, hSlider;
    [SerializeField] int divisions;

    private Vector2 entryVect, answerVect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Disable()
    {
        throw new System.NotImplementedException();
    }
    public override void Enable()
    {
        throw new System.NotImplementedException();
    }

    public override void Randomize(Transform p)
    {
        answerVect = new Vector2(Random.Range(0, divisions)/divisions, Random.Range(0, divisions) / divisions);
        entryVect = null;
    }
    public override void ResetGame()
    {
        throw new System.NotImplementedException();
    }
    public void UpdateDot()
    {
        dot.localPosition = new Vector3(vSliderCube.localPosition.x, hSliderCube.localPosition.x-hSliderCube.parent.parent.position.x, 0);
        entryVect = new Vector2(hSlider.value, 1 - vSlider.value);
        Debug.Log(entryVect);
    }
}
