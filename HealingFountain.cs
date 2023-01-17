using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable{
    
    public int healingAmount = 10;
    float healCooldown = 1.0f;
    float lastHeal;

    private void OnTriggerEnter2D(Collider2D coll) {
        
        if(coll.name != "Player")
            return;

        if(Time.time - lastHeal > healCooldown){

            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}