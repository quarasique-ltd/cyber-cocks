using System.Collections.Generic;
using System.Linq.Expressions;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour {
	FieldTileType[,] field;
	private FieldTile[,] _field;
	public GameObject Tilemap;
	private Tilemap tilemap;
	private Dictionary<FieldTileType, Sprite> spriteMap = new Dictionary<FieldTileType, Sprite>();

	public int FieldTileHealth;
	public List<int> FieldTileHealthPoints;
	public List<Sprite> FieldTileSprits;

	void Start ()
	{
		tilemap = Tilemap.GetComponent<Tilemap>();
		spriteMap.Add(FieldTileType.filled, Resources.Load<Sprite>("Sprites/background"));
		GenerateMap();
		RenderMap();
	}

	public void setTilemap(Tilemap tilemap)
	{
		this.tilemap = tilemap;
	}

	public void GenerateMap()
	{
		field = new FieldTileType[30,20];
		_field = new FieldTile[30,20];
		for (int x = 0; x <field.GetLength(0); x++) 
		{
			for (int y = 0; y < field.GetLength(1); y++)
			{
				field[x, y] = FieldTileType.filled;
				_field[x,y] = new FieldTile(FieldTileHealth, ref FieldTileHealthPoints);
			}
		}
	}

	public void RenderMap()
	{
		tilemap.ClearAllTiles(); 
		for (int x = 0; x <field.GetLength(0); x++) 
		{
			for (int y = 0; y < field.GetLength(1); y++)
			{
				if (field[x, y] != FieldTileType.unset)
				{
					Tile tile = ScriptableObject.CreateInstance<Tile>();
					tile.sprite = spriteMap[field[x, y]];
					tilemap.SetTile(new Vector3Int(x, y, 0), tile); 
				}
				else
				{
					tilemap.SetTile(new Vector3Int(x, y, 0), null); 
				}

			}
		}
	}

	public FieldTileType[,] getArray()
	{
		return field;
	}
}
