using UnityEngine;

public class Background : MonoBehaviour
{
	private Vector2 _scale = Vector2.zero;
	private Camera _camera;
	
	public void FixedUpdate()
	{
		if (_scale == Vector2.zero)
		{
			_camera = Camera.main;
			var spriteRenderer = GetComponent<SpriteRenderer>();

			var cameraHeight = _camera.orthographicSize * 2;
			var cameraSize = new Vector2(_camera.aspect * cameraHeight, cameraHeight);
			Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
			Vector2 scale = transform.localScale;
			if (cameraSize.x >= cameraSize.y)
			{
				scale *= cameraSize.x / spriteSize.x;
			}
			else
			{
				scale *= cameraSize.y / spriteSize.y;
			}

			_scale = scale;
			transform.localScale = scale;
		}

		if (_camera != null)
		{
			transform.position = (Vector2) _camera.transform.position;
		}
	}
}
