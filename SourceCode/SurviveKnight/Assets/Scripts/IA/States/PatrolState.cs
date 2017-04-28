using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Developed by alejo9604
*/


/* Patrol State: Random move*/

public class PatrolState : EnemyStateBase {

	private int nextWayPoint;

	public PatrolState(EnemyIAController controlled) : base(controlled)
	{
	}

	public override void UpdateState()
	{
		Patrol();
		Search();
	}

	public override void StartState()
	{
		SetCurrentPath();
	}

	public override void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			controlled.MakeTransition(EnemySatate.Alert);
	}

	private void Patrol()
	{
		if (!controlled.navMeshAgent.IsMoving)
		{
			//nextWayPoint = (nextWayPoint + 1) % controlled.wayPoints.Length;
			SetCurrentPath();
		}
	}

	private void SetCurrentPath()
	{
		//controlled.navMeshAgent.SetDestination(controlled.wayPoints[nextWayPoint].position);
		controlled.navMeshAgent.SetDestination(newPoint());
	}

	private Vector3 newPoint()
	{
		Vector3 point = new Vector3(
			controlled.orginalPosition.x + Random.Range(-1f, 1f) * controlled.ratioArea,
			controlled.orginalPosition.y,
			controlled.orginalPosition.z + Random.Range(-1f, 1f) * controlled.ratioArea
		);
		return point;
	}

	private void Search()
	{
		Transform player = LookForPlayer();
		if (player != null)
			ToChase(player);
	}

	private void ToChase(Transform player)
	{
		controlled.chaseTarget = player;
		if(!controlled.Fire)
			controlled.MakeTransition(EnemySatate.Chase);
		else
			controlled.MakeTransition(EnemySatate.Fire);
	}
}
