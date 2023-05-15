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
            Destroy(player.gameObject);
            Destroy(ftManager.gameObject);
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<RuntimeAnimatorController> playerAnimators;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public int[] weapondDmg = { 1, 2, 3, 4, 6, 8 };
    public List<int> xpTable;

    public Player player;
    public Weapon weapon;
    public FloatingTextManager ftManager;

    public CharacterMenu charMenu;

    public int coins;
    public int experience;
    public int level;


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

    public void AddXP(int xp)
    {
        experience += xp;
        // Level up
        while (experience >= xpTable[level - 1])
        {
            if (level < xpTable.Count)
            {
                experience -= xpTable[level - 1];
                ++level;
                player.hitpoint = xpTable[level - 1] * 2;
            }
        }
    }

    public int GetMaxHealth()
    {
        return xpTable[level - 1] * 2;
    }

    public void SaveState()
    {
        string s = "";
        s += player.selectedSkin.ToString() + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += level.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] loading = PlayerPrefs.GetString("SaveState").Split('|');
        Debug.Log(PlayerPrefs.GetString("SaveState"));

        player.SwapSprite(int.Parse(loading[0]));
        coins = int.Parse(loading[1]);
        experience = int.Parse(loading[2]);
        level = int.Parse(loading[3]);
        weapon.SetWeaponLevel(int.Parse(loading[4]));

        player.transform.position = GameObject.Find("Spawn").transform.position;
        if (s.name == "bedroom" || s.name == "catRoom")
        {
            weapon.gameObject.SetActive(false);
        }
        else
        {
            weapon.gameObject.SetActive(true);
        }

        charMenu = FindObjectOfType<CharacterMenu>();
        charMenu.UpdateMenu();
    }
}
