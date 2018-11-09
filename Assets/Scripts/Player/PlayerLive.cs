using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Developed by alejo9604
*/


/* PLayer live controller*/
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

		int skin = 0;
		if (PlayerPrefs.HasKey("Skin"))
			skin = PlayerPrefs.GetInt("Skin");

		SkinnedMeshRenderer SMR = GetComponentInChildren<SkinnedMeshRenderer>();
		SMR.material.SetTexture("_MainTex", SceneMannager.SM.skins[skin]);

	}
	

	void Update () {
	}

	/* Take damege */
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
				Invoke("callEnd", 2.5f);
			}
			else {
				Invoke("EndTakeDamage", DamageDelay);
			}
			isAttack = true;
			CanvasManager.CM.setPlayerLife(Health / initHealth);
		}
	}

	void callEnd()
	{
		SceneMannager.SM.LoadEnd(CanvasManager.CM.score);
	}

	void EndTakeDamage()
	{
		isAttack = false;
	}

	/* Restore Health */
	void AddHealth(float plusHealth)
	{
		Health += plusHealth;
		if (Health > initHealth)
			Health = initHealth;
		CanvasManager.CM.setPlayerLife(Health/initHealth);
	}

	/* Attack event */
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
