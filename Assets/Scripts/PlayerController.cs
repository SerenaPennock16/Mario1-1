using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private bool facingRight = true;

    public float speed;
    public float jumpforce;

    public Text countText;
    private int count;

    //ground check
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    //private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;

    private AudioSource source;
    public AudioClip jumpClip;
    public AudioClip coinClip;
    public AudioClip powerClip;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;




    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText();

    }

    void Awake()
    {

        source = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        Debug.Log(isOnGround);



        //stuff I added to flip my character
        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        Debug.Log(rb2d.velocity);

        if (collision.collider.tag == "Ground" && isOnGround)
        {


            if (Input.GetKey(KeyCode.UpArrow))
            {

                rb2d.velocity = Vector2.up * jumpforce;




                float vol = Random.Range(volLowRange, volHighRange); source.PlayOneShot(jumpClip);
                //GetComponent<AudioSource>().PlayOneShot(jumpClip);



            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

            GetComponent<AudioSource>().PlayOneShot(coinClip);
        }
        if (other.gameObject.CompareTag("Mushroom"))
        {
            other.gameObject.SetActive(false);

            GetComponent<AudioSource>().PlayOneShot(powerClip);
        }
    }

    void SetCountText()
    {
        countText.text = "Coins: " + count.ToString();
    }

}
