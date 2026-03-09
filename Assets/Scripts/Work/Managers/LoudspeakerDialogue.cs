using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.InputSystem;

public class LoudspeakerDialogue : MonoBehaviour
{
    public TextAsset startFile, successFile, failFile;
    [SerializeField] float writeSpeed, pauseTime, blankTime, continueDelay;
    [SerializeField] TextMeshPro bottomText;
    [SerializeField] DoorButton door;
    [SerializeField] EventReference dialogueSound, unlockSound;
    [SerializeField] InputActionAsset inputAction;
    [SerializeField] GameObject skipText;
    private InputActionMap inputMap;
    private InputAction continueKey;


    private string[] dialogueByLine;
    private Queue<string> dialogueLines = new Queue<string>();
    private int letterCount;
    private float counter;
    private float nextLineCounter;
    private float continueCounter;
    private string currentLine;
    private bool isWriting;
    private bool quickFinish;
    private bool isYapping;
    private bool canSkip;
    private bool isPaused;
    private bool waitingToYap;
    private EventInstance dialogueInstance;
    public int success;

    public delegate void EndWriting();
    public EndWriting endWriting;
    private void Awake()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().startScene += FirstDialogue;
        bottomText.text = null;
        dialogueInstance = AudioManager.ins.CreateInstance(dialogueSound);
        inputMap = inputAction.FindActionMap("Player");
        continueKey = inputMap.FindAction("Interact");
        continueKey.performed += _ => ContinueDialogue();
    }
    void FirstDialogue()
    {
        StartWriting(startFile);
    }
    public void SuccessDialogue(float timer)
    {
        counter = (1 / writeSpeed) - timer;
        StartWriting(successFile);
    }
    public void FailDialogue(float timer)
    {
        counter = (1 / writeSpeed) - timer;
        StartWriting(failFile);
    }
    public void ContinueDialogue()
    {
        if (continueCounter < 0)
        {
            quickFinish = true;
        }
        continueCounter = continueDelay;
    }
    public void StartWriting(TextAsset file)
    {
        dialogueByLine = file.text.Split("\n");
        for (int i = 0; i < dialogueByLine.Length; i++)
        {
            //Debug.Log(dialogueByLine[i]);
            dialogueLines.Enqueue(dialogueByLine[i]);
        }
        currentLine = dialogueLines.Dequeue();
        letterCount = 0;
        bottomText.text = null;
        isWriting = true;
        quickFinish = false;
        skipText.SetActive(true);
        waitingToYap = true;
        WriteText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Counter: " + counter);
        if(isWriting)
        {
            WriteText();
            /*if (!isYapping && counter >= 0)
            {
                dialogueInstance.start();
                isYapping = true;
            }*/
        }
        if (nextLineCounter > 0 && isPaused)
        {
            PauseTime();
            /*if (isYapping)
            {
                dialogueInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isYapping = false;
            }*/
        }
        if (continueCounter >= 0)
        {
            continueCounter -= Time.deltaTime;
        }
        if (waitingToYap)
        {
            if(counter >= 0)
            {
                dialogueInstance.start();
                isYapping = true;
                waitingToYap = false;
            }
        }
    }
    private void WriteText()
    {
        //Debug.Log("LetterCount: " + letterCount + ", ReadingTag: " + readingTag +", isWriting: "+isWriting);
        counter += Time.deltaTime;
        //UnityEngine.Debug.Log("Counter: " + counter + ", Letters Displayed: " + letterCount);
        if (letterCount >= currentLine.Length || quickFinish)
        {
            bottomText.text = currentLine;
            letterCount = currentLine.Length;
            isWriting = false;
            quickFinish = false;
            nextLineCounter = pauseTime * currentLine.Length;
            isPaused = true;
            canSkip = true;

            //Debug.Log("YOMAMA");
            //Debug.Log(newBoxText.GetRenderedValues(true));
        }
        else if ((counter > 1 / writeSpeed) || currentLine[letterCount] == ' ')
        {
            bottomText.text += currentLine.Substring(letterCount, 1);
            letterCount++;
            counter = 0;
        }
    }
    private void PauseTime()
    {
        nextLineCounter -= Time.deltaTime;
        if (isYapping && isPaused)
        {
            dialogueInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            isYapping = false;
        }
        if (nextLineCounter <= 0 || (quickFinish && !isWriting) && canSkip)
        {
            bottomText.text = null;
            quickFinish = false;
            isPaused = false;
            StartCoroutine(BlankTime());
            canSkip = false;
        }
    }
    IEnumerator BlankTime()
    {
        Debug.Log("Dialogue Lines Left: "+dialogueLines.Count);
        if (dialogueLines.Count > 0 )
        {
            yield return new WaitForSeconds(blankTime);
            currentLine = dialogueLines.Dequeue();
            bottomText.text = null;
            isWriting = true;
            letterCount = 0;
            dialogueInstance.start();
            isYapping = true;
        }
        else
        {
            isWriting = false;
            endWriting.Invoke();
            skipText.SetActive(false);
            if (success == 1)
            {
                door.canExit = true;
                AudioManager.ins.PlayOneShot(unlockSound, this.gameObject.transform.position);
            }
            else if (success == 2)
            {
                GameObject.Find("SceneManager").GetComponent<SceneManager>().FadeOutOfScene(2, UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
            Debug.Log("end");
        }
    }
}