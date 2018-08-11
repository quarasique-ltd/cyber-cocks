using UnityEngine;
using UnityEngine.Assertions.Must;

public class Bullet : MonoBehaviour
{
	private const string PlayerTag = "Player";
	private Vector3 _startPosition;

	private Rigidbody2D _rigidbody2D;
	private CircleCollider2D _circleCollider2D;
	
	public float Range = 9;
	public float DetonationRadius = 1;
	public Vector3 PushingForce;
	public Vector3 FlyingVelocity;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_circleCollider2D = GetComponent<CircleCollider2D>();
		_startPosition = _rigidbody2D.transform.position;
		_rigidbody2D.velocity = FlyingVelocity;
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
		_circleCollider2D.radius = DetonationRadius;
		_rigidbody2D.velocity = Vector3.zero;
		Destroy(gameObject, 1);
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
