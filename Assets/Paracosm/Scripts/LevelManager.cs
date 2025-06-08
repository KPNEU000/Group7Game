using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}
    public float levelTime = 300;
    public TMP_Text timerText;
    public TMP_Text messageText;
    public TMP_Text clueHint;
    public string nextLevel;
    public GameObject lastClue;

    float countdown;
    bool isFound = false;
    string clueName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdown = levelTime;
        IsPlaying = true;
        if (lastClue) {
            clueName = lastClue.name;
            if (clueHint) {
                clueHint.text = "Find the " + clueName;
            }
        } else {
            Debug.Log("Warning! No last clue specified!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying) {
            LevelTimer();
            SetTimerText();

            if (isFound) {
                LevelBeat();
            } else if (countdown <= 0) {
                LevelLost();
            }
        }
    }

    void LevelTimer() {
        countdown -= Time.deltaTime;

        if (countdown <= 0) {
            countdown = 0;
        }
    }

    void SetTimerText() {
        if (timerText) {
            timerText.text = "Time Left: " + countdown.ToString("0");
        }
    }

    void LevelBeat()
    {
        IsPlaying = false;
        DisplayGameMessage("Clue Found!");
        
        Invoke("ReloadSameScene", 5);
    }

    void LevelLost() {
        IsPlaying = false;
        DisplayGameMessage("You Failed to Find the Clue");
        
        Invoke("ReloadSameScene", 5);
    }

    void DisplayGameMessage(string message) {
        if (messageText) {
            messageText.text = message;
            messageText.enabled = true;
        }
    }

    void ReloadSameScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    void LoadSceneByName(string name) {
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
        if (item == clueName) {
            isFound = true;
        }
    }
}
