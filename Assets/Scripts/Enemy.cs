using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity {
    public enum State { Idle,Chasing,Attacking};
    State currentState;

    NavMeshAgent pathFinder;
    Transform target;
    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttack = 1;

    float nextAttackTime;

    float myCollisionRadius;
    float targetCollisionRadius;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        StartCoroutine(UpdatePath());
	}
	
	// Update is called once per frame
	void Update () {
        if( Time.time > nextAttackTime)
        {
            float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
            if (sqrDistanceToTarget < Mathf.Pow(attackDistanceThreshold, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttack;
                StartCoroutine(Attack());
            }
        }
        
	}

    IEnumerator Attack(){
        currentState = State.Attacking;
        pathFinder.enabled = false;
        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = target.position;
        float percent = 0;
        float attackSpeed = 3;
        while (percent<=1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = 4 * (-percent * percent + percent);
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            yield return null;
        }
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }
    IEnumerator UpdatePath()
    {
        float refreshRate = 1f;
        while(target != null)
        {
            if(currentState == State.Chasing)
            {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                if (!dead)
                {
                    pathFinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
