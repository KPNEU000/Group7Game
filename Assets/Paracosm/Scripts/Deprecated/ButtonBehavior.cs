using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{

    [SerializeField]
    AudioClip hoveredSFX;
    [SerializeField]
    AudioClip clickedSFX;
    void OnMouseOver()
    {
        AudioSource.PlayClipAtPoint(hoveredSFX, Camera.main.transform.position);
    }

    void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickedSFX, Camera.main.transform.position);
    }
}
