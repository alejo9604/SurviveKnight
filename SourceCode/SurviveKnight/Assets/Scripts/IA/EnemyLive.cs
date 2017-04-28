using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLive : MonoBehaviour {

	public float Health = 20;
	public int Points = 10;
	private Animator ThisAnimator;

	void Start () {
		ThisAnimator = GetComponent<Animator>();
	}

	void Update () {
		
	}


	public void TakeDamage(float damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			GetComponent<EnemyIAController>().Disable();
			Health = 0;
			ThisAnimator.SetTrigger("Die");
			CanvasManager.CM.setScore(Points);
			Invoke("DestroyThis", 1.2f);
		}
	}

	public void DestroyThis()
	{
		Destroy(gameObject);
	}
}
