using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestController : MonoBehaviour
{
    public int currQuest = 0;
    public TextMeshProUGUI infoText;
    public Transform waypointTarget;

    [Header("Quest Fogs")]
    public GameObject introFog;
    public GameObject fog1;
    public GameObject fog2;
    public GameObject fog3;
    public GameObject exitFog;

    [Header("Enemies")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    [Header("Popup Quest Details UI")]

    [SerializeField] GameObject questDetails;
    [SerializeField] GameObject questDetailsScript;


    // Start is called before the first frame update
    void Start()
    {
        activateQuest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activateQuest()
    {
        if (SceneManager.GetActiveScene().name == "2_Intro")
        {
            switch (currQuest)
            {
                case 0:
                    waypointTarget = introFog.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Investigate the fog ahead";
                    break;
                case 1:
                    waypointTarget = exitFog.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Escape the turoial";
                    break;

                default:
                    brokenQuest();
                    break;
            }
        }

        if (SceneManager.GetActiveScene().name == "3_StageOne")
        {

            switch (currQuest)
            {
                case 0:
                    waypointTarget = introFog.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Investigate the fog ahead";
                    break;
                case 1:
                    fog1.SetActive(true);
                    waypointTarget = fog1.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Find the voices";
                    break;
                case 2:
                    enemy1.SetActive(true);
                    waypointTarget = enemy1.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Hunt down Mercedes";
                    break;
                case 3:
                    fog2.SetActive(true);
                    waypointTarget = fog2.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Find the voices again";
                    break;
                case 4:
                    enemy2.SetActive(true);
                    waypointTarget = enemy2.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Hunt down Ferrari";
                    break;
                case 5:
                    fog3.SetActive(true);
                    waypointTarget = fog3.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Find the voices (yet) again";
                    break;
                case 6:
                    enemy3.SetActive(true);
                    waypointTarget = enemy3.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Hunt down Redbull";
                    break;
                case 7:
                    exitFog.SetActive(true);
                    waypointTarget = exitFog.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Find the exit";
                    break;
                default:
                    brokenQuest();
                    break;
            }
        }

        if (SceneManager.GetActiveScene().name == "4_StageTwo")
        {
            switch (currQuest)
            {
                case 0:
                    waypointTarget = exitFog.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Find The Exit!";
                    break;
                case 1:
                    waypointTarget = exitFog.transform;
                    Camera.main.GetComponent<MissionWaypoint>().target = waypointTarget;
                    infoText.text = "-Escape the turoial";
                    break;

                default:
                    brokenQuest();
                    break;
            }
        }
    }

    public void openQuest()
    {
        questDetails.SetActive(true);
        questDetailsScript.SetActive(true);
    }

    public void completeCurrentQuest()
    {
        currQuest += 1;
    }

    public void brokenQuest()
    {

    }
}
