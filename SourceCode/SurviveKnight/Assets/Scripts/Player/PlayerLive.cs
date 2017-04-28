using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLive : MonoBehaviour {

	[Header("Live")]
	public float Health = 50;
	public float DamageDelay = 0.3f;

	/* Control Var. */
	private float initHealth;
	private bool isAttack = false;
	private bool die = false;

	void Start () {
		CanvasManager.CM.setPlayerLife(1);
		initHealth = Health;
	}
	

	void Update () {
	}


	public void TakeDamage(float Damage)
	{
		if (!isAttack & !die)
		{
			Health -= Damage;
			if (Health <= 0)
			{
				Health = 0;
				die = true;
				GetComponent<Animator>().SetTrigger("Die");
				GetComponent<PlayerController>().enabled = false;
			}
			isAttack = true;
			Invoke("EndTakeDamage", DamageDelay);
			CanvasManager.CM.setPlayerLife(Health / initHealth);
		}
	}

	void EndTakeDamage()
	{
		isAttack = false;
	}

	void AddHealth(float plusHealth)
	{
		Health += plusHealth;
		if (Health > initHealth)
			Health = initHealth;
		CanvasManager.CM.setPlayerLife(Health/initHealth);
	}


	void OnTriggerEnter(Collider Col)
	{
		if (Col.tag == "Health")
		{
			AddHealth(10);
			Destroy(Col.gameObject);

		}if (Col.tag == "BulletReload")
		{
			BulletAmmunition bullet = Col.GetComponent<BulletAmmunition>();
			GetComponent<AmmoInventory>().Reload(bullet.Ammo, bullet.Ammunition);
			Destroy(Col.gameObject);
		}
	}
}
