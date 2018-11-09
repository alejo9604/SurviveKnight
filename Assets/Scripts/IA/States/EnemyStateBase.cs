using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Developed by alejo9604
*/


/* Abstract class and Enumeration for States*/

public enum EnemySatate{
	Patrol,
	Alert,
	Chase,
	Fire
}

public abstract class EnemyStateBase {

	protected EnemyIAController controlled;

	public EnemyStateBase(EnemyIAController controlled)
	{
		this.controlled = controlled;
	}

	public virtual void StartState()
	{
	}

	public virtual void OnTriggerEnter(Collider other)
	{
	}

	public abstract void UpdateState();


	/* Look if PLayer in Vision range */
	protected Transform LookForPlayer()
	{

		RaycastHit hit;
		Vector3 end = controlled.eyes.transform.position + controlled.eyes.transform.forward * controlled.sightRange;
		Debug.DrawLine(controlled.eyes.transform.position, end);
		if (Physics.SphereCast(controlled.eyes.transform.position, 1f, controlled.eyes.transform.forward, out hit, controlled.sightRange) && hit.collider.CompareTag("Player"))
			return hit.transform;
		else if( (controlled.eyes.transform.position - controlled.player.position).magnitude < controlled.detectionRange )
			return controlled.player;
		else
			return null;
	}
}
