using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill
{
    public static int[,] GetFloodFill(FieldTile[,] map, Vector2Int start)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        int[,] floodFillMap = new int[map.GetLength(0), map.GetLength(1)];
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int g = 0; g < map.GetLength(1); g++)
            {
                floodFillMap[i, g] = map.GetLength(0) * map.GetLength(1) + 10;
            }
        }
        queue.Enqueue(start);
        Debug.Log(start);
        floodFillMap[start.x, start.y] = 0;
        while (queue.Count > 0)
        {
            Vector2Int v = queue.Dequeue();
            if (floodFillMap[v.x, v.y] != 0)
            {
                continue;
            }
            if (v.x < map.Length - 1 && map[v.x + 1, v.y] != null)
            {
                floodFillMap[v.x + 1, v.y] = floodFillMap[v.x, v.y] + 1;
                queue.Enqueue(new Vector2Int(v.x + 1, v.y));
            }
            if (v.y < map.GetLength(1) - 1 && map[v.x, v.y + 1] != null)
            {
                floodFillMap[v.x, v.y + 1] = floodFillMap[v.x, v.y] + 1;
                queue.Enqueue(new Vector2Int(v.x, v.y + 1));
            }
            if (v.x - 1 > 0 && map[v.x - 1, v.y] != null)
            {
                floodFillMap[v.x - 1, v.y] = floodFillMap[v.x, v.y] + 1;
                queue.Enqueue(new Vector2Int(v.x - 1, v.y));
            }
            if (v.y - 1 > 0 && map[v.x + 1, v.y] != null)
            {
                floodFillMap[v.x, v.y - 1] = floodFillMap[v.x, v.y] + 1;
                queue.Enqueue(new Vector2Int(v.x, v.y - 1));
            }
        }
        return floodFillMap;
    }

    public static int PathLength(FieldTile[,] map, Vector2Int from, Vector2Int to)
    {
        int[,] FloodFill = GetFloodFill(map, to);
        return FloodFill[from.x, from.y];
    }

    public static Vector2Int[] GetPath(FieldTile[,] map, Vector2Int from, Vector2Int to, int pathLen)
    {
        int[,] FloodFill = GetFloodFill(map, to);
        int finalLen = Math.Min(pathLen, FloodFill[from.x, from.y]);
        Vector2Int[] path = new Vector2Int[finalLen];
        Vector2Int lastPoint = from;
        int lastValue = FloodFill[lastPoint.x, lastPoint.y];
        for (int i = 0; i < finalLen; i++)
        {
            Vector2Int bufPoint = new Vector2Int();
            bufPoint.x = lastPoint.x;
            bufPoint.y = lastPoint.y;
            if (lastPoint.y - 1 > 0)
            {
                bufPoint.y = bufPoint.y - 1;
            }
            if (FloodFill[bufPoint.x, bufPoint.y] < lastValue)
            {
                path[i] = bufPoint;
                lastValue = FloodFill[bufPoint.x, bufPoint.y]; 
                continue;
            }
            
            bufPoint = new Vector2Int();
            bufPoint.x = lastPoint.x;
            bufPoint.y = lastPoint.y;
            if (lastPoint.x - 1 > 0)
            {
                bufPoint.x = bufPoint.x - 1;
            }
            if (FloodFill[bufPoint.x, bufPoint.y] < lastValue)
            {
                path[i] = bufPoint;
                lastValue = FloodFill[bufPoint.x, bufPoint.y]; 
                continue;
            }
            
            bufPoint = new Vector2Int();
            bufPoint.x = lastPoint.x;
            bufPoint.y = lastPoint.y;
            if (lastPoint.y < map.GetLength(1) - 1)
            {
                bufPoint.y = bufPoint.y + 1;
            }
            if (FloodFill[bufPoint.x, bufPoint.y] < lastValue)
            {
                path[i] = bufPoint;
                lastValue = FloodFill[bufPoint.x, bufPoint.y]; 
                continue;
            }
            
            bufPoint = new Vector2Int();
            bufPoint.x = lastPoint.x;
            bufPoint.y = lastPoint.y;
            if (lastPoint.x < map.Length - 1)
            {
                bufPoint.x = bufPoint.x + 1;
            }
            if (FloodFill[bufPoint.x, bufPoint.y] < lastValue)
            {
                lastValue = FloodFill[bufPoint.x, bufPoint.y]; 
                path[i] = bufPoint;
                continue;
            }
            
    }
        return path;
    }
}