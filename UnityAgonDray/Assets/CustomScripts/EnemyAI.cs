using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public int HP = 100;
    public enum AIState {idle, wandering, attacking, patrolling, dead}
    public AIState currentState = AIState.wandering;
    private bool died = false;

    private NavMeshAgent agent;
    private Collider collider;
    public GameObject RBTurret;
    public GameObject RBBody;
    public GameObject turret;
    public GameObject body;

    public GameObject targetEnemy;
    public Transform target;

    public float moveTimer = 0f;

    public EnemyCannonFireScript fire;

    public Collider sight;

    public bool playerInSight = false;

    private void Awake()
    {
        targetEnemy = GameObject.Find("PlayerTank").GetComponentInChildren<NewPlayerController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        collider = this.gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState) {

            case AIState.idle:
                IdleBehavior();
                break;

            case AIState.wandering:
                WanderingBehavior();
                break;

            case AIState.attacking:
                AttackingBehavior();
                break;

            case AIState.dead:
                DeadBehavior();
                break;
        }

        if (HP <= 0)
        {
            currentState = AIState.dead;
        }

        moveTimer -= Time.deltaTime;
    }

    private void IdleBehavior()
    {
        
    }

    private void WanderingBehavior()
    {
        if (moveTimer <= 0)
        {
            agent.SetDestination(this.transform.position + new Vector3(Random.Range(-40, 40), 0, Random.Range(-40, 40)));
            moveTimer = 5f;
        }
        if (playerInSight) currentState = AIState.attacking;
    }

    private void PatrollingBehavior()
    {
        if (playerInSight) currentState = AIState.attacking;
    }

    private void AttackingBehavior()
    {
        target.position = new Vector3(targetEnemy.transform.position.x, targetEnemy.transform.position.y - 1f, targetEnemy.transform.position.z);
        fire.Fire();
        if (moveTimer <= 0)
        {
            agent.SetDestination(targetEnemy.transform.position + new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15)));
            moveTimer = 5f;
        }
    }

    private void DeadBehavior()
    {
        if (!died)
        {
            died = true;

            collider.enabled = false;
            body.SetActive(false);
            RBBody.SetActive(true);
            turret.SetActive(false);
            RBTurret.SetActive(true);
            RBBody.GetComponent<Rigidbody>().AddForce((agent.velocity + Vector3.up * 5),  ForceMode.VelocityChange);
            agent.enabled = false;
            RBTurret.GetComponent<Rigidbody>().AddForce((Vector3.up * 2 + this.transform.forward) * 5, ForceMode.VelocityChange);
            RBTurret.GetComponent<Rigidbody>().AddTorque(new Vector3 (Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)), ForceMode.VelocityChange);
            targetEnemy.GetComponent<NewPlayerController>().health += 10;
            if (targetEnemy.GetComponent<NewPlayerController>().health > 100)
            {
                targetEnemy.GetComponent<NewPlayerController>().health = 100;
            }


        }
    }


}
