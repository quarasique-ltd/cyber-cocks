using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<GameObject> Players;
	public Field _field;

	public Texture2D cursor;

	public void Start () {
		foreach (var t in Players)
		{
			Instantiate(t);
		}
		_field = GetComponent<Field>();
		
	}
}
