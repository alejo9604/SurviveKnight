using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Camera CameraComponent;
	public Transform target;

	[Header("Rotation")]
	public float orbitSpeed = 100f;
	public bool ActiveRotation = true;

	[Header("Zoom")]
	public float zoomSpeed = 2.0f;
	public float minZoom = 3.0f;
	public float maxZoom = 7.0f;
	public bool ActiveZoom = true;

	//Follow
	[Header("Follow")]
	public bool ActiveFollow = true;
	private Vector3 offset;

	void Start()
	{
		CameraComponent = gameObject.GetComponent<Camera>();
		transform.LookAt(target.transform);
		offset = transform.position - target.position;
	}


	void Update()
	{
		if (ActiveRotation && Input.GetMouseButton(1))
		{
			transform.LookAt(target);
			transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * orbitSpeed);
		}

		if (ActiveZoom)
		{
			float scroll = Input.GetAxis("Mouse ScrollWheel") * -1;
			CameraComponent.orthographicSize += scroll * zoomSpeed;
			if (CameraComponent.orthographicSize < minZoom)
				CameraComponent.orthographicSize = minZoom;
			else if (CameraComponent.orthographicSize > maxZoom)
				CameraComponent.orthographicSize = maxZoom;
		}

		if(ActiveFollow)
			transform.position = target.position + offset;
	}
}
