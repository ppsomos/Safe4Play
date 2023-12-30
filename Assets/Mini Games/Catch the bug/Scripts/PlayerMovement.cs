using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    private float moveSpeed = 12f;
    public Rigidbody2D rb;

    public GameObject foam;

    // Update is called once per frame
    void Update()
    {
        if (joystick.Horizontal >= .2f)
        {
            horizontalMove = moveSpeed;
        } else if (joystick.Horizontal <= -.2f)
        {
            horizontalMove = -moveSpeed;
        } else
        {
            horizontalMove = 0;
        }

        if (joystick.Vertical >= .2f)
        {
            verticalMove = moveSpeed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            verticalMove = -moveSpeed;
        }
        else
        {
            verticalMove = 0;
        }


        if (horizontalMove == 0 && verticalMove == 0)
        {
            foam.SetActive(false);
        } else
        {
            foam.SetActive(true);
        }

        rb.velocity = new Vector2(horizontalMove, verticalMove);
    }

}
