using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private GridController Grid;

    public int TurnCount { get; private set;}
    public List<PawnModel> playerCharacters { get; set; }
    public List<BaseEnemyState> enemies { get; set; }
    
    public event Action<List<DieFace>> onDiceRolled;

    [SerializeField] DiceBox diceBox;

    public int moveCount;
    public int attackCount;

    // Start is called before the first frame update
    void Start()
    {
        TurnCount = 0;
        playerCharacters = new List<PawnModel>();
        enemies = new List<BaseEnemyState>();

        diceBox.SubscribeToRollEvent(this);
    }
    public void Initialize() 
    {

        SetupLevel(1);

        List<IGameAgent> agentsOfMonarchy = Grid.boardMap.GetComponent<BoardState>().AgentsOfMonarchy;
        foreach (IGameAgent agent in agentsOfMonarchy)
        {
            
        }

        List<IGameAgent> insurgentPawns = Grid.boardMap.GetComponent<BoardState>().InsurgentPawns;
        foreach (IGameAgent agent in insurgentPawns)
        {
            BaseGameAgent baseGameAgent = (BaseGameAgent) agent;
            playerCharacters.Add(baseGameAgent.gameObject.GetComponent<PawnModel>());
        }

        // StartTurn();
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
            if (pawn.BoardState.alive)
            {
                DieFace dieFace = pawn.Roll();
                playerRolls.Add(dieFace);
            }
        }

        moveCount = 0;
        foreach (DieFace dieFace in playerRolls)
        {
            if (dieFace.GetType() == typeof(MoveFace))
            {
                moveCount++;
            }
            if (dieFace.GetType() == typeof(AttackFace))
            {
                attackCount++;
            }
        }

        onDiceRolled(playerRolls);
    }
    public Faction WhosSideAreYouOn(IGameAgent gameAgent)
    {
        BoardState bs = Grid.boardMap.GetComponent<BoardState>();
        List<IGameAgent> agentsOfMonarchy = bs.AgentsOfMonarchy;
        List<IGameAgent> insurgents = bs.InsurgentPawns;
        Faction agentFaction;
        if(insurgents.Count < agentsOfMonarchy.Count)
        {
            agentFaction = insurgents.Contains(gameAgent) ? Faction.Insurgent : Faction.AgentOfMonarchy;
        }
        else
        {
            agentFaction = agentsOfMonarchy.Contains(gameAgent) ? Faction.AgentOfMonarchy : Faction.Insurgent;
        }
        
        return agentFaction;
    }
    public void EndTurn()
    {
        TurnCount++;
        Debug.Log($"Ending the turn, enemies move and proceed to turn {TurnCount}");

        BoardState bs = Grid.boardMap.GetComponent<BoardState>();
        List<IGameAgent> agentsOfMonarchy = bs.AgentsOfMonarchy;
        foreach(BaseGameAgent agent in agentsOfMonarchy)
        {
            // try three times to get a random position on the board
            Vector3Int newPos = new Vector3Int(0, 0, 0);
            for (int tries = 3; tries > 0; tries--)
            {
                Vector3Int randomOffset = new Vector3Int(UnityEngine.Random.Range(-1, 2), UnityEngine.Random.Range(-1, 2), 0);
                newPos = agent.Position + randomOffset;
                if (Grid.IsPosInGridBounds(newPos))
                {
                    tries = 0;
                }
            }

            if (bs.Knock_Knock(newPos) == null && Grid.IsPosInGridBounds(newPos))
            {
                agent.MoveTo(newPos);
            } else if (!agentsOfMonarchy.Contains(bs.Knock_Knock(newPos)) && Grid.IsPosInGridBounds(newPos))
            {
                agent.Attack(newPos);
                BaseGameAgent attackTarget = (BaseGameAgent) bs.Knock_Knock(newPos);
                attackTarget.Die();

                bool livingPawn = false;
                List<IGameAgent> insurgentPawns = Grid.boardMap.GetComponent<BoardState>().InsurgentPawns;
                foreach(IGameAgent pawn in insurgentPawns)
                {
                    BaseGameAgent bga = (BaseGameAgent) pawn;
                    if (bga.alive)
                    {
                        livingPawn = true;
                    }
                }
                if (!livingPawn) {
                    SceneManager.LoadScene(0);
                }
            }
        }

        GameObject[] dice = GameObject.FindGameObjectsWithTag("Die");
        foreach(GameObject die in dice)
        {
            Destroy(die);
        }

        StartTurn();
    }
}
public enum Faction
{
    AgentOfMonarchy = 0,
    Insurgent = 1
}