using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    string nameOfCharacter;

    Dialogue[] dialogues;

    int nbDialogues;

    int currentDialogueIndex = 0;

    bool waitingForUserInput = false;

    bool dialogueIsActive = false;

    GameObject dialogueBox, dialoguePanel;

    private void Awake()
    {
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("dialogueBox");
        dialoguePanel = GameObject.Find("dialoguePanel");
        nameOfCharacter = gameObject.name;

        nbDialogues = calculateNbDialogues();

        dialogues = new Dialogue[nbDialogues];

        loadDialogues();
        nbDialogues = calculateNbDialogues();

        //dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueIsActive)
        {
            if (!waitingForUserInput)
            {
                dialoguePanel.SetActive(true);
                if (currentDialogueIndex != -1) displayDialogue2();
                else
                {

                    dialogueIsActive = false;
                    waitingForUserInput = false;
                    dialoguePanel.SetActive(false);
                    GameObject.Find("Player").GetComponent<ControlPlayer>().EndTalking();

                    currentDialogueIndex = 0;

                    GameObject.Find("GameManager").GetComponent<QuestSystem>().Notify(QuestSystem.possibleActions.talk_to, nameOfCharacter);


                }
                waitingForUserInput = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {

                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[0];
                    waitingForUserInput = false;

                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    currentDialogueIndex = dialogues[currentDialogueIndex].targetForResponse[1];
                    waitingForUserInput = false;
                }
            }
        }
    }

    public void loadDialogues()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        int dialogueIndex = 0;

        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter)
            {

                dialogueIndex = 0;
                foreach (XmlNode dialogueFromXML in character.SelectNodes("dialogue"))
                {
                    dialogues[dialogueIndex] = new Dialogue();
                    dialogues[dialogueIndex].message = dialogueFromXML.Attributes.GetNamedItem("content").Value;
                    int choiceIndex = 0;
                    dialogues[dialogueIndex].response = new string[2];
                    dialogues[dialogueIndex].targetForResponse = new int[2];
                    foreach (XmlNode choice in dialogueFromXML)
                    {

                        dialogues[dialogueIndex].response[choiceIndex] = choice.Attributes.GetNamedItem("content").Value;
                        dialogues[dialogueIndex].targetForResponse[choiceIndex] = int.Parse(choice.Attributes.GetNamedItem("target").Value);
                        choiceIndex++;

                    }
                    dialogueIndex++;

                }

            }


        }

    }

    public int calculateNbDialogues()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);
        int dialogueIndex = 0;

        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value == nameOfCharacter)
            {
                foreach (XmlNode dialogueFromXML in doc.SelectNodes("dialogues/character/dialogue"))
                {

                    dialogueIndex++;
                }

            }

        }

        return dialogueIndex;


    }

    public void displayDialogue1()
    {

        print(dialogues[currentDialogueIndex].message);
        print("[A]" + dialogues[currentDialogueIndex].response[0]);
        print("[B]" + dialogues[currentDialogueIndex].response[1]);

    }

    public void displayDialogue2()
    {
        GameObject.Find("dialogueImage").GetComponent<RawImage>().texture = Resources.Load<Texture2D>(gameObject.name) as Texture2D;
        string textToDisplay = "[" + gameObject.name + "]" + " " + dialogues[currentDialogueIndex].message + "\n [A]> " + dialogues[currentDialogueIndex].response[0] + "\n [B]> " + dialogues[currentDialogueIndex].response[1];
        GameObject.Find("dialogueBox").GetComponent<Text>().text = textToDisplay;


    }

    public void startDialogue()
    {
        waitingForUserInput = false;
        dialogueIsActive = true;


    }
}

