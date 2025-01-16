using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NpcMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public Transform target;
    public bool ShouldMove;

    [Header("Components")]
    private Animator Npcanimator;
    private NavMeshAgent NavMeshAgentAi;

    private void Start()
    {
        Npcanimator = GetComponentInChildren<Animator>();
        NavMeshAgentAi = GetComponent<NavMeshAgent>();

    }

   private void Update()
    {
        MoveAiToTarget();   
        PlayNpcAnimations();
    }

   

    public void MoveAiToTarget()
    {

        if (ShouldMove && target != null) // If The Npc Stopped Talking
        {
            NavMeshAgentAi.SetDestination(target.position); // Setting Target

            if (!NavMeshAgentAi.pathPending &&
                NavMeshAgentAi.remainingDistance <= NavMeshAgentAi.stoppingDistance &&
                (!NavMeshAgentAi.hasPath || NavMeshAgentAi.velocity.sqrMagnitude == 0f))
            {
                ShouldMove = false;
            }
        }
    }


    private void PlayNpcAnimations()
    {
        if (ShouldMove)
        {
            Npcanimator.Play("Female_Walking");

        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target.position, 1f);
        Gizmos.DrawLine(transform.position, target.position);
    }
}
