using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public float _speed;

    public void Move(Vector2 moveVector)
    {
        //Normalizing the Vector2 removes the ability to move faster while holding two movement keys at once
        _rigidbody.velocity = moveVector.normalized * _speed;

        if (moveVector.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        else if (moveVector.x > 0) transform.localScale = new Vector3(1, 1, 1);
    }
}
