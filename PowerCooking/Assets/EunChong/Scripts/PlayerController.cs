using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";

    CustomActions input;

    NavMeshAgent agent;
    Animator animator;
    string lookPosName;
    Vector3 stopPointPos;
    Vector3 kitchenAppliancePos;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask moveLayer;
    [SerializeField] float lookRotationSpeed;

    [Header("PrepareCook")]
    [SerializeField] LayerMask prepareCookLayer;
    [SerializeField] float checkRange;

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
        input.Main.PrepareCook.performed += ctx => ClickToPrepareCook();
    }

    void ClickToMove()
    {
        RaycastHit hit;

        if (!GetComponent<Playerinteraction>().isInteraction)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, moveLayer))
            {
                this.agent.ResetPath();
                this.agent.isStopped = false;
                this.agent.updatePosition = true;
                this.agent.updateRotation = true;

                agent.SetDestination(hit.point);

                lookPosName = "Floor";
                stopPointPos = hit.point;

                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
        }
    }

    void ClickToPrepareCook()
    {
        RaycastHit hit;

        if (!GetComponent<Playerinteraction>().isInteraction)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, prepareCookLayer))
            {
                if (Vector3.Distance(hit.transform.GetComponent<KitchenAppliance>().stopPoint.position, transform.position) < checkRange)
                {
                    this.agent.ResetPath();
                    this.agent.isStopped = false;
                    this.agent.updatePosition = true;
                    this.agent.updateRotation = true;

                    agent.SetDestination(hit.transform.GetComponent<KitchenAppliance>().stopPoint.position);

                    lookPosName = "KitchenAppliance";
                    stopPointPos = hit.transform.GetComponent<KitchenAppliance>().stopPoint.position;
                    kitchenAppliancePos = hit.transform.position;
                }
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

        if (!GetComponent<Playerinteraction>().isInteraction)
        {
            if (lookPosName == "Floor")
            {
                if (agent.velocity != Vector3.zero)
                {
                    Vector3 direction = (stopPointPos - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
                }
            }
            else if (lookPosName == "KitchenAppliance")
            {
                transform.LookAt(new Vector3(kitchenAppliancePos.x, transform.position.y, kitchenAppliancePos.z));
            }
        }

        if (GetComponent<Playerinteraction>().isInteraction) 
        {
            if (!agent.isStopped) 
            {
                ClickToPrepareCook();
            }

            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }
}
