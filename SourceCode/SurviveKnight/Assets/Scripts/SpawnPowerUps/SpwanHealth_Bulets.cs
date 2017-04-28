using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Developed by alejo9604
*/


/* Spawn Health and Bullet ammunition*/
public class SpwanHealth_Bulets : MonoBehaviour {

	public GameObject HealthPrefab;
	public GameObject BulletPrefab;
	public float ratioSpawn = 5f;
	public float AverageSpawnTime = 10f;
	private float variance = 2f;

	private AmmoInventory playerInventory;


	void Start () {
		playerInventory = GameObject.FindWithTag("Player").GetComponent<AmmoInventory>();
		Invoke("SpwanPower", AverageSpawnTime + Random.value * variance);
	}
	
	void Update () {
		
	}

	void SpwanPower()
	{
		if (Random.value < 0.3)
			SpawnNewHealth();
		else
			SpawnAmmunitionBullet();
	}

	Vector3 randomPoint()
	{
		Vector3 point = new Vector3(
			transform.position.x + Random.Range(-1f, 1f) * ratioSpawn,
			transform.position.y,
			transform.position.z + Random.Range(-1f, 1f) * ratioSpawn
		);
		return point;
	}

	void SpawnNewHealth()
	{
		Instantiate(HealthPrefab, randomPoint(), Quaternion.identity);
		Invoke("SpwanPower", AverageSpawnTime + Random.value * variance);
	}

	void SpawnAmmunitionBullet()
	{
		int ammo = Random.Range(0, playerInventory.Ammos.Count - 1);

		if (!playerInventory.Ammos[ammo].InfAmmunition)
		{
			GameObject bullet = Instantiate(BulletPrefab, randomPoint(), Quaternion.identity);

			bullet.GetComponent<MeshRenderer>().material = playerInventory.Ammos[ammo].mat;
			bullet.GetComponent<BulletAmmunition>().Ammo = ammo;
			bullet.GetComponent<BulletAmmunition>().Ammunition = playerInventory.Ammos[ammo].AmmunitionReload;
		}
		Invoke("SpwanPower", AverageSpawnTime + Random.value * variance);
	}
}
