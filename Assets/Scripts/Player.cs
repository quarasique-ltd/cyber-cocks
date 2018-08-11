using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	private Animator animator;
	private Rigidbody2D mybody;

	private IPlayerController _playerController;
	// Use this for initialization
	void Start ()
	{
		_playerController = new PlayerController();
		animator = GetComponent<Animator> ();
		mybody = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate ()
	{
		_playerController.Move(mybody);
	}
}
