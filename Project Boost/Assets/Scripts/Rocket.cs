using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		Rotate();
		Thrust();
	}

	private void Rotate()
	{
		if(Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rocketRotationVelocity * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.back * rocketRotationVelocity * Time.deltaTime);
		}
	}

	private void Thrust()
	{
		rocketRigidBody.freezeRotation = true;

		if(Input.GetKey(KeyCode.W))
		{
			rocketRigidBody.AddRelativeForce(Vector3.up * rocketThrustVelocity * Time.deltaTime);
			if(!rocketThrust.isPlaying)
				rocketThrust.Play();
		}
		else
			rocketThrust.Stop();

		rocketRigidBody.freezeRotation = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		switch(collision.gameObject.tag)
		{
			case "Friendly":
				break;
			case "Finish":
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				break;
			default:
				print("Dead");
				SceneManager.LoadScene(0);
				break;
		}
	}
}
