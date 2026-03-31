using UnityEngine;

public class GaugeButton : WorkButton
{
    [SerializeField] GaugeGame game;
    [SerializeField] bool isUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClick()
    {
        //if (game.active)
        {
            game.UpdateTarget(isUp);
        }
    }
}
