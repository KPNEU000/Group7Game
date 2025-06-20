using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //public static LevelManager Instance;
    public static bool IsPlaying { get; private set; }
    public float levelTime = 300;
    public TMP_Text messageText;
    public string nextLevel;
    public GameObject lastClue;

    float countdown;
    bool isFound = false;
    string clueName;
    GameUIBehavior gameUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
/*
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    */
    void Start()
    {
        gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUIBehavior>();
        gameUI.HideGameMessage();
        countdown = levelTime;
        IsPlaying = true;
        if (lastClue) {
            clueName = lastClue.name;
            gameUI.UpdateClueHint(clueName);
        } else {
            if (SceneManager.GetActiveScene().name != "Level 2") {
                Debug.Log("Warning! No last clue specified!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying) {
            LevelTimer();
            gameUI.SetTimerText(countdown.ToString("0"));

            if (isFound) {
                LevelBeat();
            } else if (countdown <= 0) {
                if (SceneManager.GetActiveScene().name != "Level 2") {
                    LevelLost();
                } else {
                    LevelBeat();
                }
            }
        }
    }

    void LevelTimer() {
        countdown -= Time.deltaTime;

        if (countdown <= 0) {
            countdown = 0;
        }
    }

    void LevelBeat()
    {
        //IsPlaying = false;
        gameUI.DisplayGameMessage("Clue Found!");

        LoadSceneByName(nextLevel);
        //Invoke("ReloadSameScene", 5);
    }

    public void LevelLost() {
        //IsPlaying = false;
        gameUI.DisplayGameMessage("You Failed to Find the Clue");
        
        Invoke("ReloadSameScene", 2);
    }

    void ReloadSameScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadSceneByName(string name) {
        SceneManager.LoadScene(name);
    }

    public void LoadNextLevel() {
        if (nextLevel.Length > 0) {
            LoadSceneByName(nextLevel);
        } else {
            Debug.LogWarning("No nextLevel is specified in inspector!");
        }
    }

    public void ItemCollected(string item) {
        if (clueName != null) {
            if (item == clueName) {
                isFound = true;
            }
        }
    }
}
