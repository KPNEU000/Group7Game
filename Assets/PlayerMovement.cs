using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 0.5f;
    public float gravity = 9.81f;
    public float airControl = 10f;

    Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;

    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        UpdatePlayerAnim(0);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Debug.Log(controller.isGrounded);
        //if (animator.GetInteger("animState") != 1)
        //{
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                UpdatePlayerAnim(4);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                UpdatePlayerAnim(3);
            }
            if (moveHorizontal == 0
            && moveVertical == 0
            && controller.isGrounded)
            {
                UpdatePlayerAnim(0);
            }
            moveDirection.y -= gravity * Time.deltaTime; //Apply gravity constantly
            input = transform.right * moveHorizontal + transform.forward * moveVertical;
            input.Normalize();

            if (controller.isGrounded)
            {
                moveDirection = input;
                if (Input.GetButton("Jump"))
                {
                    //UpdatePlayerAnim(2);
                    moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                }
                else
                {
                    moveDirection.y = 0.0f; //reset
                }
            }
            else //midair
            {
                input.y = moveDirection.y;
                moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
                //UpdatePlayerAnim(5); //The isGrounded variable keeps flickering, so before that's fixed I'll just comment out these animations
            }


            controller.Move(input * speed * Time.deltaTime);
        //}
    }

    public void UpdatePlayerAnim(int animState)
    {
        animator.SetInteger("animState", animState);
    }
}
