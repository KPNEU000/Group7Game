using UnityEngine;

public class KeyBehavior : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField]
    private AudioClip pickUpSFX;
    [SerializeField]
    private AudioClip twinkle;

    void Start()
    {
        InvokeRepeating("Twinkle", 0, 5f);
    }

    void Twinkle()
    {
        if (twinkle)
        {
            AudioSource.PlayClipAtPoint(twinkle, transform.position);
        }
    }

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
            other.GetComponent<PlayerMovement>().UpdatePlayerAnim(1);
            other.GetComponent<PlayerController>().hasKey = true;
        }
    }

    void DestroyPickup()
    {
        AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position);
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
