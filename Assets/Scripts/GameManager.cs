using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<GameObject> players;
	private IField _field;
	public GameObject GameObjectRenderer;
	void Start () {
		for (int i = 0; i < players.Count; i++)
		{
			Instantiate(players[i]);
		}
		_field.GenerateMap();
		_field.RenderMap();
	}
}
