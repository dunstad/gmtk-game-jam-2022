using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBox : MonoBehaviour
{

    [SerializeField] GameObject diePrefab;
    private Animator anim;
    
    [SerializeField] Sprite dieFaceMove;
    [SerializeField] Sprite dieFaceAttack;
    [SerializeField] Sprite dieFaceConvert;
    [SerializeField] Sprite dieFaceBarricade;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        // Invoke("FakeRollDice", 1);
        // RollDice(new List<DieFace>(){new MoveFace(), new AttackFace(), new BarricadeFace()});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubscribeToRollEvent(GameState gs)
    {
        gs.onDiceRolled += RollDice;
    }

    public void RollDice(List<DieFace> dieFaces)
    {

        foreach (DieFace dieFace in dieFaces)
        {
            Vector3 dieRandomSpawn = Random.insideUnitCircle * 2;
            Vector3 dieSpawnPoint = new Vector3(transform.position.x + dieRandomSpawn.x, transform.position.y + dieRandomSpawn.y, transform.position.z);
            GameObject die = Instantiate(diePrefab, dieSpawnPoint, Quaternion.identity);
            Vector3 euler = transform.eulerAngles;
            euler.z = Random.Range(0.0f, 360.0f);
            die.transform.eulerAngles = euler;

            Sprite dieSprite;
            Color dieColor;
            if (dieFace.GetType() == typeof(MoveFace))
            {
                // final face the die lands on after rolling animation
                dieSprite = dieFaceMove;
                dieColor =  new Color(1f, 0.9746141f, 0.1921569f); // yellow
            } else if (dieFace.GetType() == typeof(AttackFace))
            {
                dieSprite = dieFaceAttack;
                dieColor =  new Color(1f, 0.5867883f, 0.1921569f); // orange
            } else if (dieFace.GetType() == typeof(BarricadeFace))
            {
                dieSprite = dieFaceBarricade;
                dieColor =  new Color(0.1921569f, 1f, 0.3118789f); // green
            } else
            {
                dieSprite = dieFaceConvert;
                dieColor =  new Color(0.8851702f, 0.1921569f, 1f); // purple
            }

            die.GetComponent<OnStop>().realFace = dieSprite;
            die.GetComponent<OnStop>().realColor =  dieColor;

            die.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = dieSprite;
            die.transform.GetChild(0).GetComponent<SpriteRenderer>().color =  dieColor;

        }
        
        anim.Play("shakeUp");

    }

    void FakeRollDice()
    {

        for (int i = 0; i < 5; i++)
        {
            Vector3 dieRandomSpawn = Random.insideUnitCircle * 2;
            Vector3 dieSpawnPoint = new Vector3(transform.position.x + dieRandomSpawn.x, transform.position.y + dieRandomSpawn.y, transform.position.z);
            Instantiate(diePrefab, dieSpawnPoint, Quaternion.identity);
        }
        anim.Play("shakeUp");

    }

}
