using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public Text maxScoreText;
    public int maxScorePoints = 0;
    public string maxScoreName = "";
    public string playerName;

    public InputField inputName;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadMaxScore();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }

    public void OnStartButtonClick()
    {
        if (inputName.text != "")
        {
            playerName = inputName.text;

            SceneManager.LoadScene(1);
        }
    }

    public void OnQuitButtonClick()
    {
        MenuManager.Instance.SaveMaxScore();

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    class SaveData
    {
        public int maxScorePoints;
        public string maxScoreName;
    }

    public void SaveMaxScore()
    {
        SaveData data = new SaveData();
        data.maxScorePoints = maxScorePoints;
        data.maxScoreName = maxScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadMaxScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            maxScorePoints = data.maxScorePoints;
            maxScoreName = data.maxScoreName;

            LoadMaxScoreText();
        }
    }

    private void LoadMaxScoreText()
    {
        if (maxScoreName != "" && maxScorePoints > 0)
        {
            maxScoreText.text = "Best Score : " + maxScoreName + " : " + maxScorePoints;
        }
    }
}
