using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
	public float Damage = 100f;
	public float LifeTime = 2f;
	public float MaxSpeed = 1f;
	public bool fromEnemy = false;
	private Transform ThisTransform = null;

	void Awake()
	{
		ThisTransform = GetComponent<Transform>();
	}

	void OnEnable()
	{
		if(ThisTransform == null)
			ThisTransform = GetComponent<Transform>();
		CancelInvoke();
		Invoke("Die", LifeTime);
	}

	void Update()
	{
		ThisTransform.position += ThisTransform.forward * MaxSpeed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider Col)
	{
		if (Col.tag == "Enemy" && !fromEnemy)
		{
			Die();
			Col.transform.GetComponent<EnemyLive>().TakeDamage(Damage);
		}
		else if (Col.tag == "Player" && fromEnemy)
		{
			Die();
			Col.transform.GetComponent<PlayerLive>().TakeDamage(Damage);
		}
	}

	void Die()
	{
		CancelInvoke();
		gameObject.SetActive(false);
	}


}