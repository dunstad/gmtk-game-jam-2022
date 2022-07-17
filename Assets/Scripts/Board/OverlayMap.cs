using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayMap : MonoBehaviour
{
    private PossibleTileOutline[,] TileOverlays = new PossibleTileOutline[8,8];
    public GameObject OverlayTile;
    public List<PossibleTileOutline> EnabledTiles {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        EnabledTiles = new List<PossibleTileOutline>();
        for(int i = 0; i < 8; ++i)
        {
            for(int j = 0; j < 8; j++)
            {
                TileOverlays[i,j] = Instantiate(OverlayTile, new Vector3Int(i, j, 0), Quaternion.identity).GetComponent<PossibleTileOutline>();
                TileOverlays[i,j].Disable();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableTiles(List<Vector3Int> tilesToEnable)
    {
        foreach(Vector3Int tilePos in tilesToEnable)
        {
            PossibleTileOutline outline = TileOverlays[tilePos.x, tilePos.y];
            outline.Enable();
            EnabledTiles.Add(outline);
        }
    }
    public void RemoveCurrentOutlines()
    {
        foreach(PossibleTileOutline enabledTile in EnabledTiles)
        {
            enabledTile.Disable();
        }
    }
}
