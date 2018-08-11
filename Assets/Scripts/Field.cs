using System.Collections.Generic;
using System.Linq.Expressions;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Field : MonoBehaviour {
	FieldTileType[,] field;
	public GameObject Tilemap;
	private Tilemap tilemap;
	private Dictionary<FieldTileType, Sprite> spriteMap = new Dictionary<FieldTileType, Sprite>();

//	private Tilemap tilemap = null;
	void Start ()
	{
		Debug.Log(Tilemap);
		tilemap = Tilemap.GetComponent<Tilemap>();
		Debug.Log(tilemap);
		spriteMap.Add(FieldTileType.filled, Resources.Load("Assets/Sprites/Tiles/background.png") as Sprite);
		GenerateMap();
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
		field = new FieldTileType[30,20];
		for (int x = 0; x <field.GetLength(0); x++) 
		{
			for (int y = 0; y < field.GetLength(1); y++)
			{
				field[x, y] = FieldTileType.filled;
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
