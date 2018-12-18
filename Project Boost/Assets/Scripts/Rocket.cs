using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

	Rigidbody rocketRigidBody;
	[SerializeField] float rocketVelocity = 2f;
	// Use this for initialization
	void Start()
	{
		rocketRigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		ProcessInput();
	}

	private void ProcessInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			rocketRigidBody.AddRelativeForce(Vector3.up);
		}

		if(Input.GetKey(KeyCode.A))
		{
			print("Rotating Left");
		}
		else if (Input.GetKey(KeyCode.D))
		{
			print("Rotating Right");
		}
	}
}
