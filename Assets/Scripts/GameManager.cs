using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public List<GameObject> Players;
	public Field _field;
	public GameObject Grid;

	public Texture2D cursor;

	public void Start () {
		foreach (var t in Players)
		{
			Instantiate(t);
		}
		Instantiate(Grid);
		_field = GetComponent<Field>();
	}
}
