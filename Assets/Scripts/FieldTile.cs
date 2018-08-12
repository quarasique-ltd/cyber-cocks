using System.Collections.Generic;
using UnityEngine;

public class FieldTile
 {
     private int hp;
     private int fieldState;
     private List<int> fieldTileHealthPoints;
     private bool forRedraw = true;

     public FieldTile(int fieldTileHealth, ref List<int> fieldTileHealthPoints)
     {
         hp = fieldTileHealth;
         this.fieldTileHealthPoints = fieldTileHealthPoints;
     }

     public void takeDamage(int damage)
     {
         hp -= damage;
         checkHealth();
     }

     private void checkHealth()
     {
         if (fieldState < fieldTileHealthPoints.Count)
         {
             if (hp < fieldTileHealthPoints[fieldState])
             {
                 fieldState++;
                 forRedraw = true;
             }
         }
     }

     public int getHealth()
     {
         return hp;
     }
     
     public int getState()
     {
         return fieldState;
     }

     public bool isForRedraw()
     {
         return forRedraw;
     }

     public void wasRedrawed()
     {
         forRedraw = false;
     }
 }