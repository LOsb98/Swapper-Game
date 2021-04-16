using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float score;
    //An error comes up if this is assigned to the first instance of the Character object
    //i.e. If this =  Character, the error comes up
    //If this = Character(1), no error
    //The game still runs as normal regardless (it seems)
    public GameObject currentPlayer;
    public GameObject playerIcon;

    void Start()
    {
        //This needs to be done in Start() and not Awake()
        //The execution order will try and call SetPlayer() on the CharacterManager before it has finished getting all its compoment references
        //It will throw an error + the PlayerIcon will not track the player (only in the build version?)
        currentPlayer.GetComponent<CharacterManager>().IsPlayer = true;
    }

    void Update()
    {
        //Increment timers for spawning enemies
        playerIcon.transform.position = currentPlayer.transform.position;
    }
    
    public void EndGame()
    {
        //Logic for ending the game
    }
}
