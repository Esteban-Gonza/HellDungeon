using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable{

    //Damage Structure
    [Header("Damage")]
    public int[] damagePoint = {1,2,3,4,5,6,7};
    public float[] pushForce = {2.0f, 2.2f, 2.5f, 3.0f, 3.2f, 3.6f, 4.0f};

    //Upgrade

    [Space][Header("Upgrade")]
    public int weaponLevel = 0;
    [SerializeField] SpriteRenderer rend;

    //Swing
    [Space][Header("Swing")]
    float cooldown = 0.5f;
    float lastSwing;
    Animator anim;

    protected override void Start(){

        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update(){

        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)){

            if(Time.time -lastSwing > cooldown){

                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll){

        if(coll.tag == "Fighter"){

            if (coll.name == "Player")
                return;

            Damage dmg = new Damage{

                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.playerHit);
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    void Swing(){

        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon(){

        weaponLevel ++;
        rend.sprite = GameManager.instance.weaponsSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level){

        weaponLevel = level;
        rend.sprite = GameManager.instance.weaponsSprites[weaponLevel];
    }
}