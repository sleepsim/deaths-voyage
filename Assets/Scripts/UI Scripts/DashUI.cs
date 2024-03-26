using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI count;
    public Slider refillSlider;
    public float timerInterval;
    public float currentTimer;
    public GameObject player;
    // Update is called once per frame
    void Start()
    {

    }
    void Update()
    {
        timerInterval = player.GetComponent<NewMovement>().dashIncreaseInterval;
        currentTimer = player.GetComponent<NewMovement>().timeUntilDashRefill;

        count.text = player.GetComponent<NewMovement>().currentDash.ToString() + "x";

        // Hide slider if ammo full
        if (timerInterval == currentTimer)
        {
            refillSlider.gameObject.SetActive(false);
        }
        else if (currentTimer < timerInterval)
        {
            refillSlider.gameObject.SetActive(true);
            refillSlider.maxValue = timerInterval;
            refillSlider.value = currentTimer;
        }
    }


}
