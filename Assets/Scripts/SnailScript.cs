using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D myBody;
    private Animator anim;

    private bool moveLeft;
    private bool canMove;
    private bool stunned;


    public Transform leftCollison, rightCollison, topCollison, down_Collison;
    public LayerMask playerLayer;
    private Vector3 leftCollisonPosition, rightCollidsonPositon;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveLeft = true;

        leftCollisonPosition = leftCollison.position;
        rightCollidsonPositon = rightCollison.position;
    }



    // Start is called before the first frame update
    void Start()
    {
  
        canMove = true;
        moveLeft = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }

        }

        CheckedCollision();
    }

    void CheckedCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollison.position, Vector2.left, 0.1f,playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollison.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollison.position, 0.2f, playerLayer);

        if(topHit != null)
        {
            if(topHit.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2
                        (topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);

                   anim.Play("Stunned");
                    stunned = true;

                    //beetle code

                    if(tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(3f));
                    }

                }
            }
        }

        if (leftHit)
        {
            if(leftHit.collider.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    //apply damage to player
                }else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {

                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == "Player")
            {
                if (!stunned)
                {
                    //apply damage to player
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {

                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                    }
                }
            }
        }

        //if we don't detect collision
        if (!Physics2D.Raycast(down_Collison.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }


    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollisonPosition = leftCollison.position;
            rightCollidsonPositon = rightCollison.position;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            leftCollisonPosition = rightCollison.position;
            rightCollidsonPositon = leftCollison.position;
        }

        transform.localScale = tempScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "snail")
        {
            anim.Play("Stunned");

            if(tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");
                StartCoroutine(Dead(3f));
            }
        }
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.BULLET_TAG)
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");

                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }

            if(tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }


}//class
