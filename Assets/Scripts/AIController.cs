using System;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour, IPlayerController
{
    public const float Velocity = 0.05f;
    public Player Player;
    private GameManager gameManager;
    private GridLayout gridLayout;

    private Vector2Int currentWaypoint;

    // Use this for initialization
    void Start()
    {
        Player = GetComponent<Player>();
        GameObject gameInitializer = GameObject.Find("GameInitializer");
        gameManager = gameInitializer.GetComponent<GameManager>();
        gridLayout = GameObject.Find("Grid(Clone)").GetComponent<GridLayout>();
    }

    int cellGoodNess(FieldTile tile)
    {
        if (tile == null)
        {
            return 0;
        }
        return 5 - tile.getState();
    }

    Vector2Int ChooseWayPoint(FieldTile[,] map, Vector2Int from)
    {
        GameObject[] anotherPlayers = GameObject.FindGameObjectsWithTag("Player");
        float[,] cellsGoodness = new float[map.GetLength(0), map.GetLength(1)];
        float sum = 0;
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int g = 1; g < map.GetLength(1) - 1; g++)
            {
                cellsGoodness[i, g] = cellGoodNess(map[i, g]);
                cellsGoodness[i, g] += cellGoodNess(map[i - 1, g - 1]);
                cellsGoodness[i, g] += cellGoodNess(map[i - 1, g]);
                cellsGoodness[i, g] += cellGoodNess(map[i - 1, g + 1]);
                cellsGoodness[i, g] += cellGoodNess(map[i, g - 1]);
                cellsGoodness[i, g] += cellGoodNess(map[i, g + 1]);
                cellsGoodness[i, g] += cellGoodNess(map[i + 1, g - 1]);
                cellsGoodness[i, g] += cellGoodNess(map[i + 1, g]);
                cellsGoodness[i, g] += cellGoodNess(map[i + 1, g + 1]);

                if (Math.Abs(from.x - i) > 0.01f && Math.Abs(from.y - g) > 0.01f)
                {
                    cellsGoodness[i, g] /= Vector2.Distance(from, new Vector2(i, g));
                }

                sum += cellsGoodness[i, g];
            }
        }
        List<float> probabilities = new List<float>(map.GetLength(0) * map.GetLength(1));
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int g = 0; g < map.GetLength(1); g++)
            {
                cellsGoodness[i, g] /= sum;
                probabilities.Add(cellsGoodness[i, g]);
            }
        }

        int result = RandomFromDistribution.RandomChoiceFollowingDistribution(probabilities);
        return new Vector2Int(result % cellsGoodness.GetLength(0), result / cellsGoodness.GetLength(0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3Int bufPos;
        if ((currentWaypoint.x != 0 && currentWaypoint.y != 0) &&
            (Math.Abs(currentWaypoint.x - transform.position.x) > 1.1 ||
             Math.Abs(currentWaypoint.y - transform.position.y) > 1.1))
        {
            bufPos = gridLayout.WorldToCell(new Vector3(transform.position.x, transform.position.y, 0));
            Vector2 direction = gridLayout.CellToWorld(new Vector3Int(currentWaypoint.x, currentWaypoint.y, 0));
            direction = direction - new Vector2(bufPos.x, bufPos.y);
            direction = direction.normalized;
            Player.Move(direction * Velocity);
            return;
        }
        bufPos = gridLayout.WorldToCell(new Vector3(transform.position.x, transform.position.y, 0));
        Vector2Int from = new Vector2Int(bufPos.x, bufPos.y);
        FieldTile[,] map = gameManager._field.getArray();
        currentWaypoint = ChooseWayPoint(map, from);
        Debug.Log(currentWaypoint);
        Vector2Int[] path = FloodFill.GetPath(map, from, currentWaypoint, 3);
        if (path != null)
        {
            if (path.GetLength(0) > 1)
            {
                currentWaypoint = path[1];
            }
            else
            {
                if (path.GetLength(0) > 0)
                {
                    currentWaypoint = path[0];
                }
                else
                {
                    return;
                }
            }
            Vector2 direction = gridLayout.CellToWorld(new Vector3Int(currentWaypoint.x, currentWaypoint.y, 0));
            direction = direction - new Vector2(bufPos.x, bufPos.y);
            direction = direction.normalized;
            Player.Move(direction * Velocity);
        }
    }

    public void setPlayer(Player player)
    {
        this.Player = player;
    }
}