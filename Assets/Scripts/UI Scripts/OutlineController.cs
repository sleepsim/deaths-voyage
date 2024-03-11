using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private Outline outline;
    public GameObject rangeIndicator;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();    
        outline.enabled = false;
    }

    
    public void update()
    {
        if(rangeIndicator.activeSelf)
        {
            rangeIndicator.transform.position = transform.root.position;
        }
    }

    public void toggleOutline()
    {
        outline.enabled = !outline.enabled;
        toggleRangeIndicator();
    }

    private void toggleRangeIndicator()
    {
        if(outline.enabled)
        {
            rangeIndicator.SetActive(true);
            rangeIndicator.transform.position = transform.root.position;
        }
        else
        {
            rangeIndicator.SetActive(false);
        }
    }
}