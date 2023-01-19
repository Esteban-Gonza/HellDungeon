using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour{
    
    //Text Fields
    public TMP_Text levelText, hitPointsText, pesosText, upgradeCostText, xpText;

    //Logic
    int currentCharacterSelection = 0;
    bool menuIsOpen;
    public Image characterSelectionSprite, weaponSprite;
    public RectTransform xpBar;
    Animator menuUIAnimator;

    void Start() {
        
        menuUIAnimator = GetComponent<Animator>();
        menuIsOpen = false;
    }

    //Character Selection
    public void OnArrowClick(bool right){

        if(right){

            currentCharacterSelection++;

            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }else{

            currentCharacterSelection--;

            if(currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    void OnSelectionChanged(){

        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick(){

        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    public void UpdateMenu(){

        weaponSprite.sprite = GameManager.instance.weaponsSprites[GameManager.instance.weapon.weaponLevel];
        
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitPointsText.text = GameManager.instance.player.hitPoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        int currentLevel = GameManager.instance.GetCurrentLevel();

        if(currentLevel == GameManager.instance.xpTable.Count){

            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }else{

            int prevLevelXP = GameManager.instance.GetXPToLevel(currentLevel - 1);
            int currentLevelXP = GameManager.instance.GetXPToLevel(currentLevel);

            int diff = currentLevelXP - prevLevelXP;
            int currentXPIntoLevel = GameManager.instance.experience - prevLevelXP;

            float completionRadio = (float)currentXPIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRadio, 1 , 1);
            xpText.text = currentXPIntoLevel.ToString() + " / " + diff;
        }
    }

    void Update() {
        
        if(Input.GetKeyDown(KeyCode.P) && menuIsOpen == false){

            menuIsOpen = true;
            menuUIAnimator.SetTrigger("Show");
            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.button);
            UpdateMenu();

        } else if(Input.GetKeyDown(KeyCode.P) && menuIsOpen == true){

            menuIsOpen = false;
            menuUIAnimator.SetTrigger("Hide");
        }


    }
}