using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour{
    
    public static SoundManager instance;

    public AudioSource audioSource;

    public AudioClip playerHit;
    public AudioClip enemyHit;
    public AudioClip startSound;
    public AudioClip heal;
    public AudioClip breakCrate;
    public AudioClip coin;
    public AudioClip button;

    void Awake() {
        
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        
        audioSource.PlayOneShot(startSound);
    }

    public void ButtonSound(){
        audioSource.PlayOneShot(button);
    }
}