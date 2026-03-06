using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using FMODUnity;
using FMOD.Studio;

public class LoudspeakerDialogue : MonoBehaviour
{
    public TextAsset startFile, successFile, failFile;
    [SerializeField] float writeSpeed, pauseTime, blankTime;
    [SerializeField] TextMeshPro bottomText;
    [SerializeField] DoorButton door;
    [SerializeField] EventReference dialogueSound;

    private string[] dialogueByLine;
    private Queue<string> dialogueLines = new Queue<string>();
    private int letterCount;
    private float counter;
    private float nextLineCounter;
    private string currentLine;
    private bool isWriting;
    private bool quickFinish;
    private bool isYapping;
    private EventInstance dialogueInstance;
    public int success;

    public delegate void EndWriting();
    public EndWriting endWriting;
    private void Awake()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManager>().startScene += FirstDialogue;
        bottomText.text = null;
        dialogueInstance = AudioManager.ins.CreateInstance(dialogueSound);
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
        WriteText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isWriting)
        {
            WriteText();
            if (!isYapping && counter >= 0)
            {
                dialogueInstance.start();
                isYapping = true;
            }
        }
        if (nextLineCounter > 0)
        {
            PauseTime();
            if (isYapping)
            {
                dialogueInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isYapping = false;
            }
        }
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
            nextLineCounter = pauseTime * currentLine.Length;

            //Debug.Log("YOMAMA");
            //Debug.Log(newBoxText.GetRenderedValues(true));
        }
        else if ((counter > 1 / writeSpeed) || currentLine[letterCount] == ' ' || quickFinish)
        {
            bottomText.text += currentLine.Substring(letterCount, 1);
            letterCount++;
            counter = 0;
        }
    }
    private void PauseTime()
    {
        nextLineCounter -= Time.deltaTime;
        if(nextLineCounter <= 0)
        {
            bottomText.text = null;
            StartCoroutine(BlankTime());
        }
    }
    IEnumerator BlankTime()
    {
        Debug.Log("Dialogue Lines Left: "+dialogueLines.Count);
        if (dialogueLines.Count > 0 )
        {
            yield return new WaitForSeconds(blankTime);
            currentLine = dialogueLines.Dequeue();

            isWriting = true;
            letterCount = 0;
        }
        else
        {
            isWriting = false;
            endWriting.Invoke();
            if (success == 1)
            {
                door.canExit = true;
            }
            else if (success == 2)
            {
                GameObject.Find("SceneManager").GetComponent<SceneManager>().FadeOutOfScene(2, UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
            Debug.Log("end");
        }
    }
}
