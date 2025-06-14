using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class ButtonBehavior : MonoBehaviour
{

    [SerializeField]
    AudioClip hoveredSFX;
    [SerializeField]
    AudioClip clickedSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    void OnMouseOver()
    {
        AudioSource.PlayClipAtPoint(hoveredSFX, Camera.main.transform.position);
    }

    void OnMouseDown()
    {
        AudioSource.PlayClipAtPoint(clickedSFX, Camera.main.transform.position);
    }
}
