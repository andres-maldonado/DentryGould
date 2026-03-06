using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using FMODUnity;
using FMOD.Studio;
//using FMODUnity;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialogueFile;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] float writeSpeed;
    public float textGap;
    public float moveSpeed, commaTime, pauseTime;
    public float fadeTime;
    [SerializeField] InputActionAsset inputAction;
    [SerializeField] Transform leftSprite;
    [SerializeField] Transform rightSprite;
    [SerializeField] RuntimeAnimatorController portraitSizer;
    [SerializeField] RuntimeAnimatorController[] portraits;
    [SerializeField] EventReference[] voices;
    [SerializeField] GameObject portraitPrefab;
    [SerializeField] GameObject lineBox;
    [SerializeField] SpriteRenderer fadeInSprite;
    [SerializeField] int sceneToLoad;
    private GameObject content;
    private Transform textSpawn;
    private InputActionMap inputMap;
    private InputAction continueKey;
    private EventInstance currentVoice;

    public List<GameObject> textBoxes = new List<GameObject>();
    private GameObject newBox;
    private TextMeshProUGUI newBoxText;
    private SceneManager sceneManager;
    private string[] dialogueByLine;
    private string currentLine;
    private string currentTag;
    private float counter;
    private float fadeInAlpha;
    private int letterCount = 1;
    private int totalLetterCount;
    private bool isWriting;
    private bool isYapping;
    private bool readingTag;
    private bool quickFinish;
    private bool end;
    private bool voiceState;
    private Queue<string> dialogueLines = new Queue<String>();

    public delegate void MoveBoxesUp(float moveAmount);
    public MoveBoxesUp moveBoxesUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newBoxText = null;
        inputMap = inputAction.FindActionMap("Player");
        continueKey = inputMap.FindAction("Interact");
        continueKey.performed += _ => ContinueDialogue();
        continueKey.Disable();
        textSpawn = GameObject.Find("TextSpawn").GetComponent<Transform>();
        content = GameObject.Find("Content");
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        sceneManager.startScene += Begin;


        dialogueByLine = dialogueFile.text.Split("\n");
        for (int i = 0; i < dialogueByLine.Length; i++) 
        {
            //Debug.Log(dialogueByLine[i]);
            dialogueLines.Enqueue(dialogueByLine[i]);
        }
    }
    /* public void Set(string fileName, Sprite speakerImage, int tpl = 25)
    {
        dialogueFile = fileName;
        speakerSprite = GameObject.Find("SpeakerPortrait").GetComponent<SpriteRenderer>();
        speakerSprite.sprite = speakerImage;
        writeSpeed = tpl;
    }*/

    void Begin()
    {
        continueKey.Enable();
        ContinueDialogue();
    }

    public void WriteLine(int line)
    {

    }
    Vector2 heightShift;
    private void ContinueDialogue()
    {
        //Debug.Log("isWriting: " + isWriting);
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
            if (dialogueLines.Count > 0)
            {
                currentLine = dialogueLines.Dequeue();
                //lineBox.GetComponentInChildren<TextMeshProUGUI>().text = "<alpha=#00>" + currentLine;
                newBox = Instantiate(lineBox, textSpawn.transform.position, textSpawn.transform.rotation, content.transform);
                textBoxes.Add(newBox);
                newBoxText = newBox.GetComponentInChildren<TextMeshProUGUI>();
                newBoxText.text = "<alpha=#00>" + StripAllTags(currentLine, false);
                newBoxText.ForceMeshUpdate();
                //Debug.Log(newBoxText.GetRenderedValues(true));
                heightShift = new Vector2(0, newBoxText.GetRenderedValues(true).y + textGap);
                content.GetComponent<RectTransform>().sizeDelta += heightShift;
                if (moveBoxesUp != null)
                {
                    moveBoxesUp.Invoke(heightShift.y);
                }
                textSpawn.transform.localPosition -= new Vector3(0, heightShift.y, 0) / 2;
                leftSprite.transform.localPosition -= new Vector3(0, heightShift.y, 0) / 2;
                rightSprite.transform.localPosition -= new Vector3(0, heightShift.y, 0) / 2;
                newBoxText.text = null;
                letterCount = 0;
                //UnityEngine.Debug.Log(currentLine[letterCount]);
                isWriting = true;
            }
        }
        else if (isWriting && !quickFinish)
        {
            quickFinish = true;
        }
        if (dialogueLines.Count == 0 && !end)
        {
            end = true;
            //Debug.Log("End=true");
        }
        else if (end && !isWriting && !sceneManager.isFadingOut)
        {
            GameObject.Find("SceneManager").GetComponent<SceneManager>().FadeOutOfScene(5, sceneToLoad);
            AudioManager.ins.musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Debug.Log("Loaded Scene " + sceneToLoad);
        }
    }

    private string StripAllTags(string line, bool customOnly)
    {
        string strippedLine = null;
        bool deleteMode = false;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] != '<' && !deleteMode)
            {
                strippedLine += line[i];
            }
            else if (line[i] == '<')
            {
                if (line[i+1] == '$' || !customOnly)
                {
                    deleteMode = true;
                }
            }
            if (line[i] == '>')
            {
                deleteMode = false;
            }
        }
        //Debug.Log(strippedLine);
        return strippedLine;
    }

    private void ReadCustomTag(string tag)
    {
        if (tag.StartsWith("P"))
        {
            tag = tag.Remove(0, 1);
            if (tag.StartsWith("L"))
            {
                tag = tag.Remove(0, 1);
                //Debug.Log(tag);
                int portraitIndex = int.Parse(tag);
                GameObject newPortrait = Instantiate(portraitPrefab, new Vector3(leftSprite.transform.position.x, newBox.transform.position.y - (heightShift.y - textGap) / 1200, leftSprite.transform.position.z), leftSprite.transform.rotation, newBox.transform);
                newPortrait.GetComponent<Animator>().runtimeAnimatorController = portraitSizer;
                newPortrait.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = portraits[portraitIndex];
            }
            else if (tag.StartsWith("R"))
            {
                tag = tag.Remove(0, 1);
                //Debug.Log(tag);
                int portraitIndex = int.Parse(tag);
                GameObject newPortrait = Instantiate(portraitPrefab, new Vector3(rightSprite.transform.position.x, newBox.transform.position.y - (heightShift.y - textGap) / 1200, rightSprite.transform.position.z), rightSprite.transform.rotation, newBox.transform);
                newPortrait.GetComponent<Animator>().runtimeAnimatorController = portraitSizer;
                newPortrait.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = portraits[portraitIndex];
            }
        }
        else if (tag.StartsWith("M"))
        {
            tag = tag.Remove(0, 1);
            int parameterNum = int.Parse(tag.Substring(tag.Length - 1, 1));
            tag = tag.Remove(tag.Length - 1, 1);
            AudioManager.ins.musicEventInstance.setParameterByName(tag, parameterNum);
        }
        else if (tag.StartsWith("V"))
        {
            tag = tag.Remove(0, 1);
            int voiceIndex = int.Parse(tag);
            currentVoice = AudioManager.ins.CreateInstance(voices[voiceIndex]);
            Debug.Log("Voice made current voice");
            currentVoice.start();
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
            //currentVoice.setPaused(true);
            //currentVoice.getPaused(out voiceState);
            currentVoice.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //Debug.Log("YOMAMA");
            //Debug.Log(newBoxText.GetRenderedValues(true));
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
                //Debug.Log("I'm reading a tag");
                for (int i = letterCount; true; i++)
                {
                    currentTag += currentLine[i];
                    //Debug.Log(currentTag + ", " + i);
                    //Debug.Log(currentTag.Contains('>'));
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
                            //Debug.Log(currentTag);
                            ReadCustomTag(currentTag);
                        }
                        //Debug.Log("Read Tag: " + currentTag);
                        break;
                    }
                }
            }
            else if ((currentLine[letterCount] == '.' || currentLine[letterCount] == '!') && letterCount < StripAllTags(currentLine, true).Length)
            {
                newBoxText.text += currentLine.Substring(letterCount, 1);
                letterCount++;
                counter = (1 / writeSpeed) - pauseTime;
                currentVoice.setPaused(true);
                currentVoice.getPaused(out voiceState);
            }
            else if (currentLine[letterCount] == ',' && letterCount < currentLine.Length)
            {
                newBoxText.text += currentLine.Substring(letterCount, 1);
                letterCount++;
                counter = (1 / writeSpeed) - commaTime;
                currentVoice.setPaused(true);
                currentVoice.getPaused(out voiceState);
            }
            else if ((counter > 1 / writeSpeed && !readingTag) || (currentLine[letterCount] == ' ' && currentLine[letterCount - 1] != '.' && currentLine[letterCount - 1] != ',' && currentLine[letterCount - 1] != '!') || quickFinish)
            {
                newBoxText.text += currentLine.Substring(letterCount, 1);
                letterCount++;
                counter = 0;
                Debug.Log("state: " + voiceState);
                if (voiceState == true)
                {
                    currentVoice.setPaused(false);
                }
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
