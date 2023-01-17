using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter{
    
    Vector3 originalSize;
    Vector3 moveDelta;
    RaycastHit2D hit;
    BoxCollider2D playerCollider;

    [SerializeField] protected float ySpeed = 0.75f;
    [SerializeField] protected float xSpeed = 1.0f;

    protected virtual void Start(){
        
        originalSize = transform.localScale;
        playerCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input){

        //Reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if(moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        //Add push vector
        moveDelta += pushDirection;

        //Reduce push each frame
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Make sure we can move by cast a box. y coordinates
        hit = Physics2D.BoxCast(transform.position,playerCollider.size, 0, new Vector2(0, moveDelta.y), 
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Character","Blocking"));

        if(hit.collider == null){

            //Movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //Make sure we can move by cast a box. x coordinates
        hit = Physics2D.BoxCast(transform.position, playerCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Character", "Blocking"));

        if (hit.collider == null){

            //Movement
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}