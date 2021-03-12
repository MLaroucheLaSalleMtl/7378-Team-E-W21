using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestAnim : MonoBehaviour
{
    private Animator anim;
    private bool jump;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        jump = GetComponent<Move1>().IsJumpPressed;
        grounded = GetComponent<Move1>().isGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed", GetComponent<Move1>().Movement.magnitude);

        jump = GetComponent<Move1>().IsJumpPressed;
        grounded = GetComponent<Move1>().isGrounded;
        if (jump && grounded)
        {
            anim.SetTrigger("jump");
        }
    }

    //public void JumpAnim()
    //{
    //    anim.SetTrigger("jump");
    //}
}
