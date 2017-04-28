using UnityEngine;

/*
 * Developed by alejo9604
*/


/* Fire State*/

public class FireState : EnemyStateBase {


	public FireState(EnemyIAController controlled) : base(controlled)
	{
	}

	public override void UpdateState()
	{
		Fire();
	}

	private void Fire()
	{
		controlled.transform.LookAt(controlled.chaseTarget);
		if (IsCloseEnough())
			controlled.Attack();
		else
			ToAlert();
	}

	private bool IsCloseEnough()
	{
		return (controlled.chaseTarget.position - controlled.transform.position).magnitude < controlled.detectionRange;
	}


	/* Fire to Alert state */
	private void ToAlert()
	{
		controlled.chaseTarget = null;
		controlled.MakeTransition(EnemySatate.Alert);
	}


}
