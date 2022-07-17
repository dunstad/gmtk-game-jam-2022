using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState : MonoBehaviour
{
    public List<IGameAgent> AgentsOfMonarchy { get; private set; } = new List<IGameAgent>();
    public List<IGameAgent> InsurgentPawns { get; private set; } = new List<IGameAgent>();
    public GameObject PlayerPawn;
    public GameObject Rook;
    private int frameCount = 0;
    public int Width { get; private set; }
    public int Height { get; private set; }
    private IGameAgent[,] TileOccupant = new IGameAgent[8,8];

    // Start is called before the first frame update
    void Start()
    {
        SetupBoard();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetupBoard()
    {
        PawnSpawn(new Vector3Int(0, 0, 0));
        PawnSpawn(new Vector3Int(2, 0, 0));
        SpawnEnemy(Rook, new Vector3Int(3, 5, 0));
        SpawnEnemy(Rook, new Vector3Int(5, 5, 0));
    }
    void PawnSpawn(Vector3Int pos)
    {
        GameObject newPawn = Instantiate(PlayerPawn, pos, Quaternion.identity);
        IGameAgent gameAgent = (IGameAgent)newPawn.GetComponent<InsurgentPawn>();
        TileOccupant[pos.x, pos.y] = gameAgent;
        gameAgent.onMove += MoveAgent;
        InsurgentPawns.Add(gameAgent);
    }
    void SpawnEnemy(GameObject enemy, Vector3Int pos)
    {
        Instantiate(enemy, pos, Quaternion.identity);
        IGameAgent gameAgent = enemy.GetComponent<IGameAgent>();
        TileOccupant[pos.x, pos.y] = gameAgent;
        gameAgent.onMove += MoveAgent;
        AgentsOfMonarchy.Add(gameAgent);
    }
    // Checks who is at a specific 
    public IGameAgent Knock_Knock(Vector3Int checkPos) // who's there?!
    {
        return TileOccupant[checkPos.x, checkPos.y];
    }
    public void MoveAgent(IGameAgent agent, Vector3Int oldPos, Vector3Int newPos)
    {
        IGameAgent oldPosOccupant = TileOccupant[oldPos.x, oldPos.y];
        if(oldPosOccupant != agent)
        {
            Debug.Log($"BoardState cannot move agent ({agent}) from position ({oldPos.x}, {oldPos.y}) because it is not the current occupant.  Current Occupant is {oldPosOccupant}.");
            return;
        }
        IGameAgent newPosOccupant = TileOccupant[newPos.x, newPos.y];
        if(newPosOccupant != null)
        {
            Debug.Log($"Cannot move to position ({newPos.x}, {newPos.y}) because it is already occupied by");
            return;
        }
        TileOccupant[oldPos.x, oldPos.y] = null;
        TileOccupant[newPos.x, newPos.y] = agent;
        Debug.Log($"Moved {agent} from {oldPos} to {newPos}");
    }
}
