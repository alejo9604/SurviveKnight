using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoInventory : MonoBehaviour {

	[Header("Ammos")]
	public List<BulletType> Ammos = null;
	public int actualAmmo;
	public bool Enemy;

	void Start()
	{
		actualAmmo = 0;
		if(!Enemy)
			CanvasManager.CM.setBullets(Ammos[actualAmmo].mat.color, Ammos[actualAmmo].Ammunition);
	}

	void Update () {
		if (Input.GetMouseButtonDown(1) && !Enemy)
		{
			NextAmmo();
		}
	}

	public void NextAmmo()
	{
		actualAmmo++;
		if (actualAmmo >= Ammos.Count)
			actualAmmo = 0;

		CanvasManager.CM.setBullets(Ammos[actualAmmo].mat.color, Ammos[actualAmmo].Ammunition);
	}


	public bool CanShoot
	{
		get{ return Ammos[actualAmmo].Ammunition > 0;}
	}

	public void UseAmmo()
	{
		if (CanShoot)
		{
			if (!Ammos[actualAmmo].InfAmmunition)
			{
				Ammos[actualAmmo].Ammunition--;
			}

			if (!Enemy)
			{
				Ammos[actualAmmo].Shoot();
				CanvasManager.CM.setBullets(Ammos[actualAmmo].mat.color, Ammos[actualAmmo].Ammunition);
			}else
				Ammos[actualAmmo].Shoot(true);
		}
	}

	public void Reload(int ammo, int ammunition)
	{
		Ammos[ammo].Ammunition += ammunition;
		if(ammo == actualAmmo)
			CanvasManager.CM.setBullets(Ammos[actualAmmo].mat.color, Ammos[actualAmmo].Ammunition);
	}

}
