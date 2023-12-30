using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    GameObject[] characters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWalkAnimation()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject c in characters)
        {
            Animator anim = c.GetComponent<Animator>();
            anim.enabled = true;
            anim.Play("walk");
        }
        
    }

    public void StopWalkAnimation()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject c in characters)
        {
            Animator anim = c.GetComponent<Animator>();
            anim.enabled = false;
        }
    }
}
