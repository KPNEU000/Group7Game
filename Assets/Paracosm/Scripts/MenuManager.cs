using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Canvases;
    [SerializeField]
    Slider sensitivitySlider;
    [SerializeField]
    TMP_Text sensitivityText;
    [SerializeField]
    Material playerMaterial; //WILL LIKELY NEED TO BE CHANGED WHEN PLAYER MODEL LIKELY CHANGES
    [SerializeField]
    List<Color> Colors;
    [SerializeField]
    GameObject timeDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivitySlider.value = CameraMovement.mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTimePlayed(timeDisplay);
    }

    public void GotoCanvas(int canvasIndex)
    {
        foreach (GameObject canvas in Canvases)
        {
            if (canvas == Canvases[canvasIndex])
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }

    public void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ChangeMouseSensitivity()
    {
        CameraMovement.mouseSensitivity = sensitivitySlider.value;
        sensitivityText.text = CameraMovement.mouseSensitivity.ToString();
    }

    public void ChangePlayerColor(int colorIndex)
    {
        playerMaterial.SetColor("_EmissionColor", Colors[colorIndex]);
    }

    public void DisplayTimePlayed(GameObject timeDisplay)
    {
        if (timeDisplay.activeSelf)
        {
            timeDisplay.GetComponent<TextMeshProUGUI>().text = Time.time.ToString("0.00");
        }
    }
/*
    public void DisplayCluesCollected(GameObject clueDisplay)
    {
        clueDisplay.GetComponent<TextMeshProUGUI>().text = PlayerMovement.cluesCollected.ToString() + "/" + PlayerMovement.clues.Count.ToString();
    }
    */

    public void QuitGame()
    {
        Application.Quit();
        Application.OpenURL("about:blank");
    }
}
