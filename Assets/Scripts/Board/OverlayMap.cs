using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayMap : MonoBehaviour
{
    private PossibleTileOutline[,] TileOverlays;
    private BadTileOutline[,] BadTileOverlays;
    public GameObject BadOverlayTile;
    public GameObject OverlayTile;
    public List<PossibleTileOutline> EnabledGoodTiles {get; private set;}
    public List<BadTileOutline> EnabledBadTiles {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        TileOverlays = new PossibleTileOutline[8,8];
        BadTileOverlays = new BadTileOutline[8,8];
        EnabledGoodTiles = new List<PossibleTileOutline>();
        EnabledBadTiles = new List<BadTileOutline>();
        for(int i = 0; i < 8; ++i)
        {
            for(int j = 0; j < 8; j++)
            {
                TileOverlays[i,j] = Instantiate(OverlayTile, new Vector3Int(i, j, 0), Quaternion.identity).GetComponent<PossibleTileOutline>();
                TileOverlays[i,j].Disable();
                GameObject gameObj = Instantiate(BadOverlayTile, new Vector3Int(i, j, 0), Quaternion.identity);
                BadTileOverlays[i,j] = gameObj.GetComponent<BadTileOutline>();
                BadTileOverlays[i,j].Disable();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableTiles(OverlayTileType tileType, List<Vector3Int> tilesToEnable)
    {
        foreach(Vector3Int tilePos in tilesToEnable)
        {
            if(tileType == OverlayTileType.Good)
            {
                PossibleTileOutline outline = TileOverlays[tilePos.x, tilePos.y];
                outline.Enable();
                EnabledGoodTiles.Add(outline);
            }
            else
            {
                BadTileOutline outline = BadTileOverlays[tilePos.x, tilePos.y];
                outline.Enable();
                EnabledBadTiles.Add(outline);
            }
        }
    }
    public void RemoveCurrentOutlines()
    {
        foreach(PossibleTileOutline enabledTile in EnabledGoodTiles)
        {
            enabledTile.Disable();
        }
        foreach(BadTileOutline enabledTile in EnabledBadTiles)
        {
            enabledTile.Disable();
        }
        
    }
    public bool GetTileState(OverlayTileType tileType, Vector3Int checkPos)
    {
        if(checkPos.x < 0 || checkPos.x > 7
            || checkPos.y < 0 || checkPos.y > 7)
        {
            return false;
        }
        if(tileType == OverlayTileType.Good)
        {
            return TileOverlays[checkPos.x, checkPos.y].TileState;
        }
        else
        {
            return BadTileOverlays[checkPos.x, checkPos.y].TileState;
        }
    }
}
public enum OverlayTileType
{
    Good = 0,
    Bad = 1,
}
