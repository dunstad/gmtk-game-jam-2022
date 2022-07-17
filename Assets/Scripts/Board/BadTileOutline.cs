using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTileOutline : MonoBehaviour
{
    // Start is called before the first frame update
    public bool TileState = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Enable() 
    {
        TileState = true;
        gameObject.SetActive(TileState);
    }
    public void Disable()
    {
        TileState = false;
        gameObject.SetActive(TileState);
    }
}
