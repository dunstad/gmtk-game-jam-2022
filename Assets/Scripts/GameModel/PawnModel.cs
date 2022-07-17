using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnModel : MonoBehaviour
{
    public CharacterDie Die {get; private set;}
    public InsurgentPawn BoardState { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Die = gameObject.AddComponent<CharacterDie>();
        Die.faceCount = 3;
        MoveFace mFace = Die.gameObject.AddComponent<MoveFace>();
        Die.dieFaces = new List<DieFace>();
        // {
        //     Die.gameObject.AddComponent<MoveFace>(),
        //     Die.gameObject.AddComponent<AttackFace>(),
        //     Die.gameObject.AddComponent<BarricadeFace>(),
        // };
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
