using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineFade : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("mouse entered");
        anim.Play("outlineFadeIn");
    }
    
    private void OnMouseExit()
    {
        Debug.Log("mouse exited");
        anim.Play("outlineFadeOut");
    }


}
