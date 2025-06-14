using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public void PlayClickSound(AudioClip clickSFX)
    {
        AudioSource.PlayClipAtPoint(clickSFX, transform.position);
    }
}
