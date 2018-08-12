using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour {
	private FieldTile[,] _field;
	public GameObject Tilemap;
	private Tilemap tilemap;

	public int FieldTileHealth;
	public List<int> FieldTileHealthPoints;
	public List<String> FieldTileSpritsPaths;
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
}
