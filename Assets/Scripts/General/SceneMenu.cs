using UnityEngine;
using UnityEngine.InputSystem;

public class SceneMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActionAsset;

    private InputActionMap inputActionMap;
    public InputAction key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActionMap = inputActionAsset.FindActionMap("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
