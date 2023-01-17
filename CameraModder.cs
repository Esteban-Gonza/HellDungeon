using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModder : MonoBehaviour{

    [SerializeField] float boundX = 0.15f;
    [SerializeField] float boundY = 0.05f;
    Transform lookAt;

    void Start(){

        lookAt = GameObject.Find("Player").transform;
    }

    void LateUpdate(){

        Vector3 delta = Vector3.zero;

        //Check if we are inside the bounds on X axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX){

            if (transform.position.x < lookAt.position.x){

                delta.x = deltaX - boundX;
            }else{

                delta.x = deltaX + boundX;
            }
        }
        
        //Check if we are inside the bounds on Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY){

            if (transform.position.y < lookAt.position.y){

                delta.y = deltaY - boundY;
            }else{

                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3 (delta.x, delta.y, 0);
    }
}