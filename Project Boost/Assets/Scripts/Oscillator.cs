using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
	[SerializeField] Vector3 movementVector;
	[Range(0,1)][SerializeField] float movementFactor;

	Vector3 startPos;

	void Start()
	{
		startPos = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 offset = movementVector * movementFactor;
		transform.position = startPos + offset;
	}
}
