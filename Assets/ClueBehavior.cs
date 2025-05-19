using UnityEngine;

public class ClueBehavior : MonoBehaviour
{
    public bool collected;
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
        if (other.CompareTag("Player")) //Put in inventory?
        {
            gameObject.transform.parent = other.transform;
            collected = true;
        }
    }

    void OnMouseDown()
    {
        if (collected)
        {
            //show flavor text?

        }
    }
}
