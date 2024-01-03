using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";

    CustomActions input;

    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        input = new CustomActions();
        AssignInput();
    }

    void AssignInput()
    {
        input.Main.Move.performed += ctx => ClickToMove();
    }

    void ClickToMove()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers)) 
        {
            this.agent.ResetPath();
            this.agent.isStopped = false;
            this.agent.updatePosition = true;
            this.agent.updateRotation = true;

            agent.SetDestination(hit.point);

            if (clickEffect != null) 
            {
                Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        if (agent.transform.position == agent.destination)
        {
            this.agent.isStopped = true;
            this.agent.updatePosition = false;
            this.agent.updateRotation = false;
            this.agent.velocity = Vector3.zero;
        }
    }
}
