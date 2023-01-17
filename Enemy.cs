using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover{
    
    //Healt
    [Space][Header("Healt")]
    [SerializeField] int healtLowRange;
    [SerializeField] int healtHighRange;

    //XP
    [Space][Header("XP")]
    [SerializeField] int xpLowRange;
    [SerializeField] int xpHighRange;
    public int xpValue;

    //Logic
    [Space][Header("Logic")]
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    bool chasing;
    bool collidingWithPlayer;
    Transform playerTransform;
    Vector3 startingPosition;

    //Hitbox
    [Space][Header("Hitbox")]
    public ContactFilter2D filter;
    BoxCollider2D hitBox;
    Collider2D[] hits = new Collider2D[10];

    protected override void Start(){
        
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        xpValue = Random.Range(xpLowRange, xpHighRange);
        maxHitPoint = Random.Range(healtLowRange, healtHighRange);
        hitPoint = maxHitPoint;
    }

    void FixedUpdate(){

        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght){

            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                chasing = true;

            if(chasing){
                if(!collidingWithPlayer){
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }else{
                UpdateMotor(startingPosition - transform.position);
            }
        }else{
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps
        collidingWithPlayer = false;
        hitBox.OverlapCollider(filter, hits);
        
        for (int i = 0; i < hits.Length; i++){

            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player"){

                collidingWithPlayer = true;
            }

            //Clean the array
            hits[i] = null;
        }
    }

    protected override void Death(){
        
        Destroy(gameObject);
        GameManager.instance.GrantXP(xpValue);
        GameManager.instance.ShowText("+" + xpValue + "xp", 30, Color.red, transform.position, Vector3.up, 1.0f);
    }
}