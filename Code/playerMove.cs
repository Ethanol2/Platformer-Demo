using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    public GameObject avatar;
    public Animator anim;

    float sprintNum = 1f;
    public float sprintMultiplier = 1.5f;
    public float walkSpeed = 10f;
    public float jumpPower = 50f;
    public Vector3 currentVelocity;
    public float distFromGround;
    public float raycastDist = 2f;

    Rigidbody rB;
    RaycastHit Hit;

    // Use this for initialization
    void Start()
    {
        anim = avatar.GetComponent<Animator>();
        anim.Play("Idle");
        rB = this.GetComponent<Rigidbody>();
        distFromGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = rB.velocity;
        sprintNum = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintNum = sprintMultiplier;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rB.AddForce(transform.right * 30f * 1f);

            if (avatar.GetComponent<SpriteRenderer>().flipX)
            {
                anim.StopPlayback();
                anim.Play("Turn");
                avatar.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            if (rB.velocity.x >= walkSpeed * sprintNum)
            {
                rB.velocity = new Vector3(walkSpeed * sprintNum, rB.velocity.y, 0f);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rB.AddForce(transform.right * 30f * -1f);

            if (!avatar.GetComponent<SpriteRenderer>().flipX)
            {
                anim.StopPlayback();
                anim.Play("Turn");
                avatar.GetComponent<SpriteRenderer>().flipX = true;
            }   

            if (rB.velocity.x <= -walkSpeed * sprintNum)
            {
                rB.velocity = new Vector3(-walkSpeed * sprintNum, rB.velocity.y, 0f);
            }
        }


        if (!isGrounded())
        {
            anim.Play("Jump");
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rB.AddForce(transform.up * jumpPower);
                this.gameObject.layer = 9;
            }
            if (rB.velocity.x == 0)
            {
                anim.Play("Idle");
            }
            else if (sprintNum == sprintMultiplier)
            {
                anim.Play("Run");
            }
            else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Turn"))
            {
                anim.Play("Walk");
            }
            
        }
        
        if (rB.velocity.y > 7f)
        {
            rB.velocity = new Vector3(rB.velocity.x, 7f, 0f);
        }

        if (rB.velocity.y  < 0f)
        {
            this.gameObject.layer = 2;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        Debug.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - (distFromGround + raycastDist), this.transform.position.z), Color.red);

        if (this.transform.position.x >= 270f)
        {
            SceneManager.LoadScene("End");
        }
    }

    bool isGrounded()
    {
        Vector3 pos = this.transform.position;
        pos.z += 1f;
        return Physics.Raycast(pos, Vector3.down, distFromGround + raycastDist);
    }
}
