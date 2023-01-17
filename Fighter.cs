using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour{

    public int hitPoint;
    public int maxHitPoint;
    public float pushRecoverySpeed = 0.2f;

    [Space]
    protected float immuneTime = 1.0f;
    protected float lastImmune = 1.0f;

    [Space]
    protected Vector3 pushDirection;

    protected virtual void ReceiveDamage(Damage dmg){

        if (Time.time - lastImmune > immuneTime) {

            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            if(hitPoint <= 0){

                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death(){
    

    }
}