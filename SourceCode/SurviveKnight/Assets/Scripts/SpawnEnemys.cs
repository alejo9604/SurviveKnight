using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour {

	[Header("Prefabs")]
	public GameObject GhostPrefabs;
	public GameObject FirePrefabs;

	[Header("Ghosts")]
	public int initGhost = 3;
	public int GhostMin = 0;

	[Header("Fire")]
	public int initFire = 1;
	public int FireMin = 0;

	[Header("Spawn")]
	public float ratioSpawn = 5f;
	public float AverageSpawnTime = 10f;
	private float variance = 2f;


	/* Control Var. */
	public static int GhostCount = 0;
	public static int FireCount = 0;

	void Start () {
		GhostCount = 0;
		FireCount = 0;

		for (int i = 0; i < initGhost; i++)
			Spawn(GhostPrefabs);

		for (int i = 0; i < initFire; i++)
			Spawn(FirePrefabs);

		Invoke("SetSpawn", AverageSpawnTime + Random.value * variance);
	}


	void SetSpawn()
	{
		if (GhostCount < GhostMin)
		{
			Spawn(GhostPrefabs);
			GhostCount++;
		}
		else if (FireCount < FireMin)
		{
			Spawn(FirePrefabs);
			FireCount++;
		}
		else if (Random.value < 0.5)
		{
			Spawn(GhostPrefabs);
			GhostCount++;
		}
		else {
			Spawn(FirePrefabs);
			FireCount++;
		}

		Invoke("SetSpawn", AverageSpawnTime + Random.value * variance);
	}

	void Spawn(GameObject obj)
	{
		GameObject bullet = Instantiate(obj, randomPoint(), Quaternion.identity);
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

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * ratioSpawn);
	}

}
