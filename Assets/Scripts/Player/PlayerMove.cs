using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    Rigidbody playerRb;
    Animator anim;
    public float walkSpeed = 10f;
    public float runSpeed = 25f;
    public float mouseSensitivity = 1.5f;
    public float vertLookLimit = 70;
    // called in update, so might be more efficient to make global instead of making new var every frame?
    Vector3 movement = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        // if I happen to accidentally have player rotated at start, this fixes it
        rotation.y = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Enable pause
        if (Time.timeScale == 0)
            return;

        // Get world variables from the local variables
        Vector3 forwardDir = transform.TransformDirection(Vector3.forward);
        Vector3 rightDir = transform.TransformDirection(Vector3.right);

        // Get movement inputs
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool shift = Input.GetKey(KeyCode.LeftShift);

        // Check if sprinting
        bool walking = h != 0f || v != 0f;
        bool running = false;
        float speed = walkSpeed;
        if(walking && shift)
        {
            speed = runSpeed;
            walking = false;
            running = true;
        }

        movement = (forwardDir * v) + (rightDir * h);
        movement = movement.normalized * speed; //* Time.deltaTime;
        //playerRb.MovePosition(transform.position + movement);
        //playerRb.velocity = Vector3.ClampMagnitude(new Vector3(movement.x, playerRb.velocity.y - (9.8f*Time.deltaTime), movement.z), speed);
        playerRb.velocity = new Vector3(movement.x, playerRb.velocity.y - (9.8f * Time.deltaTime), movement.z);


        // Get rotation based on mouse position
        rotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotation.x -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        // Limit how far up/down you can look
        rotation.x = Mathf.Clamp(rotation.x, -vertLookLimit, 10);
        playerRb.MoveRotation(Quaternion.Euler(0, rotation.y, 0));

        // Move camera up/down, but not the player
        Vector3 cameraAngle = cameraTransform.eulerAngles;
        cameraAngle.x = rotation.x;
        cameraTransform.eulerAngles = cameraAngle;

        // Animation time
        anim.SetBool("isWalk", walking);
        anim.SetBool("isRun", running);
        anim.SetFloat("Vertical", v);
        anim.SetFloat("Horizontal", h);

        //Debug.Log(Input.GetAxis("Mouse X") + " " + Input.GetAxis("Mouse Y"));
        //Debug.Log(transform.rotation);
    }

    public void SetRotation(Vector2 eul)
    {
        rotation = eul;
        playerRb.MoveRotation(Quaternion.Euler(eul.x, eul.y, 0));
    }
}
