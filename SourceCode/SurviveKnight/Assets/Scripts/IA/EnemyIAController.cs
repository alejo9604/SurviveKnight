using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgentController))]
public class EnemyIAController : MonoBehaviour {

	[Header("Look")]
	public float sightRange = 20f;
	public float detectionRange = 2f;
	public float Damage = 10;

	[Header("Patrol")]
	public float ratioArea = 40f;
	[HideInInspector]
	public Vector3 orginalPosition;

	[Header("Search")]
	public Transform eyes;
	public float searchingTurnSpeed = 120f;
	public float searchingDuration = 4f;

	public bool Fire = false;
	public float ReloadDelay = 0.1f;
	private bool CanFire = true;

	/* Components */
	[HideInInspector]
	public NavMeshAgentController navMeshAgent;
	[HideInInspector]
	public Transform chaseTarget;
	[HideInInspector]
	public Transform player;

	private EnemyStateBase currentState;
	private Dictionary<EnemySatate, EnemyStateBase> states;
	AmmoInventory ammo;

	private void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgentController>();
		states = new Dictionary<EnemySatate, EnemyStateBase>();
		states.Add(EnemySatate.Patrol, new PatrolState(this));
		states.Add(EnemySatate.Alert, new AlertState(this));
		states.Add(EnemySatate.Chase, new ChaseState(this));
		states.Add(EnemySatate.Fire, new FireState(this));
		currentState = states[EnemySatate.Patrol];
		orginalPosition = transform.position;
		player = GameObject.FindGameObjectWithTag("Player").transform;

		if(Fire)
			ammo = GetComponent<AmmoInventory>();
	}

	private void Update()
	{
		currentState.UpdateState();
	}

	public void MakeTransition(EnemySatate state)
	{
		//Debug.Log(state);
		currentState = states[state];
		currentState.StartState();
	}

	private void OnTriggerEnter(Collider other)
	{
		currentState.OnTriggerEnter(other);
	}

	public void Disable()
	{
		navMeshAgent.Stop();
		this.enabled = false;
	}

	public void Attack()
	{
		if (chaseTarget != null)
		{
			if (Fire && CanFire){
				ammo.UseAmmo();
				CanFire = false;
				Invoke("EnableFire", ReloadDelay);
			}else if(!Fire)
				chaseTarget.GetComponent<PlayerLive>().TakeDamage(Damage);
		}
	}

	void EnableFire()
	{
		CanFire = true;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position, transform.position + transform.forward*detectionRange);
	}
}
