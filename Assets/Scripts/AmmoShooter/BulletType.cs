using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Developed by alejo9604
*/


/* Ammo/Weapon parameters*/

public class BulletType : MonoBehaviour{

	/* Shoot */
	public float Damage;
	public float LifeTime;
	public float MaxSpeed;

	/* Ammunition */
	public int Ammunition;
	public bool InfAmmunition;

	/* Visual effects */
	public Material mat;
	public int AmmunitionReload;

	public BulletType()
	{
		Damage = 10f;
		LifeTime = 1.5f;
		MaxSpeed = 3f;
		Ammunition = 99;
		InfAmmunition = true;
		AmmunitionReload = 5;
	}

	public BulletType(float newDamage, float newLifeTime, float newMaxSpeed, int newAmmunition, bool isInfAmmunition)
	{
		Damage = newDamage;
		LifeTime = newLifeTime;
		MaxSpeed = newMaxSpeed;
		Ammunition = newAmmunition;
		InfAmmunition = isInfAmmunition;
	}


	/* Spawn Ammo/Weapon for Player*/
	public void Shoot()
	{
		foreach (Transform T in transform)
			AmmoManager.SpawnAmmo(T.position, T.rotation, this);
	}

	/* Spawn Ammo/Weapon for Enemy*/
	public void Shoot(bool Enemy)
	{
		foreach (Transform T in transform)
			AmmoManager.SpawnAmmoEnemy(T.position, T.rotation, this);
	}
}
