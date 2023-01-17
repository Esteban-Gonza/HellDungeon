using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable{

    [SerializeField] Sprite emptyChest;
    [SerializeField] int minPesosAmaount;
    [SerializeField] int maxPesosAmaount;
    int pesosAmaount;

    protected override void Start() {
        
        base.Start();
        pesosAmaount = Random.Range(minPesosAmaount, maxPesosAmaount);
    }

    protected override void OnCollect(){

        if (!collected){

            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.coin);
            GameManager.instance.pesos += pesosAmaount;
            GameManager.instance.ShowText("+$"+ pesosAmaount, 25, Color.yellow, transform.position, Vector3.up * 25, 1f) ;
        }
    }
}