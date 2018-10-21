using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{

    public float speed;
    public float wallHitWidth;
    public float wallHitHeight;
    public LayerMask isGround;
    public Transform wallHitBox;

    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        //wallHit is a bool similar to the ground one in the player code.
        //Physics2D.OverlapBox is similar to Physics2D.OverlapCircle but uses a box
        //The next is a Vector 2 with the box's Width and Height which are floats that I made public so I could edit them in the editor. 
        //The zero is the z value we don't need.
        //isGround is a LayerMask of everything that is ground.

        bool wallHit;  
        wallHit = Physics2D.OverlapBox(wallHitBox.position, new Vector2(wallHitWidth, wallHitHeight), 0, isGround);
        if (wallHit == true)
        {
            speed = speed * -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        bool playerHit = collision.gameObject.transform.position.y > transform.position.y +1; 
        if (collision.collider.tag == "Player" && playerHit == true)
        {
            anim.SetBool("isDead", true);
            Debug.Log("Goomba dead");
            Destroy(gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }


    }
}
