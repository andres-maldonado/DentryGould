using UnityEngine;

public class SetSongPositionOnStart : MonoBehaviour
{
    [SerializeField] string paramName;
    [SerializeField] int value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.ins.musicEventInstance.setParameterByName(paramName, value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
