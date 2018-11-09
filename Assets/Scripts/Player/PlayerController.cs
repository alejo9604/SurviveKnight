using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
	/* Components */
	private CharacterController ThisController = null;
	private Transform ThisTransform = null;
	private Animator ThisAnimator = null;

	[Header("Move")]
	public float RotateSpeed = 90f;
	public float MaxSpeed = 50f;
	public bool run = false;
	public bool walk = false;
	private float MinSpeed = 0.5f;

	[Header("Jump")]
	public float JumpForce = 50f;
	public float GroundedDist = 0.1f;
	public bool IsGrounded = false;
	public LayerMask GroundLayer;
	private Vector3 Velocity = Vector3.zero;

	/* Control Var */
	private float distCameraZ;

	void Awake()
	{
		ThisController = GetComponent<CharacterController>();
		ThisTransform = GetComponent<Transform>();
		ThisAnimator = GetComponent<Animator>();
		run = false;

		distCameraZ = (Camera.main.transform.position - transform.position).magnitude;
	}

	void Update()
	{
		Vector3 MousePosWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
																Input.mousePosition.y, distCameraZ));

		MousePosWorld = new Vector3(MousePosWorld.x, ThisTransform.position.y, MousePosWorld.z);
		Vector3 LookDirection = MousePosWorld - ThisTransform.position;
		ThisTransform.localRotation = Quaternion.LookRotation(LookDirection.normalized, Vector3.up);
	}



	void FixedUpdate()
	{
		
		float Horz = CrossPlatformInputManager.GetAxis("Horizontal");
		float Vert = CrossPlatformInputManager.GetAxis("Vertical");

		//ThisTransform.rotation *= Quaternion.Euler(new Vector3(0, RotateSpeed * Time.deltaTime * Horz, 0));

		//Calculate Move Dir
		Velocity.z = Vert * MaxSpeed;
		Velocity.x = Horz * MaxSpeed;

		//Are we grounded?
		IsGrounded = (DistanceToGround() < GroundedDist) ? true : false;


		if (Input.GetKeyUp(KeyCode.Space) && IsGrounded)
		{
			Velocity.y = JumpForce;
			ThisAnimator.SetTrigger("Jump");
			run = false;
			walk = false;
		}

		//Apply gravity
		Velocity.y += Physics.gravity.y * Time.deltaTime;

		ThisController.Move(ThisTransform.TransformDirection(Velocity) * Time.deltaTime);
		setAnim();
	}

	public float DistanceToGround()
	{
		RaycastHit hit;
		float distanceToGround = 0;
		if (Physics.Raycast(ThisTransform.position, -Vector3.up, out hit, Mathf.Infinity, GroundLayer))
			distanceToGround = hit.distance;
		return distanceToGround;
	}


	void setAnim()
	{
		if (!(DistanceToGround() < GroundedDist))
		{
			run = false;
			walk = false;
			return;
		}

		if (Velocity.z > MinSpeed && !run)
		{
			//ThisAnimator.SetTrigger("Run");
			run = true;
		}
		else if (Velocity.z < -MinSpeed && !walk)
		{
			//ThisAnimator.SetTrigger("Walk");
			walk = true;
		}
		else if (Velocity.z < MinSpeed && Velocity.z > -MinSpeed)
		{
			if (run)
			{
				//ThisAnimator.SetTrigger("Run");
				run = false;
			}
			else if (walk)
			{
				//ThisAnimator.SetTrigger("Walk");
				walk = false;
			}
		}

		if (ThisAnimator.GetBool("RunBool") != run)
			ThisAnimator.SetBool("RunBool", run);


		if (ThisAnimator.GetBool("WalkBool") != walk)
			ThisAnimator.SetBool("WalkBool", walk);
	}

}
