using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private Grid grid;

    [SerializeField] public Tilemap boardMap = null;

    [SerializeField] private Tilemap collisionMap = null;

    [SerializeField] private Tilemap overlayTileMap = null;

    [SerializeField] private Tile hoverTile = null;
    
    //[SerializeField] private RuleTile pathTile = null;

    private Vector3Int previousMousePos = new Vector3Int();
    
    public BoardState BoardState {get; set;}
    public GameState gameState { get; set; }

    private OverlayMap overlayMap {get; set;}    

    public InsurgentPawn SelectedPawn { get; set; }
    public Action<Vector3Int> SelectedAction { get; set; }
    public bool playerConsideringAction {get; private set;} = false;
    private bool actionConfirmed = false;
    private Vector3Int previousMouseClickPos = new Vector3Int(-99, -99, -99);

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
    }
    public void Initialize()
    {
        BoardState = boardMap.GetComponent<BoardState>();
        gameState = gameObject.GetComponent<GameState>();
        overlayMap = overlayTileMap.GetComponent<OverlayMap>();
    }
    // Update is called once per frame
    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        // if (!mousePos.Equals(previousMousePos)) {

        //     overlayMap.SetTile(previousMousePos, null); // Remove old hoverTile
        //     overlayMap.SetTile(mousePos, hoverTile);
        //     previousMousePos = mousePos;
        // }

        // Left mouse click -> Modal based on context of what is being selected
        if (Input.GetMouseButton(0) && !MouseClickPosMatchesPrevious(mousePos)) 
        {
            previousMouseClickPos = mousePos;
            IGameAgent agent = BoardState.Knock_Knock(mousePos);
            InsurgentPawn clickedPawn = agent as InsurgentPawn;
            if(clickedPawn != null && clickedPawn != SelectedPawn)
            {
                overlayMap.RemoveCurrentOutlines();
                SelectedPawn = agent as InsurgentPawn;
                overlayMap.EnableTiles(OverlayTileType.Good, new List<Vector3Int>() { mousePos });
            }
            else if(SelectedAction != null && SelectedPawn != null && overlayMap.GetTileState(OverlayTileType.Good, mousePos))
            {
                ConfirmAction(mousePos);
            }
            else if(mousePos.x < 8 && mousePos.x >= 0
                    && mousePos.y < 8 && mousePos.y >= 0)
            {
                Debug.Log($"Clicked the board on not a valid slot at ({mousePos.x}, {mousePos.y}), cancelling action and selection");
                SelectedAction = null;
                SelectedPawn = null;
                overlayMap.RemoveCurrentOutlines();
            }
            // if(!playerConsideringAction && SelectedPawn != null)
            // {
            //     playerConsideringAction = true;
            //     IGameAgent agent = BoardState.Knock_Knock(mousePos);
            //     if(agent is InsurgentPawn)
            //     {
            //         SelectedPawn = agent as InsurgentPawn;
            //         Debug.Log($"SelectedPawn = {SelectedPawn}");
            //         overlayMap.EnableTiles(new List<Vector3Int>() { mousePos });
            //     }
            // }
            // else
            // {
            //     if(!actionConfirmed && overlayMap.GetTileState(mousePos))
            //     {
            //         actionConfirmed = true;
            //         playerConsideringAction = false;
            //         ConfirmAction(mousePos);
            //     }
            // }
            //todo: left click logic for selecting which character to move
        }
        // Right mouse click -> ????
        if (Input.GetMouseButton(1)) 
        {
            //todo: right click logic for ...
        }

    }
    private bool MouseClickPosMatchesPrevious(Vector3Int mousePos)
    {
        if(mousePos.x == previousMouseClickPos.x && mousePos.y == previousMouseClickPos.y)
        {
            return true;
        }
        return false;
    }
    private void ConfirmAction(Vector3Int actionTarget)
    {
        if(SelectedAction == null || SelectedPawn == null)
        {
            return;
        }
        Debug.Log($"CONFIRM ACTION -- SelectedAction = {SelectedAction}; SelectedPawn = {SelectedPawn}");
        SelectedAction.Invoke(actionTarget);
        overlayMap.RemoveCurrentOutlines();
        SelectedAction = null;
        SelectedPawn = null;
        //also need to confirm a die use
    }
    private void MoveAction(Vector3Int actionTarget)
    {
        SelectedPawn.MoveTo(actionTarget);
        BoardState.ParentGameState.moveCount--;
    }
    private void BarricadeAction(Vector3Int actionTarget)
    {
        // place a barricade
    }
    private void AttackAction(Vector3Int actionTarget)
    {
        BoardState.ParentGameState.attackCount--;
        BaseGameAgent gameAgent = BoardState.Knock_Knock(actionTarget) as BaseGameAgent;
        gameAgent.Die();
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
    public void PreviewBarricade()
    {
        if(SelectedPawn == null)
        {
            return;
        }
        if(SelectedAction == BarricadeAction)
        {
            Debug.Log("You are already previewing a Move action for this Pawn");
            return;
        }
        SelectedAction = BarricadeAction;
        List<Vector3Int> possibleMoves = new List<Vector3Int>();
        Vector3Int centerPos = SelectedPawn.Position;

        int startXIdx = centerPos.x - 1 < 0 ? 0 : centerPos.x - 1;
        int startYIdx = centerPos.y - 1 < 0 ? 0 : centerPos.y - 1;
        int endXIdx = centerPos.x + 1 > 7 ? 7 : centerPos.x + 1;
        int endYIdx = centerPos.y + 1 > 7 ? 7 : centerPos.y + 1;
        for(int i = startXIdx; i <= endXIdx; ++i)
        {
            for(int j = startYIdx; j <= endYIdx; ++j)
            {
                Vector3Int evalPos = new Vector3Int(i, j, 0);
                if(BoardState.Knock_Knock(evalPos) == null)
                {
                    possibleMoves.Add(evalPos);
                }
            }
        }
        overlayMap.EnableTiles(OverlayTileType.Good, possibleMoves);
    }
    public void PreviewAttack()
    {
        if(SelectedPawn == null)
        {
            return;
        }
        if(SelectedAction == AttackAction)
        {
            Debug.Log("You are already previewing a Move action for this Pawn");
            return;
        }
        SelectedAction = AttackAction;
        List<Vector3Int> possibleMoves = new List<Vector3Int>();
        List<Vector3Int> blockedMoves = new List<Vector3Int>();
        Vector3Int centerPos = SelectedPawn.Position;

        List<Vector3Int> candidatePositions = new List<Vector3Int>()
        {
            new Vector3Int(centerPos.x - 1, centerPos.y - 1, 0),
            new Vector3Int(centerPos.x - 1, centerPos.y + 1, 0),
            new Vector3Int(centerPos.x + 1, centerPos.y - 1, 0),
            new Vector3Int(centerPos.x + 1, centerPos.y + 1, 0),
        };

        foreach(Vector3Int candidate in candidatePositions)
        {
            IGameAgent agent = BoardState.Knock_Knock(candidate);
            if(!IsPosInGridBounds(candidate) || agent == null)
            {
                blockedMoves.Add(candidate);
            }
            if(gameState.WhosSideAreYouOn(agent) == Faction.AgentOfMonarchy)
            {
                possibleMoves.Add(candidate);
            }
        }
        overlayMap.EnableTiles(OverlayTileType.Good, possibleMoves);
        overlayMap.EnableTiles(OverlayTileType.Bad, blockedMoves);
    }
    public void PreviewMove()
    {
        int moveCount = BoardState.ParentGameState.moveCount;

        if(moveCount < 1)
        {
            Debug.Log("no moves left");
            return;
        }
        if(SelectedPawn == null)
        {
            return;
        }
        if(SelectedAction == MoveAction)
        {
            Debug.Log("You are already previewing a Move action for this Pawn");
            return;
        }
        SelectedAction = MoveAction;
        List<Vector3Int> possibleMoves = new List<Vector3Int>();
        Vector3Int centerPos = SelectedPawn.Position;

        int startXIdx = centerPos.x - 1 < 0 ? 0 : centerPos.x - 1;
        int startYIdx = centerPos.y - 1 < 0 ? 0 : centerPos.y - 1;
        int endXIdx = centerPos.x + 1 > 7 ? 7 : centerPos.x + 1;
        int endYIdx = centerPos.y + 1 > 7 ? 7 : centerPos.y + 1;
        for(int i = startXIdx; i <= endXIdx; ++i)
        {
            for(int j = startYIdx; j <= endYIdx; ++j)
            {
                Vector3Int evalPos = new Vector3Int(i, j, 0);
                if(BoardState.Knock_Knock(evalPos) == null)
                {
                    possibleMoves.Add(evalPos);
                }
            }
        }
        overlayMap.EnableTiles(OverlayTileType.Good, possibleMoves);
    }
    public bool IsPosInGridBounds(Vector3Int checkPos)
    {
        if(checkPos.x > 7 || checkPos.x < 0
            || checkPos.y > 7 || checkPos.y < 0)
        {
            return false;   
        }
        return true;
    }
}
