using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	Rigidbody rocketRigidBody;
	AudioSource rocketThrust;
	[SerializeField] float rocketThrustVelocity = 2f;
	[SerializeField] float rocketRotationVelocity = 2f;
	// Use this for initialization
	void Start()
	{
		rocketRigidBody = GetComponent<Rigidbody>();
		rocketThrust = GetComponent<AudioSource>();
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
			rocketRigidBody.AddRelativeForce(Vector3.up * rocketThrustVelocity * Time.deltaTime);
			if(!rocketThrust.isPlaying)
				rocketThrust.Play();
		}
		else
			rocketThrust.Stop();

		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rocketRotationVelocity * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.back * rocketRotationVelocity * Time.deltaTime);
		}
	}
}
