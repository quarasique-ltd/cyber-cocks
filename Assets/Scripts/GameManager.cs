using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public List<GameObject> players;

	void Start () {
		for (int i = 0; i < players.Count; i++)
		{
			Instantiate(players[i]);
		}
	}
}
