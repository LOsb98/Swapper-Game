using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Characters were going to have separate types (melee/projectile)
//It will be a lot easier to just implement "projectile" characters
//Melee can be represented by very short-range projectiles
//Like the sword beam from the original Legend of Zelda
[CreateAssetMenu(menuName = "Characters/New Character")]
public class Character : ScriptableObject
{
    public Sprite _sprite;
    public int _health;
    public int _speed;
    public float _fireRate;
    public GameObject _projectile;
    public Vector2 _size;

    [Range(1f, 100f)]
    public int _spawnRate;
}
