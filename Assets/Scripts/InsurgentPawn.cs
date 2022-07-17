using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsurgentPawn : BaseGameAgent
{
    private PawnModel Model;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        Model = gameObject.GetComponent<PawnModel>();
        //MoveTo(new Vector3Int(5, 6, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
