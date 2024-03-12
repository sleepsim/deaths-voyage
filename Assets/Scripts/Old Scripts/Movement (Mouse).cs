// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.AI;

// public class Movement : MonoBehaviour
// {

//     public NavMeshAgent agent;
//     public float rotateSpeedMovement = 0.05f;
//     private float rotateVelocity;

//     public Animator anim;
//     float motionSmoothTime = 0.1f;

//     // Start is called before the first frame update
//     void Start()
//     {
//         agent = gameObject.GetComponent<NavMeshAgent>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Animation();
//         Move();
//     }

//     public void Animation()
//     {
//         float speed = agent.velocity.magnitude / agent.speed;
//         anim.SetFloat("Speed", speed, motionSmoothTime, Time.deltaTime);
//     }

//     public void Move()
//     {
//         if(Input.GetMouseButtonDown(1))
//         {
//             RaycastHit hit;

//             if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
//             {
//                 if(hit.collider.tag == "Earth")
//                 {
//                     agent.SetDestination(hit.point);
//                     agent.stoppingDistance = 0;
//                 }
//             }
//         }
//     }
// }
