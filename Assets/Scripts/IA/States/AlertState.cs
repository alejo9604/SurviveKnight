using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Developed by alejo9604
*/


/* Alert State: Tourn around*/

public class AlertState : EnemyStateBase
{
	private float searchTimer;

	public AlertState(EnemyIAController controlled) : base(controlled)
	{
	}

	public override void UpdateState()
	{
		Search();
		LookAround();
	}

	private void Search()
	{
		Transform player = LookForPlayer();
		if (player != null)
		{
			if (controlled.Fire)
			{
				ToFire(player);
			}
			else {
				ToChase(player);
			}
		}
	}

	/* Alert to Chase state */
	private void ToChase(Transform player)
	{
		controlled.chaseTarget = player;
		controlled.MakeTransition(EnemySatate.Chase);
	}

	/* Alert to Fire state */
	private void ToFire(Transform player)
	{
		controlled.chaseTarget = player;
		controlled.MakeTransition(EnemySatate.Fire);
	}

	private void LookAround()
	{
		controlled.navMeshAgent.Stop();
		controlled.transform.Rotate(0, controlled.searchingTurnSpeed * Time.deltaTime, 0);
		searchTimer += Time.deltaTime;

		if (searchTimer >= controlled.searchingDuration)
			ToPatrolState();
	}

	/* Alert to Patrol state */
	private void ToPatrolState()
	{
		searchTimer = 0f;
		controlled.MakeTransition(EnemySatate.Patrol);
	}
}
