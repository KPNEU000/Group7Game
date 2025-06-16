using UnityEngine;

public class ClueBehavior : MonoBehaviour
{

    public bool collected;
    [Header("Audio")]

    [SerializeField]
    private AudioClip clueCollected;
    LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }
    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime);
    }

/*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Put in inventory?
        {
            
            other.GetComponent<PlayerMovement>().UpdatePlayerAnim(1);
        }
    }
*/

    public void PickedUp()
    {
        levelManager.ItemCollected(gameObject.name);
        collected = true;
        AudioSource.PlayClipAtPoint(clueCollected, Camera.main.transform.position);
        gameObject.SetActive(false);
    }
}
