using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<GameObject> Players;
	public Field _field;
	public GameObject Grid;

	public void Start () {
		foreach (var t in Players)
		{
			Instantiate(t);
		}
		Instantiate(Grid);
		_field = GetComponent<Field>();
	}
}
