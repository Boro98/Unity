using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private bool isFacingRight = false;
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f;
    private Animator animator;
    private float startPositionX;
    float moveRange = 1.0f;
    bool isMovingRight = false;
    //private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            if (this.transform.position.x <= startPositionX + moveRange)
            {
                MoveRight();
            }
            else
            {
                Flip();
                isMovingRight = false;
                MoveLeft();
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - moveRange)
            {
                MoveLeft();
            }
            else
            {
                Flip();
                isMovingRight = true;
                MoveRight();
            }
        }
    }

    void Awake()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        //defaultGravityScale = rigidBody.gravityScale;
        animator = GetComponent<Animator>();
        startPositionX = this.transform.position.x;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * (-1);
        transform.localScale = theScale;
    }

    private void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        //isWalking = true;
        if (!isFacingRight)
        {
            Flip();
        }
    }

    private void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        //isWalking = true;
        if (isFacingRight)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.position.y < collision.gameObject.transform.position.y)
            {
                animator.SetBool("isDead", true);
                StartCoroutine(KillOnAnimationEnd());
            }
        }
    }

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
