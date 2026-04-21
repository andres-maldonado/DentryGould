using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class MusicManager : MonoBehaviour
{
    [SerializeField] EventReference startMusic;

    private bool waitingToReplace;
    private EventReference replaceTrack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        AudioManager.ins.musicEventInstance.setParameterByName("MusicPosition", 0);
    }
    void Start()
    {
        AudioManager.ins.musicEventInstance.getDescription(out EventDescription musicDesc);
        musicDesc.getPath(out string musicPath);
        if (musicPath != startMusic.Path)
        {
            StartMusic();
        }
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.ins.musicEventInstance.getPlaybackState(out PLAYBACK_STATE state);
        if (waitingToReplace && state == PLAYBACK_STATE.STOPPED)
        {
            AudioManager.ins.InitializeMusic(replaceTrack);
            waitingToReplace = false;
        }
    }
    public void StartMusic()
    {
        AudioManager.ins.InitializeMusic(startMusic);
    }
    public void ReplaceMusic(EventReference newMusic)
    {
        AudioManager.ins.musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        replaceTrack = newMusic;
        waitingToReplace = true;
    }
}
