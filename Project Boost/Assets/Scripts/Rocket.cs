﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
	Rigidbody rocketRigidBody;
	AudioSource rocketThrust;
	[SerializeField] float rocketThrustVelocity = 2f;
	[SerializeField] float rocketRotationVelocity = 2f;
	[SerializeField] float levelChangeDelay = 1f;
	[SerializeField] AudioClip thrustSound;
	[SerializeField] AudioClip deathSound;
	[SerializeField] AudioClip winSound;

	[SerializeField] ParticleSystem thrustParticles;
	[SerializeField] ParticleSystem deathParticles;
	[SerializeField] ParticleSystem winParticles;

	enum State { Alive, Dying, Transcending }
	State state = State.Alive;

	bool disableCollisions = false;

	// Use this for initialization
	void Start()
	{
		rocketRigidBody = GetComponent<Rigidbody>();
		rocketThrust = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		if(state == State.Alive)
		{
			HandleRotateInput();
			HandleThrustInput();
		}

		if(Debug.isDebugBuild)
			DebugKeys();
	}

	private void DebugKeys()
	{
		if(Input.GetKey(KeyCode.L))
			LoadNextScene();
		if(Input.GetKey(KeyCode.C))
			disableCollisions = !disableCollisions;
	}

	private void HandleRotateInput()
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

	private void HandleThrustInput()
	{
		rocketRigidBody.freezeRotation = true;

		if(Input.GetKey(KeyCode.W))
		{
			rocketRigidBody.AddRelativeForce(Vector3.up * rocketThrustVelocity * Time.deltaTime);
			if(!rocketThrust.isPlaying)
				rocketThrust.PlayOneShot(thrustSound);
			thrustParticles.Play();
		}
		else
		{
			rocketThrust.Stop();
			thrustParticles.Stop();
		}

		rocketRigidBody.freezeRotation = false;
	}

	void OnCollisionEnter(Collision collision)
	{
		// TO protect multiple death collisions
		if (state != State.Alive || disableCollisions)
			return;

		switch(collision.gameObject.tag)
		{
			case "Friendly":
				print("Friendly");
				//state = State.Alive;
				break;
			case "Finish":
                print("Finish");
                rocketThrust.Stop();
                rocketThrust.PlayOneShot(winSound);
				winParticles.Play();
                state = State.Transcending;
                Invoke("LoadNextScene", levelChangeDelay);
				break;
			default:
				print("Dead");
				rocketThrust.Stop();
				rocketThrust.PlayOneShot(deathSound);
				deathParticles.Play();
				state = State.Dying;
				Invoke("LoadFirstScene", levelChangeDelay);
				break;
		}
	}

	private void LoadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void LoadFirstScene()
	{
		SceneManager.LoadScene(0);
	}
}
