using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	private Collider2D _collider2D;
	private SpriteRenderer _spriteRenderer;
	private Animator _animator;
	
	private double _lastStunTime;
	
	private IPlayerController _playerController;

	public Gun Gun;
	public float StunTimeSeconds = 1;
	public float Velocity = 0.1f;
	
	private void Start()
	{
		_playerController = gameObject.GetComponent<IPlayerController>();
		_playerController.setPlayer(this);
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_collider2D = GetComponent<Collider2D>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_animator = GetComponent<Animator>();
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
			_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
			_spriteRenderer.sortingLayerName = "DeadPlayer";
		}

		if (_rigidbody2D.position.y < -30)
		{
			Destroy(gameObject, .5f);
		}
	}

	private bool IsStunned()
	{
		return GetTimeInSeconds() - _lastStunTime < StunTimeSeconds;
	}

	private void ApplyMovementAnimation()
	{
		_animator.SetBool("Run", true);
	}
	
	private void ApplyIdleAnimation()
	{
		_animator.SetBool("Run", false);
	}
	
	private void ApplyAttackAnimation()
	{
		_animator.SetTrigger("Attack");
	}

	public void Move(Vector2 direction)
	{
		if (IsStunned() || direction == Vector2.zero)
		{
			ApplyIdleAnimation();
			return;
		}

		_rigidbody2D.velocity = Vector3.zero;
		ApplyMovementAnimation();
		FlipCharacter(direction);
		_rigidbody2D.transform.position += (Vector3) direction * Velocity;
	}

	private void FlipCharacter(Vector2 direction)
	{
		if (Math.Abs(direction.x) < float.Epsilon)
		{
			return;
		}
		_spriteRenderer.flipX = Math.Sign(direction.x) < 0;
	}

	public void Shoot(Vector3 point)
	{
		var direction = (point - transform.position).normalized;
		ApplyAttackAnimation();
		FlipCharacter(direction);
		Gun.Shoot(direction);
	}

	private static double GetTimeInSeconds()
	{
		var timeSpan = DateTime.UtcNow - new DateTime(1970,1,1,0,0,0);
		return timeSpan.TotalSeconds;
	}

	public void Stun(Vector3 direction)
	{
		_lastStunTime = GetTimeInSeconds();
		Move((direction + _rigidbody2D.transform.position).normalized);
	}
}
