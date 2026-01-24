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
    public float moveDistance;
    public float moveSpeed;
    [SerializeField] InputActionAsset inputAction;
    [SerializeField] SpriteRenderer leftSprite;
    [SerializeField] SpriteRenderer rightSprite;
    [SerializeField] Sprite[] portraits;
    [SerializeField] GameObject portraitPrefab;
    [SerializeField] GameObject lineBox;
    private GameObject content;
    private Transform textSpawn;
    private InputActionMap inputMap;
    private InputAction continueKey;

    public List<GameObject> textBoxes = new List<GameObject>();
    private GameObject newBox;
    private TextMeshProUGUI newBoxText;
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

    public delegate void MoveBoxesUp();
    public MoveBoxesUp moveBoxesUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newBoxText = null;
        inputMap = inputAction.FindActionMap("Player");
        continueKey = inputMap.FindAction("Interact");
        UnityEngine.Debug.Log(continueKey);
        continueKey.performed += _ => ContinueDialogue();
        continueKey.Enable();
        textSpawn = GameObject.Find("TextSpawn").GetComponent<Transform>();
        content = GameObject.Find("Content");

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
        Debug.Log("isWriting: " + isWriting);
        if (!isWriting)
        {
            //textBox.text += "\n"; (if one box)
            /*foreach (GameObject obj in textBoxes)
            {
                if(obj != null)
                {
                    GetComponent<BoxBehavior>().MoveUp();
                }
                else { Debug.Log("shits null ig"); }
            }*/
            if (moveBoxesUp != null)
            {
                moveBoxesUp.Invoke();
            }
            content.GetComponent<RectTransform>().sizeDelta += new Vector2(0, moveDistance);
            newBox = Instantiate(lineBox, textSpawn.transform.position, textSpawn.transform.rotation, content.transform);
            newBoxText = newBox.GetComponentInChildren<TextMeshProUGUI>();
            textBoxes.Add(newBox);
            currentLine = dialogueLines.Dequeue();
            letterCount = 0;
            //UnityEngine.Debug.Log(currentLine[letterCount]);
            isWriting = true;
        }
        else {quickFinish = true;}
    }

    private void ReadCustomTag(string tag)
    {
        if (tag.StartsWith("P"))
        {
            tag = tag.Remove(0, 1);
            if (tag.StartsWith("L"))
            {
                tag = tag.Remove(0, 1);
                Debug.Log(tag);
                int portraitIndex = int.Parse(tag);
                GameObject newPortrait = Instantiate(portraitPrefab, leftSprite.transform.position, leftSprite.transform.rotation, newBox.transform);
                newPortrait.GetComponent<SpriteRenderer>().sprite = portraits[portraitIndex];
            }
            else if (tag.StartsWith("R"))
            {
                tag = tag.Remove(0, 1);
                Debug.Log(tag);
                int portraitIndex = int.Parse(tag);
                GameObject newPortrait = Instantiate(portraitPrefab, rightSprite.transform.position, rightSprite.transform.rotation, newBox.transform);
                newPortrait.GetComponent<SpriteRenderer>().sprite = portraits[portraitIndex];
            }
        }
    }

    private void WriteText()
    {
        //Debug.Log("Current Line Length: " + currentLine.Length + ", Letter Count: " + letterCount);
        //Debug.Log("LetterCount: " + letterCount + ", ReadingTag: " + readingTag +", isWriting: "+isWriting);
        counter += Time.deltaTime;
        //UnityEngine.Debug.Log("Counter: " + counter + ", Letters Displayed: " + letterCount);
        if (letterCount >= currentLine.Length)
        {
            letterCount = currentLine.Length;
            isWriting = false;
            quickFinish = false;
            Debug.Log("YOMAMA");
        }
        else
        {
            if (currentLine[letterCount] == '<')
            {
                readingTag = true;
                currentTag = null;
            }
            if (readingTag)
            {
                for (int i = letterCount; true; i++)
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
                            newBoxText.text += currentTag;
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

            if ((counter > 1 / writeSpeed && !readingTag) || currentLine[letterCount] == ' ' || quickFinish)
            {
                newBoxText.text += currentLine.Substring(letterCount, 1);
                letterCount++;
                counter = 0;
            }
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
