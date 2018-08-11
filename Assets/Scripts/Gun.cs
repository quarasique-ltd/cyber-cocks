using UnityEngine;

public class Gun : MonoBehaviour
{
	public Bullet Bullet;

	public void Shoot(Vector2 direction)
	{
		Bullet.transform.position = transform.position;
		var bulletScript = Bullet.GetComponent<Bullet>();
		bulletScript.FlyingForce = direction.normalized;
		bulletScript.PushingForce = direction.normalized;
		Instantiate(Bullet);
	}
}
