using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    static int currentStage = 0;
    [NonSerialized] public PlayerInfo player = new PlayerInfo(PlayerInfo.playerType.Akai);
    public GameObject gameUI;
    GameObject gameCanvas;
    GameObject dialoguePanel;

    private void Awake()
    {
        GameObject[] t = GameObject.FindGameObjectsWithTag("gameManager");
        if (t.Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        GameObject gameCanvas;
        if (SceneManager.GetActiveScene().name == "level1")
        {

            gameCanvas = Instantiate(gameUI);
            gameCanvas.name = "Canvas";
            gameCanvas.transform.parent = gameObject.transform;

        }
        else
        {
            gameCanvas = GameObject.Find("Canvas");

        }
        if (SceneManager.GetActiveScene().name == "levelComplete") gameCanvas.SetActive(false);
        else gameCanvas.SetActive(true);

        dialoguePanel = GameObject.Find("dialoguePanel");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitOneFrame());   
    }

    IEnumerator WaitOneFrame()
    {
        yield return null;
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStage(int newStage)
    {
        currentStage = newStage;

    }

    public void IncreaseStage(int increment)
    {

        currentStage += increment;

    }

    public int GetStage()
    {

        return (currentStage);
    }

    public void LoadNewScene()
    {

        string nextScene = "level" + (GetStage() + 1);
        GetComponent<QuestSystem>().stagePanel.SetActive(true);
        if (GetStage() > 0) GetComponent<QuestSystem>().Init();
        SceneManager.LoadScene(nextScene);

    }
}
