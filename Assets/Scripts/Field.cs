using System;
using System.Collections.Generic;
using System.Linq;
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
		tilemap.ClearAllTiles(); 
		RenderMap();
	}

	private void FixedUpdate()
	{
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
		Dictionary<Vector2Int, Sprite> list = generateRedrawList();
		foreach (var item in list)
		{
			Tile tile = ScriptableObject.CreateInstance<Tile>();
			tile.sprite = item.Value;
			tilemap.SetTile(new Vector3Int(item.Key.x, item.Key.y, 0), tile);
		}
	}

	private Dictionary<Vector2Int, Sprite> generateRedrawList()
	{
		Dictionary<Vector2Int, Sprite> list = new Dictionary<Vector2Int, Sprite>();
		for (int x = 0; x <_field.GetLength(0); x++) 
		{
			for (int y = 0; y < _field.GetLength(1); y++)
			{
				if (_field[x, y] != null && _field[x, y].isForRedraw())
				{
					_field[x, y].wasRedrawed();
					Sprite newSprite = null;
					
					if (_field[x, y].getState() != FieldTileSprits.Count)
					{
						newSprite = FieldTileSprits[_field[x, y].getState()];	
					}
					else
					{
						_field[x, y] = null;
					}
					
					list.Add(new Vector2Int(x, y), newSprite);
				}
			}
		}
		return list;
	}

	public FieldTile[,] getArray()
	{
		return _field;
	}

	public List<Vector2Int> getSpawns(int count)
	{
		Random rnd = new Random();
		HashSet<Vector2Int> spawns = new HashSet<Vector2Int>();
		while (spawns.Count != count)
		{
			int x = rnd.Next(0, _field.GetLength(0));
			int y = rnd.Next(0, _field.GetLength(1));
			if (_field[x, y] != null)
			{
				spawns.Add(new Vector2Int(x, y));
			}
		}
		return spawns.ToList();
	}
}
