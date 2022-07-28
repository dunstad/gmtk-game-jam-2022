using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IGameAgent
{
    Vector3Int Position { get; }

    void MoveTo(Vector3Int newPos);
    void Attack(Vector3Int direction);
    
    // start death animation
    void Die();
    
    // finish removing the thing after it dies, called from animation event
    void FinishDying();
    // get game object handle
    UnityEngine.Object GetGameObject();   
    event Action<IGameAgent, Vector3Int, Vector3Int> onMove;

}
