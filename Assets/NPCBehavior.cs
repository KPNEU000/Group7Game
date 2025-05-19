using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public Transform target;
    public GameObject clue;
    public GameObject text1;
    public GameObject text2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (clue.GetComponent<ClueBehavior>().collected == true)
            {
                text2.SetActive(true);
            }
            else
            {
                text1.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        text1.SetActive(false);
        text2.SetActive(false);
    }

    void LateUpdate() //Called after everything in the Update field 
    {
        if (target) {
        transform.LookAt(target.position);
        }
    }
}
