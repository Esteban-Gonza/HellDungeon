using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate1 : Fighter{

    protected override void Death(){
        
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.breakCrate);
        this.gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}