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

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_circleCollider2D = GetComponent<CircleCollider2D>();
		_startPosition = _rigidbody2D.transform.position;
		_rigidbody2D.velocity = FlyingVelocity;
		var yRotation = Vector3.Angle(FlyingVelocity, Vector3.right);
		gameObject.transform.eulerAngles = new Vector3(0, 0, yRotation);
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
