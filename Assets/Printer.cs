using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Printer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] tileArray = tilemap.GetTilesBlock(bounds);
        for (int index = 0; index < tileArray.Length; index++)
        {
            Tile tile = (Tile)tileArray[index];
            if(tile == null)
            {
                continue;
            }
            //print(tile.transform);
        }
        // Tilemap tilemap = (Tilemap)gameObject.GetComponent(typeof(Tilemap));
        // for(int i = 0; i < 100; i++)
        // {
        //     for(int j = 0; j < 100; j++)
        //     {
        //         if(tilemap.HasTile(new Vector3Int(i, j, 0)))
        //         {
        //             Debug.Log($"Tile at ({i},{j})");
        //         }
        //         else
        //         {
        //             Debug.Log($"No Tile at ({i},{j})");
        //         }
        //     }
        // }
        
        //Debug.Log(tilemap.GetSprite(new Vector3Int(0,0,0)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
