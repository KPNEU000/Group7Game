using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform target;

    private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = transform.position - target.position; //Distance of Camera to target
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, Time.deltaTime * 10);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.RotateAround(target.position, Vector3.up, Time.deltaTime * 10);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.RotateAround(target.position, Vector3.up, Time.deltaTime * -10);
        }
    }

    void LateUpdate() //Called after everything in the Update field 
    {
        if (target) {
        transform.position = target.position + offset; //Always maintain the initial distance
        transform.LookAt(target.position);
        }
    }
}
