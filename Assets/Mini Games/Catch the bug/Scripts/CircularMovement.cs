using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField]
    Transform rotationCenter;

    [SerializeField]
    float rotationRadius = 1f, angularSpeed = 2f;

    float posX, posY, angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rotationCenter = transform;
    }

    // Update is called once per frame
    void Update()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector3(posX, posY, 0f);
        
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f)
            angle = 0f;
    }
}
