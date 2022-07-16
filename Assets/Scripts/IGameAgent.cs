using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameAgent
{
    Vector3Int Position { get; }

    void MoveTo(Vector3Int newPos);

}
