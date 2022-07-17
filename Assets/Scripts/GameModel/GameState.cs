using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int TurnCount { get; private set;}
    public List<PawnModel> playerCharacters { get; set; }
    public List<BaseEnemyState> enemies { get; set; }
    
    public event Action<List<DieFace>> onDiceRolled;
    // Start is called before the first frame update
    void Start()
    {
        TurnCount = 0;
        playerCharacters = new List<PawnModel>();
        enemies = new List<BaseEnemyState>() {

        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn()
    {
        List<DieFace> playerRolls = new List<DieFace>();
        foreach(PawnModel pawn in playerCharacters)
        {
            DieFace dieFace = pawn.Roll();
            playerRolls.Add(dieFace);
        }
        onDiceRolled(playerRolls);
    }
}
