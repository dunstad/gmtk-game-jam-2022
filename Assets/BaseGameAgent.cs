using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameAgent : MonoBehaviour, IGameAgent
{

    private Animator anim;

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
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector3Int newPos)
    {
        // probably need a coroutine for this one
        // anim.Play("move");
        gameObject.transform.position = newPos;
    }

    public void Attack(Vector3Int newPos)
    {
        anim.Play("attack");
    }

    // start death animation
    public void Die()
    {
        anim.Play("die");
    }

    // finish removing the thing after it dies, called from animation event
    public void FinishDying()
    {
        Destroy(gameObject);
        // spawn particles?
    }

}
