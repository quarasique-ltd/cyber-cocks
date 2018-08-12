using UnityEngine;
public class CameraController : MonoBehaviour
{
	private void FixedUpdate()
	{
		var players = GameObject.FindGameObjectsWithTag("Player");
		if (players.Length != 1)
		{
			return;
		}
		Vector3 playerPosition = players[0].transform.position;
		transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
	}
}
