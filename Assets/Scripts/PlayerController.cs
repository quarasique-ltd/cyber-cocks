using UnityEngine;
using UnityEngine.XR.WSA.WebCam;

public class PlayerController : MonoBehaviour, IPlayerController
{
	private const string Horizontal = "Horizontal";
	private const string Vertical = "Vertical";
	
	public Player Player;
	public float Velocity = 1f;

	public void Start()
	{
		Player = GetComponent<Player>();
	}

	private void HandleMouse()
	{
		if (!Input.GetButtonDown("Fire1"))
		{
			return;
		}
		var mousePos = Input.mousePosition;
		mousePos.z = 0;
		var pointToRay = Camera.main.ScreenPointToRay(mousePos);	
		Player.Shoot(pointToRay.origin);
	}

	private void HandleArrows()
	{
		var direction = new Vector3(Input.GetAxis(Horizontal), Input.GetAxis(Vertical)) * Velocity;
		Player.Move(direction.normalized);
	}

	public void FixedUpdate()
	{
		HandleMouse();
		HandleArrows();
	}

	public void setPlayer(Player player)
	{
		this.Player = player;
	}
}
