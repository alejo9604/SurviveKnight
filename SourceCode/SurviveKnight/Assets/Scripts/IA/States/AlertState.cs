﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void ToChase(Transform player)
	{
		controlled.chaseTarget = player;
		controlled.MakeTransition(EnemySatate.Chase);
	}

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

	private void ToPatrolState()
	{
		searchTimer = 0f;
		controlled.MakeTransition(EnemySatate.Patrol);
	}
}
