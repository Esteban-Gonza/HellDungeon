using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextManager : MonoBehaviour{

    public GameObject textContainer;
    public GameObject textPrefab;

    List<FloatingText> floatingText = new List<FloatingText>();

    void Update(){

        foreach (FloatingText txt in floatingText){
        
            txt.UpdateFloatingText();
        }
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){

        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text = message;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); //Transfer world space to screen space
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    FloatingText GetFloatingText(){

        FloatingText txt = floatingText.Find(t => !t.active);

        if(txt == null){

            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<TMP_Text>();

            floatingText.Add(txt);
        }

        return txt;
    }
}