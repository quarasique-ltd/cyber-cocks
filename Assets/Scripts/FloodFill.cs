using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFill
{
    
    private static Dictionary<Vector2, int[,]> buffer = new Dictionary<Vector2, int[,]>();
    public static int[,] GetFloodFill(FieldTile[,] map, Vector2Int start)
    {
        if (buffer.ContainsKey(start))
        {
            return buffer[start];
        }
        Queue<KeyValuePair<Vector2Int, int>> queue = new Queue<KeyValuePair<Vector2Int, int>>();
        int[,] floodFillMap = new int[map.GetLength(0), map.GetLength(1)];
        int maxValue = map.GetLength(0) * map.GetLength(1) + 10;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int g = 0; g < map.GetLength(1); g++)
            {
                floodFillMap[i, g] = 100000;
            }
        }
        queue.Enqueue(new KeyValuePair<Vector2Int, int>(start, 0));
        while (queue.Count > 0)
        {
            KeyValuePair<Vector2Int, int> pair = queue.Dequeue();
            Vector2Int v = pair.Key;
            int value = pair.Value;
            if (floodFillMap[v.x, v.y] <= value)
            {
                continue;
            }
            floodFillMap[v.x, v.y] = value;
            if (v.x + 1 < map.GetLength(0) && map[v.x + 1, v.y] != null && floodFillMap[v.x+1, v.y] > floodFillMap[v.x, v.y] + 1)
            {
                queue.Enqueue(new KeyValuePair<Vector2Int, int>(new Vector2Int(v.x+1, v.y), value+1));
            }
            if (v.y < map.GetLength(1) - 1 && map[v.x, v.y + 1] != null && floodFillMap[v.x, v.y+1] > floodFillMap[v.x, v.y] + 1)
            {
                queue.Enqueue(new KeyValuePair<Vector2Int, int>(new Vector2Int(v.x, v.y + 1), value + 1));
            }
            if (v.x  > 0 && map[v.x - 1, v.y] != null&& floodFillMap[v.x-1, v.y] > floodFillMap[v.x, v.y] + 1)
            {
                queue.Enqueue(new KeyValuePair<Vector2Int, int>(new Vector2Int(v.x - 1, v.y), value + 1));
            }
            if (v.y  > 0 && map[v.x, v.y-1] != null&& floodFillMap[v.x, v.y-1] > floodFillMap[v.x, v.y] + 1)
            {
                queue.Enqueue(new KeyValuePair<Vector2Int, int>(new Vector2Int(v.x, v.y - 1), value + 1));
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
        if (Math.Abs(from.x - to.x) < 0.5 && Math.Abs(from.y - to.y) < 0.5)
        {
            return null;
        }
        int[,] FloodFill = GetFloodFill(map, to);
        int finalLen = Math.Min(pathLen, FloodFill[from.x, from.y]);
        Vector2Int[] path = new Vector2Int[finalLen];
        Vector2Int lastPoint = from;
        int lastValue = FloodFill[lastPoint.x, lastPoint.y];
        for (int i = 0; i < finalLen; i++)
        {
            Vector2Int bufPoint = new Vector2Int(0,0);
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
                lastPoint = bufPoint;
                continue;
            }
            
            bufPoint = new Vector2Int(0,0);
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
                lastPoint = bufPoint;
                continue;
            }
            
            bufPoint = new Vector2Int(0,0);
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
                lastPoint = bufPoint;
                continue;
            }
            
            bufPoint = new Vector2Int(0,0);
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
                lastPoint = bufPoint;
                continue;
            }
            
    }
        return path;
    }
}