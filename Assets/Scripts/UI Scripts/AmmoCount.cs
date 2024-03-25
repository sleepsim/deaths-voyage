using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI count;
    public Slider refillSlider;
    public float timerInterval;
    public float currentTimer;
    private float lerpSpeed = 0.02f;
    public GameObject player;
    // Update is called once per frame
    void Start()
    {

    }
    void Update()
    {
        timerInterval = player.GetComponent<NewCombat>().ammoIncreaseInterval;
        currentTimer = player.GetComponent<NewCombat>().timeUntilAmmoRefill;

        count.text = "x" + player.GetComponent<NewCombat>().currentAmmo.ToString();

        // Hide slider if ammo full
        if (timerInterval == currentTimer)
        {
            Debug.Log("yup they equal");
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
