using System.Collections.Generic;
using UnityEngine;
 
 public class FieldTile: MonoBehaviour
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
         if (hp < fieldTileHealthPoints[fieldState])
         {
             if (fieldState < fieldTileHealthPoints.Count)
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