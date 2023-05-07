using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public int[] weapondDmg = { 1, 2, 3, 4, 6, 8 };
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;
    public FloatingTextManager ftManager;

    public CharacterMenu charMenu;

    // Logic
    public int coins;
    public int experience;


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        ftManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public bool UpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel + 1)
        {
            return false;
        }
        else if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }


    /*
        int preferedSkin
        int coins
        int experience
        int weaponLevel
    */
    public void SaveState()
    {
        string s = "";
        s += "0" + "|"; // pref skin
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString(); // weapon level

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log(s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] loading = PlayerPrefs.GetString("SaveState").Split('|');
        Debug.Log(loading);

        // pref skin
        coins = int.Parse(loading[1]);
        experience = int.Parse(loading[2]);
        weapon.SetWeaponLevel(int.Parse(loading[3]));
    }
}
