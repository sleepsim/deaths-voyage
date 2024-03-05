using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Outline outline;
    public GameObject rangeIndicator;
    public GameObject targetEnemy;
    private bool isInRange;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();    
        outline.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rangeIndicator.activeSelf)
        {
            rangeIndicator.transform.position = transform.root.position;
        }

        if(CheckDistance(targetEnemy))
        {
            GetComponent<RangedCombat>().autoAttackToggle = true;
        }
        else
        {
            GetComponent<RangedCombat>().autoAttackToggle = false;
        }
    }

    // Outline around enemy
    public void toggleOutline()
    {
        outline.enabled = !outline.enabled;
        toggleRangeIndicator();
    }

    // Show Enemy Range indicator
    private void toggleRangeIndicator()
    {
        if(outline.enabled)
        {
            rangeIndicator.SetActive(true);
            rangeIndicator.transform.position = transform.root.position;
            // rangeIndicator.transform.Rotate(0, -90, 0);
            rangeIndicator.transform.localScale = new Vector3(200, 10, 400);
        }
        else
        {
            rangeIndicator.SetActive(false);
        }
    }

    // Check Distance between player and this object
    public bool CheckDistance(GameObject other)
    {
        return Vector3.Distance(other.transform.position, transform.position) <= 10 ;
    }

    // If in player attack range, change outline color
    public void SetOutlineGreen(bool b)
    {   
        if(b)
        {
            outline.OutlineColor = Color.green;
            isInRange = true;
        }
        else
        {
            outline.OutlineColor = Color.red;
            isInRange = false;
        }
    }

    public bool InRange()
    {
        return isInRange;
    }
}
