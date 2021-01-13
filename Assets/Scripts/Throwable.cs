using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private State state;

    public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    public Sprite[] spriteArray;
    public float MinXForce;
    public float MaxXForce;
    public float YForce;
    public Type type;

    private enum State
    {
        Playing,
        GameOver
    }

    public enum Type
    {
        SlowMotion,
        BiggerPlatform,
        Mattress,
        Obstacle
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        InitSpriteDict();
    }

    void Start()
    {
        state = State.Playing;
        Level.OnGameOver += GameOver;
        Init();
    }

    private void Init()
    {
        float xForce = Random.Range(MinXForce, MaxXForce);
        rb.AddForce(new Vector2(xForce, YForce), 0f);
    }

    public void SetType(string type)
    {

        switch (type)
        {
            case "SlowMotion":
                this.type = Type.SlowMotion;
                break;
            case "BiggerPlatform":
                this.type = Type.BiggerPlatform;
                break;
            case "Obstacle":
                this.type = Type.Obstacle;
                break;
            case "Mattress":
                this.type = Type.Mattress;
                break;
        }

        sr.sprite = sprites[type];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(state == State.Playing)
        {
            if (collision.tag == "Player")
            {
                if(type != Type.Obstacle)
                {
                    Level.GetInstance().ApplyPowerUp(type.ToString());
                    Destroy(gameObject);
                }
                else
                {
                    Fireman.GetInstance().HurtFireman();
                    Destroy(gameObject);
                }
            }
            else if (collision.tag == "Ground")
            {
                Destroy(gameObject);
            }
        }
    }


    private void GameOver(bool gameOver)
    {
        state = State.GameOver;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnDestroy()
    {
        Level.OnGameOver -= GameOver;
    }

    private void InitSpriteDict()
    {
        sprites.Add("Mattress", spriteArray[0]);
        sprites.Add("BiggerPlatform", spriteArray[1]);
        sprites.Add("Obstacle", spriteArray[2]);
        sprites.Add("SlowMotion", spriteArray[3]);
    }
}
