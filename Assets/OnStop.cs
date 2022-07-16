using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStop : MonoBehaviour
{

    // make sure we're not registering 0 velocity before we started moving
    bool hasLanded = false;
    bool stopped = false;
    int bounceCount = 0;
    [SerializeField] PhysicsMaterial2D bouncy;
    [SerializeField] PhysicsMaterial2D noBouncing;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = (Rigidbody2D) gameObject.GetComponent(typeof(Rigidbody2D));
        rb.sharedMaterial = bouncy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {

        if (bounceCount > 2)
        {

            Rigidbody2D rb = (Rigidbody2D) gameObject.GetComponent(typeof(Rigidbody2D));
            rb.sharedMaterial = noBouncing;
            if (rb.velocity == Vector2.zero)
            {
                stopped = true; 
                Debug.Log("stopped");
                rb.sharedMaterial = bouncy;
                bounceCount = 0;
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        hasLanded = true;
        bounceCount++;
    }

}
