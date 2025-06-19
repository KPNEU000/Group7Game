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
