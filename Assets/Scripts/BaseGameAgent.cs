using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseGameAgent : MonoBehaviour, IGameAgent
{

    protected Animator anim;
    protected Animator weaponAnim;
    public bool alive = true;

    public event Action<IGameAgent, Vector3Int, Vector3Int> onMove;

    [SerializeField] GameObject projectile;
    [SerializeField] AudioSource attackSound;
    [SerializeField] AudioSource moveSound;

    public Vector3Int Position 
    { 
        get 
        {
            Vector3 pos = gameObject.transform.position;
            return new Vector3Int((int)pos.x, (int)pos.y, 0);
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        weaponAnim = GetComponentsInChildren<Animator>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector3Int newPos)
    {
        // gameObject.transform.position = newPos;
        moveSound.Play();
        StartCoroutine(MoveOverTime(newPos));
        onMove(this, Position, newPos);
    }

    private float InSine(float t) => (float)-Math.Cos(t * Math.PI / 2);
    private float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
    private float InOutSine(float t) => (float)(Math.Cos(t * Math.PI) - 1) / -2;

    // used internally to animate the movement
    private IEnumerator MoveOverTime(Vector3Int targetPosition)
    {
        Debug.Log("moveovertime called");
        // in case we need to prevent player from interacting while the piece moves?
        // moving = true;
        float sqrRemainingDistance = (gameObject.transform.position - targetPosition).sqrMagnitude;
        float timeSinceTick = 0f;
        float tickSeconds = 5f;

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

    private IEnumerator LaunchProjectile(Vector3Int targetPosition)
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Debug.Log("LaunchProjectile called");
        float sqrRemainingDistance = (newProjectile.transform.position - targetPosition).sqrMagnitude;
        float timeSinceTick = 0f;
        float tickSeconds = 5f;

        while (sqrRemainingDistance > 0.01) {
            timeSinceTick += Time.deltaTime;
            var progress = timeSinceTick / tickSeconds;
            Vector3 newPosition = Vector3.Lerp(newProjectile.transform.position, targetPosition, progress);
            newProjectile.transform.position = newPosition;
            sqrRemainingDistance = (newProjectile.transform.position - targetPosition).sqrMagnitude;
            yield return null;
        }
        Destroy(newProjectile);
    }
    public void Attack(Vector3Int newPos)
    {
        StartCoroutine(LaunchProjectile(newPos));
        weaponAnim.Play("attack");
        attackSound.Play();
    }

    // start death animation
    public virtual void Die()
    {
        Invoke("FinishDying", 0.3f);
        alive = false;
    }

    // finish removing the thing after it dies, called from animation event
    public void FinishDying()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(.5f, 0f ,0f);
        // Destroy(gameObject);
        // spawn particles?
    }

}
