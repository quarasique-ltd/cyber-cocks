using System;
using UnityEngine;

[Serializable]
public class FieldTileSprite
{
    public string Name;
    public Sprite TileImage;
    public FieldTileType TileType;

    public FieldTileSprite()
    {
        Name = "Unset";
        TileImage = null;
        TileType = FieldTileType.unset;
    }

    public FieldTileSprite(string name, Sprite image, FieldTileType tileType)
    {
        Name = name;
        TileImage = image;
        TileType = tileType;
    }
}