using System.Collections.Generic;
using UnityEngine;
 
 public class FieldTile
 {
     private int hp;
     private int fieldState = 0;
     private List<int> fieldTileHealthPoints;

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
             }
         }
     }

     public int getState()
     {
         return fieldState;
     }
 }