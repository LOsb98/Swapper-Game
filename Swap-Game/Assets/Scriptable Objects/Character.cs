using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Characters were going to have separate types (melee/projectile)
//It will be a lot easier to just implement "projectile" characters
//Melee can be represented by very short-range projectiles
//Like the sword beam from the original Legend of Zelda
[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    public Sprite sprite;
    public int health;
    public int speed;
    public float fireRate;
    public GameObject projectile;
}
