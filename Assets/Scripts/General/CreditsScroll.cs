using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed, musicChangeHeight, stopHeight;
    private bool drumsOut, leaving;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, scrollSpeed, 0) * Time.deltaTime;
        if (transform.localPosition.y > musicChangeHeight && !drumsOut)
        {
            AudioManager.ins.musicEventInstance.setParameterByName("SongPosition", 3);
            drumsOut = true;
        }
        if (transform.localPosition.y > stopHeight && !leaving)
        {
            GameObject.Find("SceneManager").GetComponent<SceneManager>().FadeOutOfScene(5, 0, true);
            leaving = true;
        }
    }
}
