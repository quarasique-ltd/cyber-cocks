using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	private Animator _animator;
	private Rigidbody2D _mybody;
	private Collider2D _collider2D;
	private PlayerController _playerController;

	public Gun Gun;

	private void Start()
	{
		_playerController = gameObject.AddComponent<PlayerController>();
		_playerController.Player = this;
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

	public void Move(float x = 0, float y = 0)
	{
		var direction = new Vector3(x, y);
		_mybody.transform.position = _mybody.transform.position + direction;
	}

	public void Shoot(Vector2 direction)
	{
		Gun.Shoot(direction);
	}
}
