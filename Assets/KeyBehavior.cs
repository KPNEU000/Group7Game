using UnityEngine;

public class KeyBehavior : MonoBehaviour
{

    public AudioClip pickUpSFX;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyPickup();
            other.GetComponent<PlayerController>().hasKey = true;
        }
    }

    void DestroyPickup()
    {
        PlayAudio();
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        
    }

    void PlayAudio()
    {
       // AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position);
    }
}
