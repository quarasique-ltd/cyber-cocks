using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class Field : MonoBehaviour {
	private FieldTile[,] _field;
	public GameObject Tilemap;
	private Tilemap tilemap;

	public int FieldTileHealth;
	public List<int> FieldTileHealthPoints;
	public List<Sprite> FieldTileSprits;

	void Start ()
	{
		tilemap = Tilemap.GetComponent<Tilemap>();
		GenerateMap();
		RenderMap();
	}

	public void setTilemap(Tilemap tilemap)
	{
		this.tilemap = tilemap;
	}

	public void GenerateMap()
	{
		_field = new FieldTile[30,20];
		for (int x = 0; x <_field.GetLength(0); x++) 
		{
			for (int y = 0; y < _field.GetLength(1); y++)
			{
				_field[x,y] = new FieldTile(FieldTileHealth, ref FieldTileHealthPoints);
			}
		}
	}

	public void RenderMap()
	{
		tilemap.ClearAllTiles(); 
		for (int x = 0; x <_field.GetLength(0); x++) 
		{
			for (int y = 0; y < _field.GetLength(1); y++)
			{
				if (_field[x, y] != null)
				{
					if (_field[x, y].getState() != FieldTileSprits.Count)
					{
						Tile tile = ScriptableObject.CreateInstance<Tile>();
						tile.sprite = FieldTileSprits[_field[x, y].getState()];	
						tilemap.SetTile(new Vector3Int(x, y, 0), tile);
					}
					else
					{
						tilemap.SetTile(new Vector3Int(x, y, 0), null);
					}
					 
				}
				else
				{
					tilemap.SetTile(new Vector3Int(x, y, 0), null); 
				}

			}
		}
	}
/*
	private Dictionary<Tuple<int, int>, FieldTile> renderField()
	{
		return null;
	}
*/
	public FieldTile[,] getArray()
	{
		return _field;
	}

	public List<Tuple<int,int>> getSpawns(int count)
	{
		Random rnd = new Random();
		HashSet<Tuple<int, int>> spawns = new HashSet<Tuple<int, int>>();
		while (spawns.Count != count)
		{
			int x = rnd.Next(0, _field.GetLength(0));
			int y = rnd.Next(0, _field.GetLength(1));
			if (_field[x, y] != null)
			{
				spawns.Add(new Tuple<int, int>(x, y));
			}
		}
		return spawns.ToList();
	}
}
