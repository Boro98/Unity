using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{

    //public GameManager GameManager;
   
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.9f;
    [Range(0.01f, 20.0f)] [SerializeField] private float jumpForce = 6.0f;
    //[Space(10)]

    [SerializeField] AudioClip bSound;
    [SerializeField] AudioClip hSound;
    [SerializeField] AudioClip gemSound;

    Vector2 startPosition;

    private AudioSource source;
    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;

    public const float rayLength = 1.5f;
    private Animator animator;
    private bool isWalking = false;
    private bool isFacingRight = true;
    public int score = 0;
    int lives = 3;
    
    int keysFound = 0;
    const int keysNumber = 3;
    
    //GameManager gameManager;
    

    public ResultScrenS ResultScrenS;
    
    //public void GameOver()
   // {
    //    ResultScrenS.Setup(score);
    //}
  
    public void Death()
    {    
        lives -= 1;
    }



    private void Awake()
    {
        source = GetComponent<AudioSource>();

        startPosition = transform.position;

        rigidBody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Update is called once per frame
    void Update()
    {

        isWalking = false;
      


        if (Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.D))
        {
            if (isFacingRight == false)
            {
                Flip();
            }

            isWalking = true;
            transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow) ^ Input.GetKey(KeyCode.A))
        {
            if (isFacingRight == true)
            {
                Flip();
            }

            isWalking = true;
            transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //isWalking = false;
            Jump();
        }

       

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isWalking", isWalking);
        


        // Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 1, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonus") == true)

        {
            //score += 1;
            //Debug.Log("Score: " + score);

            source.PlayOneShot(bSound, AudioListener.volume);

            GameManager.instance.AddPoints(1);


            other.gameObject.SetActive(false);

        }

        if (other.CompareTag("Enemy") == true)
        {
            if (transform.position.y > other.gameObject.transform.position.y)
            {
               
                score += 1;
                Debug.Log("Killed an enemy! Score " + score);
            }
            else
            {
                GameManager.instance.RevokeLives(1);
                lives -= 1;
            }
            if (lives == 0)
            {
                SceneManager.LoadScene("DeathScreen");
            }
            else
            {
                Debug.Log("Remaining lives:" + lives);
            }
        }

        if (other.CompareTag("Key") == true)

        {
            source.PlayOneShot(gemSound, AudioListener.volume);

            GameManager.instance.AddKeys(Color.grey);
            Debug.Log("Gratulacje, znalazles klucz! Iloœæ kluczy: " + keysFound);
            other.gameObject.SetActive(false);
            keysFound += 1;

        }

        if (other.CompareTag("Heart") == true)

        {
            source.PlayOneShot(hSound, AudioListener.volume);

            GameManager.instance.AddLives(1);

            lives += 1;
            //Debug.Log("Gratulacje, znalazles dodatkowe zycie! Iloœæ zyc: " + lives);
            other.gameObject.SetActive(false);

        }




        if (other.tag == "FallDetection")
        {

            transform.position = startPosition;
            GameManager.instance.RevokeLives(1);
            lives -= 1;
            //SceneManager.LoadScene("DeathScreen");

            

        }
        if (lives == 0)
        {
            SceneManager.LoadScene("DeathScreen");
        }
        

        if (other.tag == "LvlEnd")
        {
            if (keysFound >= 3)
            {
                score = 100 * lives;
                GameManager.instance.LevelCompleted();
            }
            else
            {
                SceneManager.LoadScene("MissingKeys");
            }
        }

       
          if (other.CompareTag("MovingPlatform") == true)

          {
                transform.SetParent(other.transform);
          } 


    }

    void OnTriggerExit2D(Collider2D other)
    {
         transform.SetParent(null);   
    }


    void Jump()
    {
        if (IsGrounded() == true)
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //Debug.Log("jumping");
        }
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }
}