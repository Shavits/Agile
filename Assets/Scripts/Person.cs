using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private State state;
    
    
    public float jumpMulti = 0.75f;
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
            Jump();
        }else if(collision.tag == "Ambulance")
        {
            Finish(1);
        }else if(collision.tag == "Ground")
        {
            Level.GetInstance().GameOver();
        }
    }

    //Gives initial force with random value
    private void Init()
    {
        state = State.Alive;
        float initForce = Random.Range(minForce, maxForce);
        rb.AddForce(Vector2.right * initForce, 0f);
        rb.AddTorque(-initForce * 0.3f);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y * jumpMulti);
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
        Debug.Log("died");
    }

    private void OnDestroy()
    {
        Level.OnGameOver -= Die;
    }
}
