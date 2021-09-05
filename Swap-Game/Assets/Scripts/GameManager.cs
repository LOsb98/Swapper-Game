using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerIcon;

    [SerializeField] private float _score;

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public GameObject CurrentPlayer { get; set; }


    //An error comes up if this is assigned to the first instance of the Character object
    //i.e. If this =  Character, the error comes up
    //If this = Character(1), no error
    //The game still runs as normal regardless (it seems)


    void Start()
    {
        //This needs to be done in Start() and not Awake()
        //The execution order will try and call SetPlayer() on the CharacterManager before it has finished getting all its compoment references
        //It will throw an error + the PlayerIcon will not track the player (only in the build version?)
        SetNewPlayer(CurrentPlayer);
    }

    void Update()
    {
        //Increment timers for spawning enemies
        _playerIcon.transform.position = CurrentPlayer.transform.position;
    }
    
    public void SetNewPlayer(GameObject newPlayer)
    {
        var currentCharManager = CurrentPlayer.GetComponent<CharacterManager>();
        currentCharManager.SetPlayerControl(false);

        var newCharManager = newPlayer.GetComponent<CharacterManager>();
        newCharManager.SetPlayerControl(true);

        CurrentPlayer = newPlayer;
    }

    public void AddScore(int score)
    {

    }

    public void EndGame()
    {
        //Logic for ending the game
    }
}
