using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : MonoBehaviour, IGameAgent
{
    public Vector3Int Position 
    { 
        get 
        {
            Vector3 pos = gameObject.transform.position;
            return new Vector3Int((int)pos.x, (int)pos.y, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector3Int newPos)
    {
        gameObject.transform.position = newPos;
    }
}
