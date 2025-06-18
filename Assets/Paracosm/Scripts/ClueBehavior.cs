using Unity.VisualScripting;
using UnityEngine;

public class ClueBehavior : MonoBehaviour
{

    public bool collected;
    [Header("Audio")]

    [SerializeField]
    private AudioClip clueCollected;
    LevelManager levelManager;
    public PlayerMovement pm;


    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
        pm.AddClueToList(gameObject);
        levelManager.ItemCollected(gameObject.name);
        collected = true;
        AudioSource.PlayClipAtPoint(clueCollected, Camera.main.transform.position);
        gameObject.SetActive(false);
    }
}
