using UnityEngine;
using System.Collections;

/*
 * Developed by alejo9604
*/


/* Ammo/Weapon Move*/

public class AmmoMove : MonoBehaviour
{
	public float MaxSpeed = 1f;
	private Transform ThisTransform = null;

	void Awake()
	{
		ThisTransform = GetComponent<Transform>();
	}

	void Update()
	{
		ThisTransform.position += ThisTransform.forward * MaxSpeed * Time.deltaTime;
	}
}
