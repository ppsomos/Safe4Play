using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovement : MonoBehaviour
{

    GameObject[] rightCharacters;
    GameObject[] leftCharacters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(int direction)
    {
        rightCharacters = GameObject.FindGameObjectsWithTag("RightSide");
        foreach (GameObject c in rightCharacters)
        {
            Animator anim = c.GetComponent<Animator>();
            anim.Play("walk");

            Rigidbody2D myRigidbody = c.transform.parent.gameObject.GetComponent<Rigidbody2D>();
            myRigidbody.velocity = new Vector2(-1*direction, 0);
        }

        leftCharacters = GameObject.FindGameObjectsWithTag("LeftSide");
        foreach (GameObject c in leftCharacters)
        {
            Animator anim = c.GetComponent<Animator>();
            anim.Play("walk");

            Rigidbody2D myRigidbody = c.transform.parent.gameObject.GetComponent<Rigidbody2D>();
            myRigidbody.velocity = new Vector2(direction*1, 0);
        }
    }

    public void StopMove()
    {
        rightCharacters = GameObject.FindGameObjectsWithTag("RightSide");
        foreach (GameObject c in rightCharacters)
        {
            Animator anim = c.GetComponent<Animator>();
            anim.Play("idle");

            Rigidbody2D myRigidbody = c.transform.parent.gameObject.GetComponent<Rigidbody2D>();
            myRigidbody.velocity = new Vector2(0, 0);
        }

        leftCharacters = GameObject.FindGameObjectsWithTag("LeftSide");
        foreach (GameObject c in leftCharacters)
        {
            Animator anim = c.GetComponent<Animator>();
            anim.Play("idle");

            Rigidbody2D myRigidbody = c.transform.parent.gameObject.GetComponent<Rigidbody2D>();
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }
}
