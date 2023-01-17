using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable{
    
    //Damage
    public int damage;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D coll){

        if(coll.tag == "Fighter" && coll.name == "Player"){

            //Create damage object
            Damage dmg = new Damage{

                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.enemyHit);
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}