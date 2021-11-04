using UnityEngine;

public class Targetable : MonoBehaviour
{ HealthSys healthSystem = new HealthSys(50);

 public void Hit(int amount)
 {
     healthSystem.Damage(amount);
     if(healthSystem.getHealth() <= 0)
     {
         Die();
     }
 }

 void Die(){
     Destroy(gameObject);
 }
}
