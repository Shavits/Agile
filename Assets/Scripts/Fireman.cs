using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireman : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            float newPos = Mathf.Clamp(mousePos.x, -7f, 2.5f);
            rb.position = new Vector2(newPos, rb.position.y);
        }
    }
}
