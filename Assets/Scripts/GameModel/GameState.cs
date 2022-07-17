using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private GridController Grid;

    public int TurnCount { get; private set;}
    public List<PawnModel> playerCharacters { get; set; }
    public List<BaseEnemyState> enemies { get; set; }
    
    public event Action<List<DieFace>> onDiceRolled;
    // Start is called before the first frame update
    void Start()
    {
        TurnCount = 0;
        playerCharacters = new List<PawnModel>();
        enemies = new List<BaseEnemyState>();
    }
    public void Initialize() 
    {
        SetupLevel(1);
    }
    // Sets up a level from the level number
    void SetupLevel(int level)
    {
        int[,] agentLocations = LevelDictionary.Levels[level];
        for(int i = 0; i < 8; ++i)
        {
            for(int j = 0; j < 8; j++)
            {
                AgentType type = (AgentType)agentLocations[i,j];
                if(type != AgentType.None)
                {
                    Debug.Log($"Found {type} at i = {i}; j = {j}");
                    Debug.Log($"Putting {type} at x = {j}, y = {8-i}");
                }
                Grid.SpawnEntity(type, new Vector3Int(j, 7-i, 0));
            }
        }
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
    public void EndTurn()
    {
        TurnCount++;
        Debug.Log($"Ending the turn, enemies move and proceed to turn {TurnCount}");
    }
}
