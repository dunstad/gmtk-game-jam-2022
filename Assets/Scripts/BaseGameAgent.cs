using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseGameAgent : MonoBehaviour, IGameAgent
{

    private Animator anim;

    public event Action<IGameAgent, Vector3Int, Vector3Int> onMove;

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
        // gameObject.transform.position = newPos;
        StartCoroutine(MoveOverTime(newPos));
        onMove(this, Position, newPos);
    }

    private float InSine(float t) => (float)-Math.Cos(t * Math.PI / 2);
    private float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
    private float InOutSine(float t) => (float)(Math.Cos(t * Math.PI) - 1) / -2;

    // used internally to animate the movement
    // our game model shouldn't rely on the transform position
    private IEnumerator MoveOverTime(Vector3Int targetPosition)
    {
        Debug.Log("moveovertime called");
        // in case we need to prevent player from interacting while the piece moves?
        // moving = true;
        float sqrRemainingDistance = (gameObject.transform.position - targetPosition).sqrMagnitude;
        float timeSinceTick = 0f;
        float tickSeconds = 25f;

        while (sqrRemainingDistance > 0.01) {
            timeSinceTick += Time.deltaTime;
            var progress = InOutSine(timeSinceTick / tickSeconds);
            Vector3 newPosition = Vector3.Lerp(gameObject.transform.position, targetPosition, progress);
            gameObject.transform.position = newPosition;
            sqrRemainingDistance = (gameObject.transform.position - targetPosition).sqrMagnitude;
            yield return null;
        }
        gameObject.transform.position = targetPosition;
        // moving = false;
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
