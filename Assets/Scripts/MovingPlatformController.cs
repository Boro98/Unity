using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public float moveRange = 2.0f;
    private float moveSpeed = 1.0f; 
    private bool isMovingRight = true;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private float startPositionX;
    
    void Start()
    {

    }

    void Update()
    {
        if (isMovingRight)
        {
            if (this.transform.position.x < (startPositionX + moveRange))
            {
                MoveRight();
            }
            else
            {
                isMovingRight = false;
                MoveLeft();
            }
        }
        else
        {
            if (this.transform.position.x > (startPositionX - moveRange))
            {
                MoveLeft();
            }
            else
            {
                isMovingRight = true;
                MoveRight();
            }
        }

    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPositionX = transform.position.x;
    }

    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    private void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }
}
