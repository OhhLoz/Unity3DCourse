using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
	[SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f); //Done to avoid bugs in future, can easily see if object is moving
	[Range(0,1)][SerializeField] float movementFactor;
	[SerializeField] float timeperiod = 2f;

	Vector3 startPos;

	void Start()
	{
		startPos = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		float cycles = Time.time / timeperiod;
		const float tau = Mathf.PI * 2;
		float rawSine = Mathf.Sin(cycles * tau); // Varies between -1 and 1

		movementFactor = (rawSine / 2f) + 0.5f; // Turns the range from -1-1 to 0-1
		Vector3 offset = movementVector * movementFactor;
		transform.position = startPos + offset;
	}
}
