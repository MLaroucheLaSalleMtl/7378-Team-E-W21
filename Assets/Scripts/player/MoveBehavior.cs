using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
	//movement variables
	private Rigidbody rb;
	public Transform cam;
	private float speed = 9;
	private Vector3 movement;

	//check if grounded
	[SerializeField] private bool isGrounded;
	public Transform GroundCheck;
	private float groundDistance = 0.2f;
	[SerializeField] private LayerMask groundMask;

	//jumping variables
	[SerializeField]private float jumpHeight = 9;
	private float gravity = 9.81f;
	[SerializeField] private bool isJumpPressed;
	private bool isGlidePressed;

	//turning with camera
	private float turnSmoothTime = 0.1f;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

    public void move()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		//if vector3 is not zero follow the camera's direction
		if (movement != Vector3.zero)
		{
			movement = cam.TransformDirection(movement);
			movement.y = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnSmoothTime);
		}
		rb.MovePosition(transform.position + movement * speed * Time.fixedDeltaTime);
	}

	//calculated jumping velocity
	public float jumpVelocity()
    {
		return Mathf.Sqrt(2 * gravity * jumpHeight);
	}

	//while jumping calculate gravity and add forces
	public void jump()
    {
		float vel = jumpVelocity();
		//rb.AddForce(transform.up * vel, ForceMode.VelocityChange);
		if (isGrounded == true && isJumpPressed == true)
		{
			isJumpPressed = false;
			rb.AddForce(transform.up * vel, ForceMode.VelocityChange);
		}
		if (isGrounded == false && rb.velocity.y < -1f && isGlidePressed == true)
		{
			isGlidePressed = false;
			rb.MovePosition(transform.position + new Vector3(0, -1, 0) * Time.fixedDeltaTime);
		}
		else
		{
			rb.AddForce(-transform.up * gravity, ForceMode.Acceleration);
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			isJumpPressed = true;
		}
		if (Input.GetButton("Jump"))
		{
			isGlidePressed = true;
		}
	}

	void FixedUpdate()
	{
		isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
		jump();
		move();
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
