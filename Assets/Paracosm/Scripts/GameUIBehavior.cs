using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIBehavior : MonoBehaviour
{
    public static GameUIBehavior Instance;
    [SerializeField]
    GameObject levelText;
    [SerializeField]
    GameObject clueHint;

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
            levelText.SetActive(false);
            clueHint.SetActive(false);
        }
    }
}
