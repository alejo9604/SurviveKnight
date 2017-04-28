using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AmmoInventory))]
public class AmmoShooter : MonoBehaviour
{
	/* Components */
	private AmmoInventory ThisInventory = null;

	/* Control Var */
	private bool CanFire = true;
	public float ReloadDelay = 0.1f;

	void Awake()
	{
		ThisInventory = GetComponent<AmmoInventory>();
	}

	void Update()
	{
		//Check fire control
		if (Input.GetButtonDown("Fire1") && CanFire && ThisInventory.CanShoot)
		{
			ThisInventory.UseAmmo();
			CanFire = false;
			Invoke("EnableFire", ReloadDelay);
		}
	}


	void EnableFire()
	{
		CanFire = true;
	}
}
