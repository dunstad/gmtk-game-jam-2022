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
        InsurgentPawns.Add((IGameAgent)newPawn.GetComponent<InsurgentPawn>());
    }
    void SpawnEnemy(GameObject enemy, Vector3Int pos)
    {
        Instantiate(enemy, pos, Quaternion.identity);
        AgentsOfMonarchy.Add((IGameAgent)enemy.GetComponent<Rook>());
    }
}
