using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDictionary : MonoBehaviour
{
    // Int arrays initialize the levels:
    // 0 means nothing spawns here
    // 1 means an insurgent pawn spawns here
    // 2 means an enemy pawn spawns here
    // 3 means a knight spawns here
    // 4 means a rook spawns here
    // 5 means a bishop spawns here
    // 6 means a queen spawns here
    // 7 means a king spawns here
    public static Dictionary<int, int[,]> Levels = new Dictionary<int, int[,]>()
    {
        { 0, new int[8,8] {
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        } },
        { 1, new int[8,8] {
            { 0, 0, 3, 4, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
        } },
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum AgentType 
{
    None = 0,
    InsurgentPawn = 1,
    EnemyPawn = 2,
    Knight = 3,
    Rook = 4,
    Bishop = 5,
    Queen = 6,
    King = 7
}
