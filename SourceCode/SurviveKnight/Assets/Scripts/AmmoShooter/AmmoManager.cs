using UnityEngine;
using System.Collections.Generic;

/*
 * Developed by alejo9604
*/


/* Pool controller for Ammo/Weapon objects*/

public class AmmoManager : MonoBehaviour
{
	/* Ammo reference*/
	public GameObject AmmoPrefab = null;

	/* Control Var.*/
	public int PoolSize = 100;

	/* Pool */
	private Queue<Transform> AmmoQueue = new Queue<Transform>();
	private GameObject[] AmmoArray;

	public static AmmoManager AmmoManagerSingleton = null;

	void Awake()
	{
		if (AmmoManagerSingleton != null)
		{
			Destroy(GetComponent<AmmoManager>());
			return;
		}

		AmmoManagerSingleton = this;
		AmmoArray = new GameObject[PoolSize];

		for (int i = 0; i < PoolSize; i++)
		{
			AmmoArray[i] = Instantiate(AmmoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Transform ObjTransform = AmmoArray[i].GetComponent<Transform>();
			ObjTransform.parent = GetComponent<Transform>();
			AmmoQueue.Enqueue(ObjTransform);
			AmmoArray[i].SetActive(false);
		}
	}


	/* Spwan Ammo for Simple shoot */
	public static Transform SpawnAmmo(Vector3 Position, Quaternion Rotation)
	{
		Transform SpawnedAmmo = AmmoManagerSingleton.AmmoQueue.Dequeue();

		SpawnedAmmo.gameObject.SetActive(true);
		SpawnedAmmo.position = Position;
		SpawnedAmmo.rotation = Rotation;

		//Add to queue end
		AmmoManagerSingleton.AmmoQueue.Enqueue(SpawnedAmmo);

		//Return ammo
		return SpawnedAmmo;
	}


	/* Spwan Ammo for Player shoot */
	public static Transform SpawnAmmo(Vector3 Position, Quaternion Rotation, BulletType Bullet)
	{
		Transform SpawnedAmmo = AmmoManagerSingleton.AmmoQueue.Dequeue();
		SpawnedAmmo.GetComponent<MeshRenderer>().material = Bullet.mat;
		Ammo SpawnedAmmoComponent = SpawnedAmmo.GetComponent<Ammo>();
		SpawnedAmmoComponent.Damage = Bullet.Damage; 
		SpawnedAmmoComponent.LifeTime = Bullet.LifeTime;
		SpawnedAmmoComponent.MaxSpeed = Bullet.MaxSpeed;

		SpawnedAmmo.gameObject.SetActive(true);
		SpawnedAmmo.position = Position;
		SpawnedAmmo.rotation = Rotation;

		//Add to queue end
		AmmoManagerSingleton.AmmoQueue.Enqueue(SpawnedAmmo);

		//Return ammo
		return SpawnedAmmo;
	}

	/* Spwan Ammo for Enemy shoot */
	public static Transform SpawnAmmoEnemy(Vector3 Position, Quaternion Rotation, BulletType Bullet)
	{
		Transform bullet = SpawnAmmo(Position, Rotation, Bullet);

		bullet.GetComponent<Ammo>().fromEnemy = true;

		return bullet;
	}
}
