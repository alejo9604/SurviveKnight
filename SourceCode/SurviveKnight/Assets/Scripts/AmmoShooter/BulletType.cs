using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType : MonoBehaviour{

	public float Damage;
	public float LifeTime;
	public float MaxSpeed;
	public int Ammunition;
	public bool InfAmmunition;
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

	public void Shoot()
	{
		//Debug.Log("Shoot");
		foreach (Transform T in transform)
			AmmoManager.SpawnAmmo(T.position, T.rotation, this);
	}

	public void Shoot(bool Enemy)
	{
		//Debug.Log("Shoot");
		foreach (Transform T in transform)
			AmmoManager.SpawnAmmoEnemy(T.position, T.rotation, this);
	}
	/*
	void SetAmmunition()
	{
		Ammos = new List<BulletType>();
		Ammos.Add(new BulletType());
		Ammos.Add(new BulletType(15f, 2f, 3f, 10, false));
		Ammos.Add(new BulletType(10f, 1.5f, 3f, 5, false));
		Ammos.Add(new BulletType(15f, 1f, 4f, 1, false));

		actualAmmo = 0;
	}
	*/
}
