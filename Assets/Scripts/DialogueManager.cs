using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
//using FMODUnity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialogueFile;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] float writeSpeed;
    [SerializeField] InputActionAsset inputAction;
    [SerializeField] SpriteRenderer leftSprite;
    [SerializeField] Sprite[] portraits;
    [SerializeField] GameObject portraitPrefab;
    private InputActionMap inputMap;
    private InputAction continueKey;

    private string[] dialogueByLine;
    private string currentLine;
    private string currentTag;
    private float counter;
    private int letterCount = 1;
    private int totalLetterCount;
    private bool isWriting;
    private bool readingTag;
    private bool quickFinish;
    private Queue<string> dialogueLines = new Queue<String>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textBox.text = null;
        inputMap = inputAction.FindActionMap("Player");
        continueKey = inputMap.FindAction("Interact");
        UnityEngine.Debug.Log(continueKey);
        continueKey.performed += _ => ContinueDialogue();
        continueKey.Enable();

        dialogueByLine = dialogueFile.text.Split("\n");
        for (int i = 0; i < dialogueByLine.Length; i++) 
        {
            //Debug.Log(dialogueByLine[i]);
            dialogueLines.Enqueue(dialogueByLine[i]);
        }
        ContinueDialogue();
    }
    /* public void Set(string fileName, Sprite speakerImage, int tpl = 25)
    {
        dialogueFile = fileName;
        speakerSprite = GameObject.Find("SpeakerPortrait").GetComponent<SpriteRenderer>();
        speakerSprite.sprite = speakerImage;
        writeSpeed = tpl;
    }*/

    public void WriteLine(int line)
    {

    }

    private void ContinueDialogue()
    {
        if (!isWriting)
        {
            textBox.text += "\n";
            currentLine = dialogueLines.Dequeue();
            letterCount = 0;
            UnityEngine.Debug.Log(currentLine[letterCount]);
            isWriting = true;
        }
        else {quickFinish = true;}
    }

    private void ReadCustomTag(string tag)
    {
        if (tag.StartsWith("P"))
        {
            tag = tag.Remove(0, 1);
            Debug.Log(tag);
            int portraitIndex = int.Parse(tag);
            GameObject newPortrait = Instantiate(portraitPrefab, leftSprite.transform.position, leftSprite.transform.rotation, textBox.transform);
            newPortrait.GetComponent<SpriteRenderer>().sprite = portraits[portraitIndex];
        }
    }

    private void WriteText()
    {
        counter += Time.deltaTime;
        //UnityEngine.Debug.Log("Counter: " + counter + ", Letters Displayed: " + letterCount);
        if (currentLine[letterCount] == '<')
        {
            readingTag = true;
            currentTag = null;
        }
        if (readingTag)
        {
            for (int i = letterCount; true;  i++)
            {
                currentTag += currentLine[i];
                Debug.Log(currentTag + ", " + i);
                Debug.Log(currentTag.Contains('>'));
                if (currentTag.Contains(">"))
                {
                    letterCount += currentTag.Length;
                    readingTag = false;
                    if (!currentTag.Contains("$"))
                    {
                        textBox.text += currentTag;
                    }
                    else
                    {
                        currentTag = currentTag.Remove(0, 2);
                        currentTag = currentTag.Remove(currentTag.Length - 1, 1);
                        Debug.Log(currentTag);
                        ReadCustomTag(currentTag);
                    }
                    break;
                }
            }
        }
        if ((counter > 1/writeSpeed && !readingTag) || currentLine[letterCount] == ' ')
        {
            textBox.text += currentLine.Substring(letterCount, 1);
            letterCount++;
            counter = 0;
        }
        if (letterCount >= currentLine.Length || quickFinish)
        {
            letterCount = currentLine.Length;
            isWriting = false;
            quickFinish = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWriting)
        {
            WriteText();
        }
    }
}
