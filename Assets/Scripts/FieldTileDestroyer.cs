using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class FieldTileDestroyer : MonoBehaviour
{
    public int damagePower;
    
    private Field field;
    private Dictionary<Vector2Int, int>  targets = new Dictionary<Vector2Int, int>();


    public void Start()
    {
        field = GetComponent<Field>();
        InitialSearchTargets();
    }

    private void FixedUpdate()
    {
        InitialSearchTargets();
        toHurt();
    }

    private void InitialSearchTargets()
    {
        if(field.getArray() == null) return;
        targets.Clear();
        for(int x = 1; x < (field.getArray().GetLength(0) - 1); x++)
        {
            for(int y = 1; y < (field.getArray().GetLength(1) - 1); y++)
            {
                int count = countEmptyNeighbors(x, y);
                if (count > 0)
                {
                    targets.Add(new Vector2Int(x, y), count);
                }
            }
        }
    }

    private void toHurt()
    {
        Debug.Log("TOOOO HURRRRRRRT");
        foreach (var target in targets)
        {
            if (field.getArray()[target.Key.x, target.Key.y] != null)
            {
                field.AttackTiel(target.Key.x, target.Key.y, target.Value * damagePower);
                //field.getArray()[target.Key.x, target.Key.y].takeDamage(target.Value * damagePower);   
            }
        }
    }


    private void SearchTargets()
    {
        /*foreach (var target in COLLECTION)
        {
            
        }*/
    }

    private int countEmptyNeighbors(int x, int y)
    {
        int count = 0;
        if (field.getArray()[x - 1 , y + 1] == null) count++;
        if (field.getArray()[x + 0 , y + 1] == null) count++;
        if (field.getArray()[x + 1 , y + 1] == null) count++;
        
        if (field.getArray()[x - 1 , y - 1] == null) count++;
        if (field.getArray()[x + 0 , y - 1] == null) count++;
        if (field.getArray()[x + 1 , y - 1] == null) count++;
        
        if (field.getArray()[x - 1 , y] == null) count++;
        if (field.getArray()[x + 1 , y] == null) count++;

        return count;
    }

    public Dictionary<Vector2Int, int> getTargets()
    {
        return targets;
    }
}