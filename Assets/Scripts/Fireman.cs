using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireman : MonoBehaviour
{
    private Rigidbody2D rb;
    private State state;
    private static Fireman instance;
    private SpriteRenderer sr;
    private bool hurt;
    private Transform explosionInstance;
    private int spriteIdx;

    public Sprite[] Skins;
    public GameObject HurtOverlay;
    public Transform Explosion;

    private enum State
    {
        Playing,
        GameOver
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        spriteIdx = SaveData.GetInstance().GetSpriteIdx();
        sr.sprite = Skins[spriteIdx];
        state = State.Playing;
        hurt = false;
        Level.OnGameOver += GameOver;
    }

    void Update()
    {
        
        if(state == State.Playing)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                float newPos = Mathf.Clamp(mousePos.x, -7f, 4.5f);
                rb.position = new Vector2(newPos, rb.position.y);
            }
        }
    }

    private void GameOver(bool over)
    {
        state = State.GameOver;
    }

    private void OnDestroy()
    {
        Level.OnGameOver -= GameOver;
        
    }

    public void ChangeScale(float scale)
    {
        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void HurtFireman()
    {
        if (hurt)
        {
            Level.GetInstance().GameOver();
        }
        else
        {
            hurt = true;
            explosionInstance = Instantiate(Explosion, transform);
            explosionInstance.position = transform.position;
            Instantiate(HurtOverlay, transform);
            Destroy(explosionInstance.gameObject, 1f);
        }

    }

    public static Fireman GetInstance()
    {
        return instance;
        
    }
}
