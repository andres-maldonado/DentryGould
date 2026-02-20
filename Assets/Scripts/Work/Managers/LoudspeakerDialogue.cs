using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class LoudspeakerDialogue : MonoBehaviour
{
    [SerializeField] TextAsset dialogueFile;
    [SerializeField] float writeSpeed, pauseTime, blankTime;
    [SerializeField] TextMeshPro bottomText;

    private string[] dialogueByLine;
    private Queue<string> dialogueLines = new Queue<string>();
    private int letterCount;
    private float counter;
    private float nextLineCounter;
    private string currentLine;
    private bool isWriting;
    private bool quickFinish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueByLine = dialogueFile.text.Split("\n");
        for (int i = 0; i < dialogueByLine.Length; i++)
        {
            //Debug.Log(dialogueByLine[i]);
            dialogueLines.Enqueue(dialogueByLine[i]);
        }
        currentLine = dialogueLines.Dequeue();
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
        }
        if (nextLineCounter > 0)
        {
            PauseTime();
        }

    }
    private void WriteText()
    {
        Debug.Log("Current Line Length: " + currentLine.Length + ", Letter Count: " + letterCount);
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
            Debug.Log("added "+currentLine.Substring(letterCount, 1));
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
        yield return new WaitForSeconds(blankTime);
        currentLine = dialogueLines.Dequeue();
        isWriting = true;
        letterCount = 0;
        Debug.Log("Start writing again: " + currentLine);
    }
}
