using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;


public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject highscoreMenu;
    [SerializeField]
    private TMP_InputField playerNameInput;
    [SerializeField]
    private TMP_Text highscorePlayerScoreText;

    public GameObject canvas;



    public static Menu Instance;

    private string playerName;
    public string highscoreName;
    public int highscoreScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);

        ShowMainmenu();
        LoadHighscore();
        highscorePlayerScoreText.text = highscoreName + "\n" + highscoreScore + " points";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartNewGame()
    {
        playerName = playerNameInput.text;

        SceneManager.LoadScene(1);
        Debug.Log("LoadScene: " + SceneManager.GetSceneByBuildIndex(1).name);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ShowHighscore()
    {
        mainMenu.SetActive(false);
        highscoreMenu.SetActive(true);
    }

    public void ShowMainmenu()
    {
        highscoreMenu.SetActive(false);
        mainMenu.SetActive(true);
    }



    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int score;
    }

    public void SaveHighscore(int points)
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.score = points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log("File.WriteAllText(" + Application.persistentDataPath + "/savefile.json" +", " + json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscoreName = data.playerName;
            highscoreScore = data.score;

            Debug.Log("File.ReadAllText(" + Application.persistentDataPath + "/savefile.json" + ", " + json);


        }
    }
}
