using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOutline : MonoBehaviour
{

    private float delay = 0.01f;
    private Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();    
        outline.enabled = false;
    }

    // void OnMouseOver()
    // {   
    //     if(Input.GetMouseButtonDown(0))
    //     {
    //         outline.enabled = !outline.enabled;
    //     }
    // }

    // public void OnMouseExit()
    // {
    //     // outline.enabled = false;
    // }

    public void toggleOutline()
    {
        outline.enabled = !outline.enabled;
    }
}