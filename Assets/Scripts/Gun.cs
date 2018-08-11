using UnityEngine;

public class Gun : MonoBehaviour
{
	public Bullet Bullet;
	public float Power = 5;

	public void Shoot(Vector3 velocity)
	{
		Bullet.transform.position = transform.position + velocity;
		var bulletScript = Bullet.GetComponent<Bullet>();
		bulletScript.FlyingVelocity = velocity * Power;
		bulletScript.PushingForce = velocity * Power;
		Instantiate(Bullet);
	}
}
