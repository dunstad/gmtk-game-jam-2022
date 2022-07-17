using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : BaseGameAgent
{

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Die()
    {
        anim.Play("dieRook");
        base.Die();
    }
}
