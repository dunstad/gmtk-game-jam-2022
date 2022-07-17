using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnModel : MonoBehaviour
{
    public readonly CharacterDie Die = new CharacterDie() { faceCount = 3 };
    public InsurgentPawn BoardState { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Die.dieFaces = new List<DieFace>() {
            new MoveFace(),
            new AttackFace(),
            new BarricadeFace(),
        };
        BoardState = gameObject.GetComponent<InsurgentPawn>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DieFace Roll()
    {
        int faceIndex = Random.Range(0, Die.faceCount);
        return Die.dieFaces[faceIndex];
    }
}
