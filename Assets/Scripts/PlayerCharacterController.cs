using System.Collections;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Transform[] groundchecks;
    [SerializeField] private Transform[] wallchecks;
    [SerializeField] private AudioClip jumpsfx;

    private float gravity = -20f;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;
    private float horizontalInput;
    private bool jumpPressed;
    private float jumpTimer;
    private float jumpGracePeriod = .2f;

    public bool CanMove { get; set; } = true; // Add this line

    // Start is called before the first frame update
    void Start()
    {
        //Reference controller
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove) return; // Prevent movement if CanMove is false

        horizontalInput = 1;

        //Face Forward
        transform.forward = new Vector3(horizontalInput, 0, Mathf.Abs(horizontalInput) - 1);

        //isGrounded
        isGrounded = false;

        foreach (var groundcheck in groundchecks)
        {
            if (isGrounded = Physics.CheckSphere(groundcheck.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
                break;
            }
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        else
        {
            //Add Gravity
            velocity.y += gravity * Time.deltaTime;
        }

        //wall checks is for preventing player from stopping when they run into a wall
        var blocked = false;
        foreach (var wallCheck in wallchecks)
        {
            if (Physics.CheckSphere(wallCheck.position, 0.01f, groundLayers, QueryTriggerInteraction.Ignore))
            {
                blocked = true;
                break;
            }
        }

        if (!blocked)
        {
            characterController.Move(new Vector3(horizontalInput * runSpeed, 0, 0) * Time.deltaTime);
        }

        //Jumping
        jumpPressed = Input.GetButtonDown("Jump");

        if (jumpPressed)
        {
            jumpTimer = Time.deltaTime;
        }
        if (isGrounded && (jumpPressed || (jumpTimer > 0 && Time.deltaTime < jumpTimer + jumpGracePeriod)))
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            if (jumpsfx != null)
            {
                AudioSource.PlayClipAtPoint(jumpsfx, transform.position, 0.5f);
            }
            jumpTimer = -1;
        }

        //vertical velocity
        characterController.Move(velocity * Time.deltaTime);

        //Run Animation
        animator.SetFloat("Speed", horizontalInput);
        //Jump Animation
        animator.SetBool("IsGrounded", isGrounded);

        animator.SetFloat("VerticalSpeed", velocity.y);
    }
}
