using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private Grid grid;

    [SerializeField] public Tilemap boardMap = null;

    [SerializeField] private Tilemap collisionMap = null;

    [SerializeField] private Tilemap overlayMap = null;

    [SerializeField] private Tile hoverTile = null;
    
    //[SerializeField] private RuleTile pathTile = null;

    private Vector3Int previousMousePos = new Vector3Int();
    public BoardState BoardState {get; set;}

    public InsurgentPawn SelectedPawn { get;set; }



    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }
    public void Initialize()
    {
        BoardState = boardMap.GetComponent<BoardState>();
        Debug.Log(BoardState);
    }
    // Update is called once per frame
    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos)) {

            overlayMap.SetTile(previousMousePos, null); // Remove old hoverTile
            overlayMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }

        // Left mouse click -> Modal based on context of what is being selected
        if (Input.GetMouseButton(0)) 
        {
            //todo: left click logic for selecting which character to move
        }
        // Right mouse click -> ????
        if (Input.GetMouseButton(1)) 
        {
            //todo: right click logic for ...
        }

    }

    Vector3Int GetMousePosition () 
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    public void SpawnEntity(AgentType agentType, Vector3Int gridPos)
    {
        if(agentType == AgentType.None)
        {
            return;
        }
        BoardState.Spawn(agentType, gridPos);
    }
}
