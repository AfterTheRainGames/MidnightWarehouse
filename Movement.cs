using UnityEngine;

public class Movement : MonoBehaviour
{

    private Transform player;
    private CharacterController controller;
    private float speed = 5f;
    private Vector3 velocity;
    private float gravity = -20f;
    
    public Transform camPlayer;
    public float sens = .5f;
    private float xRotation = 0f;

    void Start()
    {
        player = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (controller.enabled == true)
        {
            CameraRotation();
            PlayerMovement();
        }
    }

    void CameraRotation()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            sens = Mathf.Clamp(sens + 0.1f, .1f, 2f);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            sens = Mathf.Clamp(sens - 0.1f, .1f, 2f);
        }


        float mouseX = Input.GetAxis("Mouse X") * sens;
        float mouseY = Input.GetAxis("Mouse Y") * sens;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        camPlayer.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void PlayerMovement()
    {

            Vector3 forwardDirection = camPlayer.forward;
            Vector3 rightDirection = camPlayer.right;
            forwardDirection.y = 0;
            rightDirection.y = 0;
            float forwardPos = Input.GetAxisRaw("Vertical");
            float rightPos = Input.GetAxisRaw("Horizontal");
            Vector3 moveDirection = (forwardDirection * forwardPos + rightDirection * rightPos).normalized;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 7f;
            }
            else
            {
                speed = 5f;
            }

            if (moveDirection.magnitude != 0)
            {
                controller.Move(moveDirection * speed * Time.deltaTime);
            }

            if (controller.isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

    }

}