using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    
    public static GameManager instance;

    //Resources
    [Header("Resources")]
    public List<Sprite> weaponsSprites;
    public List<Sprite> playerSprites;
    public List<int> weaponPrices;

    public List<int> xpTable;

    //References
    [Space][Header("References")]
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpoitBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnim;

    //Amounts
    [Space][Header("Amounts")]
    public int pesos;
    public int experience;

    void Awake() {

        if (GameManager.instance != null){

            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){

        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon(){

        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;
        
        if(pesos >= weaponPrices[weapon.weaponLevel]){

            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //HitPoit Bar
    public void OnHitpointChange(){

        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitpoitBar.localScale = new Vector3(ratio, 1, 1);
    }

    //Experience system
    public int GetCurrentLevel(){

        int r = 0;
        int add = 0;

        while(experience >= add){

            add += xpTable[r];
            r++;

            if(r == xpTable.Count)
                return r;
        }

        return r;
    }
    public int GetXPToLevel(int level){

        int r = 0;
        int xp = 0;

        while(r < level){

            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXP(int xp){

        int currentLevel = GetCurrentLevel();
        experience += xp;
        if(currentLevel < GetCurrentLevel())
            OnLevelUp();
    }
    public void OnLevelUp(){

        player.OnLevelUP();
        OnHitpointChange();
        ShowText("LEVEL UP!!", 80, Color.green, transform.position, Vector3.zero, 1.5f);
    }

    //On Scene loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode){

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //Death Menu and Respawn
    public void Respawn(){

        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
        pesos = 0;
    }

    //Save state
    public void SaveState(){

        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode){

        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLvel(GetCurrentLevel());

        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}