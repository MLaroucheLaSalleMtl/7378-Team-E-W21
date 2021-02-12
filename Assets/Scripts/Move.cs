using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	public Rigidbody rb;
	private float speed = 9;
	private Vector3 movement;

	private Vector3 jump;
	public bool isGrounded;
	private float jumpforce = 8000;

	public Transform GroundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	private float turnspeed = 0.1f;

	private bool canpush = false;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void CharacterMove(Vector3 vector)
	{
		movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		rb.MovePosition(transform.position + vector * speed * Time.fixedDeltaTime);
		if (movement != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), turnspeed);
		}
	}

	void Jump()
	{
		jump = new Vector3(0, jumpforce, 0);
		if (isGrounded == true && Input.GetButtonDown("Jump"))
		{
			rb.AddForce(jump);
		}
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "jumppad")
		{
			jumpforce = 20000;
		}
		else if (other.gameObject.tag == "speedup")
		{
			speed = 15;
		}
		else if (other.gameObject.tag == "slowdown")
		{
			speed = 3;
		}
		if (other.gameObject.tag == "button")
		{
			canpush = true;
		}
	}

    private void OnTriggerExit(Collider other)
    {
		if (other.gameObject.tag == "jumppad")
		{
			jumpforce = 8000;
		}
		else if (other.gameObject.tag == "speedup")
		{
			speed = 9;
		}
		else if (other.gameObject.tag == "slowdown")
		{
			speed = 9;
		}
		if (other.gameObject.tag == "button")
		{
			canpush = false;
		}
	}

	private void pushplayer()
    {
		if (canpush == true && Input.GetKeyDown(KeyCode.E))
		{
			rb.AddForce(new Vector3(0, 0, -20000));
		}
	}
    private void Update()
	{
		isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
		Jump();
		pushplayer();
	}

	void FixedUpdate()
	{
		CharacterMove(movement);
	}
}
