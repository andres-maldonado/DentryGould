using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EveningText : MonoBehaviour
{
    public TextAsset dialogueFile;
    [SerializeField] float writeSpeed, pauseTime, commaTime, endTime, speedChangeTime, speedChangeRate, extraTime;
    public int sceneToLoad;
    [SerializeField] EventReference dialogueSound;
    [SerializeField] InputActionAsset inputAction;
    private EventInstance dialogueInstance;
    private TextMeshPro text;

    private string[] dialogueByLine;
    private Queue<string> dialogueLines = new Queue<string>();
    private int letterCount;
    private float counter;
    private float nextLineCounter;
    private string currentLine;
    private bool isWriting;
    private bool quickFinish;
    private bool isPaused;
    private bool isEnding;
    private InputActionMap inputMap;
    private InputAction continueKey;
    private void Awake()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().startScene += StartWriting;
        if (!dialogueSound.IsNull)
        {
            dialogueInstance = AudioManager.ins.CreateInstance(dialogueSound);
        }
        text = gameObject.GetComponent<TextMeshPro>();
        text.text = null;
        inputMap = inputAction.FindActionMap("Player");
        continueKey = inputMap.FindAction("Interact");
        continueKey.performed += _ => SpeedText();
    }
    void StartWriting()
    {
        Debug.Log("startWRiting");
        //dialogueByLine = dialogueFile.text.Split("\n");
        //for (int i = 0; i < dialogueByLine.Length; i++)
        {
            //Debug.Log(dialogueByLine[i]);
            //dialogueLines.Enqueue(dialogueByLine[i]);
        }
        //currentLine = dialogueLines.Dequeue();
        currentLine = dialogueFile.text;
        Debug.Log(currentLine);
        text.text = null;
        isWriting = true;
        dialogueInstance.start();
        WriteText();
    }
    private void WriteText()
    {
        //Debug.Log("LetterCount: " + letterCount + ", ReadingTag: " + readingTag +", isWriting: "+isWriting);
        counter += Time.deltaTime;
        //UnityEngine.Debug.Log("Counter: " + counter + ", Letters Displayed: " + letterCount);
        if (letterCount >= currentLine.Length)
        {
            letterCount = currentLine.Length;
            isWriting = false;
            EndWrite();
            //Debug.Log(newBoxText.GetRenderedValues(true));
        }
        else if (counter > 1 / writeSpeed || quickFinish)
        {
            text.text += currentLine.Substring(letterCount, 1);
            if (isPaused && !dialogueSound.IsNull)
            {
                dialogueInstance.setPaused(false);
            }
            letterCount++;
            counter = 0;
        }
        if (currentLine[letterCount] == '.' || currentLine[letterCount] == '?')
        {
            text.text += currentLine.Substring(letterCount, 1);
            letterCount++;
            if (!dialogueSound.IsNull)
            {
                dialogueInstance.setPaused(true);
                dialogueInstance.getPaused(out isPaused);
            }
            counter = (1 / writeSpeed) - pauseTime;
        }
        if (currentLine[letterCount] == ',')
        {
            text.text += currentLine.Substring(letterCount, 1);
            letterCount++;
            if (!dialogueSound.IsNull)
            {
                dialogueInstance.setPaused(true);
                dialogueInstance.getPaused(out isPaused);
            }
            counter = (1 / writeSpeed) - commaTime;
        }
        if (letterCount == speedChangeTime && speedChangeTime != 0)
        {
            writeSpeed = speedChangeRate;
        }
    }
    void EndWrite()
    {
        isEnding = true;
        counter = endTime;
    }
    void SpeedText()
    {
        if (isWriting)
        {
            quickFinish = true;
            counter += extraTime;
        }
        else
        {
            isEnding = true;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isWriting)
        {
            WriteText();
            Debug.Log(text.text);
        }
        if (isEnding)
        {
            Debug.Log("Counter: "+counter);
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                GameObject.Find("SceneManager").GetComponent<SceneManager>().FadeOutOfScene(2, sceneToLoad, false);
                isEnding = false;
            }
        }
    }
}
