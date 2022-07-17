using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFace : MonoBehaviour
{
    public delegate List<Vector3Int> PreviewAction(Vector3Int currentPos);
    public delegate GameState ExecuteAction(GameState gs);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
