using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<GameObject> players;
	private Field _field;
	public GameObject Grid;
	void Start () {
		for (int i = 0; i < players.Count; i++)
		{
			Instantiate(players[i]);
		}
		Instantiate(Grid);
		_field = GetComponent<Field>();
	}
}
