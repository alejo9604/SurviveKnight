using UnityEngine;

/*
 * Developed by alejo9604
*/


/* Chase State */

public class ChaseState : EnemyStateBase
{
	public ChaseState(EnemyIAController controlled) : base(controlled)
	{
	}

	public override void UpdateState()
	{
		Chase();
		Look();
	}


	private void Look()
	{
		Transform player = LookForPlayer();
		if (player != null)
			controlled.chaseTarget = player;
		else
			ToAlert();
	}

	private void Chase()
	{
		controlled.navMeshAgent.SetDestination(controlled.chaseTarget.position);
		if (IsCloseEnough())
			controlled.Attack();
	}

	private bool IsCloseEnough()
	{
		return (controlled.chaseTarget.position - controlled.transform.position).magnitude < 1.5;
	}

	/* Chase to Alert state */
	private void ToAlert()
	{
		controlled.chaseTarget = null;
		controlled.MakeTransition(EnemySatate.Alert);
	}
}
