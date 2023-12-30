using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D myRigidbody;
    private Animator anim;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int WalkDirection;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            anim.Play("walk");
            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(moveSpeed, moveSpeed);
                    break;
                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, -moveSpeed);
                    break;
                case 2:
                    myRigidbody.velocity = new Vector2(-moveSpeed, -moveSpeed);
                    break;
                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, moveSpeed);
                    break;
            }

            if (walkCounter < 0)
            {
                anim.Play("idle");
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ChooseDirection();
    }
}
