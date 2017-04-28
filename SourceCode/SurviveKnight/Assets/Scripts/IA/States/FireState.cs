using UnityEngine;

public class FireState : EnemyStateBase {


	public FireState(EnemyIAController controlled) : base(controlled)
	{
	}

	public override void UpdateState()
	{
		Fire();
		//Look();
	}


	private void Look()
	{
		
		Transform player = LookForPlayer();
		if (player != null)
			controlled.transform.LookAt(controlled.chaseTarget);
		else
			ToAlert();
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

	private void ToAlert()
	{
		controlled.chaseTarget = null;
		controlled.MakeTransition(EnemySatate.Alert);
	}


}
