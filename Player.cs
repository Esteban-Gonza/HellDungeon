using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover{

    SpriteRenderer rend;
    bool isAlive = true;

    protected override void Start(){

        base.Start();
        rend = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage dmg){
        
        if(!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.magenta, transform.position, Vector3.zero, 0.3f);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Death(){

        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.startSound);
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
        this.gameObject.SetActive(false);
    }

    void FixedUpdate(){

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(isAlive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinID){

        rend.sprite = GameManager.instance.playerSprites[skinID];
    }
    public void OnLevelUP(){

        maxHitPoint += 20;
        hitPoint = maxHitPoint;
    }
    public void SetLvel(int level){

        for(int i = 0; i < level; i++){

            OnLevelUP();
        }
    }
    public void Heal(int heallingAmount){

        if(hitPoint == maxHitPoint)
            return;

        hitPoint += heallingAmount;

        if(hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;

        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.heal);
            
        GameManager.instance.ShowText("+" + heallingAmount.ToString() + "hp",
            20, Color.green, transform.position, Vector3.up * 30, 1.0f);
        
        GameManager.instance.OnHitpointChange();
    }
    public void Respawn(){

        this.gameObject.SetActive(true);
        Heal(maxHitPoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}