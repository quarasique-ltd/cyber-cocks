using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public interface IField
    {
        void setTilemap(Tilemap tilemap);
        void GenerateMap();
        void RenderMap();
        FieldTileType[,] getArray();
    }
}