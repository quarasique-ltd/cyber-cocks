using UnityEngine;

public class Bullet : MonoBehaviour
{
	private const string PlayerTag = "Player";
	private Vector3 _startPosition;

	private Rigidbody2D _rigidbody2D;
	private CircleCollider2D _circleCollider2D;
	
	public float Range = 9;
	public float DetonationRadius = 1;
	public Vector2 PushingForce = Vector2.zero;
	public Vector2 FlyingForce = Vector2.zero;

	private void Start()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_circleCollider2D = GetComponent<CircleCollider2D>();
		_startPosition = _rigidbody2D.transform.position;
	}
	
	private void FixedUpdate()
	{
		_rigidbody2D.velocity = FlyingForce;
		if (Range <= Vector3.Distance(_startPosition, _rigidbody2D.transform.position))
		{
			Detonate();
		}
	}

	private void Detonate()
	{
		_circleCollider2D.radius = DetonationRadius;
		_rigidbody2D.velocity = Vector2.zero;
		Destroy(gameObject, 1);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag(PlayerTag)) 
		{
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(PushingForce);
		}
	}
}
