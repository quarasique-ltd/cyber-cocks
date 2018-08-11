using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private const float Velocity = 1f;
	private const string Horizontal = "Horizontal";
	private const string Vertical = "Vertical";
	
	public Player Player;

	public void Start()
	{
		Player = GetComponent<Player>();
	}

	public void FixedUpdate()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var direction = Input.mousePosition;
			Player.Shoot(direction);
		}
		Player.Move(Input.GetAxis(Horizontal) * Velocity, Input.GetAxis(Vertical) * Velocity);
	}
}
