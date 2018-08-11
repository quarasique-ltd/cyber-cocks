using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	private Animator _animator;
	private Rigidbody2D _mybody;
	private Collider2D _collider2D;
	
	private double _lastStunTime;
	
	private IPlayerController _playerController;

	public Gun Gun;
	public float StunTimeSeconds = 1;
	public float Velocity = 5;
	
	private void Start()
	{
		_playerController = gameObject.GetComponent<IPlayerController>();
		_playerController.setPlayer(this);
		_animator = GetComponent<Animator>();
		_mybody = GetComponent<Rigidbody2D>();
		_collider2D = GetComponent<Collider2D>();
		Gun = Instantiate(Gun, transform);
	}

	private void FixedUpdate()
	{
		var collider2Ds = new Collider2D[10];
		var contactFilter2D = new ContactFilter2D();
		contactFilter2D.NoFilter();
		var platformIntersections = Physics2D.OverlapCollider(_collider2D, contactFilter2D, collider2Ds);
		if (platformIntersections == 0)
		{
			_mybody.bodyType = RigidbodyType2D.Dynamic;
			var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sortingLayerName = "DeadPlayer";
		}

		if (_mybody.position.y < -30)
		{
			Destroy(gameObject, .5f);
		}
	}

	private bool IsStunned()
	{
		return GetTimeInSeconds() - _lastStunTime < StunTimeSeconds;
	}

	public void Move(Vector2 direction)
	{
		if (IsStunned())
		{
			return;
		}
		_mybody.transform.position = _mybody.transform.position + (Vector3) direction;
	}

	public void Shoot(Vector3 direction)
	{
		Gun.Shoot((direction - transform.position).normalized);
	}

	private static double GetTimeInSeconds()
	{
		var timeSpan = DateTime.UtcNow - new DateTime(1970,1,1,0,0,0);
		return timeSpan.TotalSeconds;
	}

	public void Stun(Vector3 direction)
	{
		_lastStunTime = GetTimeInSeconds();
		Move((direction + _mybody.transform.position).normalized);
	}
}
