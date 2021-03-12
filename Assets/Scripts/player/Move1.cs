using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour
{
	private Rigidbody rb;
	public Transform cam;
	private float speed = 9;
	private Vector3 movement;
	private Vector3 moveDir;

	private Vector3 jump;
	private bool isJumpPressed;
	private bool isFlowPressed;
	private float jumpforce = 10000;

    public bool isGrounded;
	public Transform GroundCheck;
	public float groundDistance = 0.3f;
	public LayerMask groundMask;

	private float turnsmoothtime = 0.1f;
	private float turnvelocity;

	private bool canpush = false;

    public Vector3 Movement { get => movement; set => movement = value; }
    public bool IsJumpPressed { get => isJumpPressed; set => isJumpPressed = value; }

    void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void CharacterMove()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");
		movement = new Vector3(x, 0, y);
		moveDir = new Vector3(x, 0, y).normalized;

		//movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		if (movement != Vector3.zero)
		{
			movement = cam.TransformDirection(movement);
			movement.y = 0;
			moveDir = cam.TransformDirection(moveDir);
			moveDir.y = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), turnsmoothtime);
			//moveDir = cam.transform.forward * movement.z + cam.transform.right * movement.x;
			//moveDir = transform.InverseTransformDirection(moveDir);
			//float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			//float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnvelocity, turnsmoothtime);
			//transform.rotation = Quaternion.Euler(0f, angle, 0f);
			//moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
		}
		rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
	}

	void Jump()
	{
		jump = new Vector3(0, jumpforce, 0);
		if (isGrounded == true && isJumpPressed == true)
		{
			isJumpPressed = false;
			rb.AddForce(jump);
		}
		if (isGrounded == false && rb.velocity.y < -1f && isFlowPressed == true)
        {
			isFlowPressed = false;
			Physics.gravity = new Vector3(0f, -1f, 0f);
        }
        else
        {
			Physics.gravity = new Vector3(0f, -9.81f, 0f);
		}
	}

    private void Update()
	{
        if(Input.GetButtonDown("Jump"))
        {
			isJumpPressed = true;
        }
		if(Input.GetButton("Jump"))
        {
			isFlowPressed = true;
        }
	}

	void FixedUpdate()
	{
		isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
		Jump();
		CharacterMove();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slow")
        {
            speed = 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slow")
        {
            speed = 9;
        }
    }
}
