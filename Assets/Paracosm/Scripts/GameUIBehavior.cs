using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIBehavior : MonoBehaviour
{
    public static GameUIBehavior Instance;
    [SerializeField]
    TMP_Text messageText;
    [SerializeField]
    TMP_Text clueHint;
    [SerializeField]
    TMP_Text timerText;

    void Awake()
    {
        if (Instance != null & Instance != this) //If there is another Instance
        {
            Destroy(gameObject); //Destroy this one so there is only one 
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            messageText.enabled = false;
            clueHint.enabled = false;
        }
    }

    public void UpdateClueHint(string clueName) {
        if (clueHint) {
            clueHint.text = "Find the " + clueName;
        }
    }

    public void SetTimerText(string countdown) {
        if (timerText) {
            timerText.text = "Time Left: " + countdown;
        }
    }

    public void DisplayGameMessage(string message) {
        if (messageText) {
            messageText.text = message;
            messageText.enabled = true;
        }
    }

    public void HideGameMessage() {
        if (messageText) {
            messageText.enabled = false;
        }
    }
}
