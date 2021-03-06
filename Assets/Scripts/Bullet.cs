﻿using System;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private const string PlayerTag = "Player";
	private Vector3 _startPosition;

	private Rigidbody2D _rigidbody2D;
	private CircleCollider2D _circleCollider2D;
	private Animator _animator; 
	
	public float Range = 9;
	public float DetonationRadius = 1;
	public Vector3 PushingForce;
	public Vector3 FlyingVelocity;
	private GameManager gameManager;
	public int Damage;

	private void Start()
	{
		GameObject gameInitializer = GameObject.Find("GameInitializer");
		gameManager = gameInitializer.GetComponent<GameManager>();
		_animator = GetComponent<Animator>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_circleCollider2D = GetComponent<CircleCollider2D>();
		_startPosition = _rigidbody2D.transform.position;
		_rigidbody2D.velocity = FlyingVelocity;
//		var yRotation = Vector3.SignedAngle(FlyingVelocity, new Vector3(1, 0, 0), new Vector3(1, 0, 0));
		float zRotation = Vector3.Angle(FlyingVelocity, new Vector3(1, 0, 0));
		Vector3 cross = Vector3.Cross(FlyingVelocity, new Vector3(1, 0, 0));
		if (cross.z > 0)
		{
			zRotation = -zRotation;
		}
		gameObject.transform.eulerAngles = new Vector3(0, 0, zRotation);
	}
	
	private void FixedUpdate()
	{
		if (Range <= Vector3.Distance(_startPosition, _rigidbody2D.transform.position))
		{
			Detonate();
		}
	}

	private void Detonate()
	{
		if (_animator == null)
		{
			return;
		}
		FieldTile[,] map = gameManager._field.getArray();
		if (transform.position.x > 0 && transform.position.x < map.GetLength(0) && transform.position.y > 0 &&
		    transform.position.y < map.GetLength(1))
		{
			if(map[(int)Math.Round(transform.position.x), (int)Math.Round(transform.position.y)] != null) {
				map[(int) Math.Round(transform.position.x), (int) Math.Round(transform.position.y)].takeDamage(Damage);
			}
		}
		_animator.SetTrigger("Destroy");	
		_circleCollider2D.radius = DetonationRadius;
		_rigidbody2D.velocity = Vector3.zero;
		Destroy(gameObject, .5f);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag(PlayerTag))
		{
			return;
		}
		var player = other.gameObject.GetComponent<Player>();
		player.Stun(PushingForce);
		Detonate();
	}
}
