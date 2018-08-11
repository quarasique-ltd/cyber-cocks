using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	private Animator animator;
	private Rigidbody2D mybody;
	private Collider2D _collider2D;
	private IPlayerController _playerController;
	// Use this for initialization
	void Start ()
	{
		_playerController = new PlayerController();
		animator = GetComponent<Animator> ();
		mybody = GetComponent<Rigidbody2D>();
		_collider2D = GetComponent<Collider2D>();
	}
	
	void FixedUpdate ()
	{
		_playerController.Move(mybody);
		Collider2D[] collider2Ds = new Collider2D[10];
		ContactFilter2D contactFilter2D = new ContactFilter2D();
		contactFilter2D.NoFilter();
		int platformIntersections = Physics2D.OverlapCollider(_collider2D, contactFilter2D, collider2Ds);
		Debug.Log(platformIntersections);
		if (platformIntersections == 0)
		{
			Debug.Log("you dead");
			mybody.bodyType = RigidbodyType2D.Dynamic;
			SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sortingLayerName = "DeadPlayer";
		}
	}

}
