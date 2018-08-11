using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IPlayerController
{

	public float Velocity = 1f;
	
	public void Move(Rigidbody2D mybody) {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal") * Velocity, Input.GetAxis("Vertical") * Velocity, 0);
		mybody.transform.position = mybody.transform.position + velocity;
	}
}
