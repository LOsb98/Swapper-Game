using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base class used for the player and AI inputs
//The CharacterManager will switch between each
public abstract class InputBase : MonoBehaviour
{
    //Step() is essentially the update function for the input classes
    public abstract void Step();
    //When the player dies the game ends, when an AI dies the score is increased
    public abstract void Die();
    private GameManager gameManager;
    protected Attack attack;
    protected Movement movement;

    void Start()
    {
        //FindObjectWithTag() is better than GameObject.Find() as it doesn't need the name of the object, which may change later on
        //FindObjectOfType() is apparently very slow
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        attack = GetComponent<Attack>();
        movement = GetComponent<Movement>();
    }
}
