using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private State state;
    private float jumpTimer;
    
    
    public float minForce;
    public float maxForce;
    public Sprite[] sprites;


    //enum to know if the person has died or is still alive
    private enum State {
        Alive,
        Dead
        }
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        int spriteIdx = Level.GetInstance().GetPeopleSpawned() % sprites.Length;
        sr.sprite = sprites[spriteIdx];
        Level.OnGameOver += Die;
        Init();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(jumpTimer<= 0)
            {
                Jump();
            }
        }else if(collision.tag == "Ambulance")
        {
            Finish(1);
        }else if(collision.tag == "Ground")
        {
            Level.GetInstance().GameOver();
        }

        jumpTimer -= Time.deltaTime;
    }

    //Gives initial force with random value
    private void Init()
    {
        state = State.Alive;
        float initForce = Random.Range(minForce, maxForce);
        rb.AddForce(Vector2.right * initForce, 0f);
        rb.AddTorque(-initForce * 0.3f);
        jumpTimer = 0f;
    }

    private void Jump()
    {
        float positionDiff = transform.position.x - Fireman.GetInstance().GetPosition().x;
        float xSize = transform.localScale.x * 0.6f;
        if(positionDiff<= xSize && positionDiff >= -xSize)
        {
            rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x), -rb.velocity.y);
        }else if (positionDiff > xSize)
        {
            rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x) * -0.9f, -rb.velocity.y * 0.75f);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x) * 1.1f, -rb.velocity.y * 1.15f);
        }
    }


    private void Finish(int score)
    {
       
        Level.GetInstance().ChangeScore(score);
        
        Destroy(gameObject);
    }

    //Makes the person stop moving. Called from the OnGameOver event
    private void Die(bool died)
    {
        state = State.Dead;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnDestroy()
    {
        Level.OnGameOver -= Die;
    }
}
