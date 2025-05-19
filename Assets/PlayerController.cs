using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 8;
    public float jumpForce = 5;

    private bool isGrounded;

    public bool hasKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"); //Value between 1 (right) and -1 (left)
        float verticalMovement = Input.GetAxis("Vertical"); //Value between 1 (forward) and -1 (backward)
    
        //Compute a movement vector
        Vector3 playerMovement = new Vector3(horizontalMovement, 0, verticalMovement).normalized; //Normalized used to get only the direction of the vector
        rb.AddForce(playerMovement * speed);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //PlaySound();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //ForceMode applied immediately and not continuously
            isGrounded = false;
        }
    }

    private void PlaySound()
    {

        var audioSource = GetComponent<AudioSource>();
        if(audioSource.clip)
        {
           // audioSource.clip = jumpSFX;
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.transform.name);

        //GROUNDED CHECK
        ContactPoint ground = collision.contacts[0]; //The first point of collision, which in this case is the floor 
        if(ground.normal.y > 0.5f) //roughly horizontal surface
        {
            isGrounded = true;
        }
        Debug.Log("Contact position: " + ground.point);
        Debug.Log("Contact normal: " + ground.normal); //1 if horizontal, 0 if vertical 

    
    }
}
