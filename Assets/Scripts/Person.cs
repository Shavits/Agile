using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpMulti = 0.75f;
    public float minForce;
    public float maxForce;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Finish(-1);
        }
    }

    public void Init()
    {
        float initForce = Random.Range(minForce, maxForce);
        rb.AddForce(Vector2.right * initForce, 0f);
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
}
