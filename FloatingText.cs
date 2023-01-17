using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FloatingText{

    public float duration;
    public float lastShown;
    public bool active;
    public Vector3 motion;
    public GameObject go;
    public TMP_Text txt;
    
    public void Show(){

        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide(){

        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText(){

        if (!active)
            return;

        if(Time.time -lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}