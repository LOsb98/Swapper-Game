using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveVector)
    {
        //Normalizing the Vector2 removes the ability to move faster while holding two movement keys at once
        rb.velocity = moveVector.normalized * speed;
    }
}
