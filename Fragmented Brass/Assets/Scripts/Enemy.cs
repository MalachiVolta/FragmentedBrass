using UnityEngine;

public class Enemy : MonoBehaviour,ITargetable
{
[SerializeField]private int currentHealth=50;
 public void Hit(int amount)
 {
     currentHealth-=amount;
     if(currentHealth <= 0)
     {
         Die();
     }
 }

 public void Die(){
     Destroy(gameObject);
 }
}
