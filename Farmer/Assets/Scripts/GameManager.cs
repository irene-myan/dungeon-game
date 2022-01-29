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
    public List<int> xpTable;

    public Player player;
    // public weapon weapon
    public FloatingTextManager ftManager;

    // Logic
    public int coins;
    public int experience;


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        ftManager.Show(msg, fontSize, color, position, motion, duration);
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
        s += "0"; // weapon level

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] loading = PlayerPrefs.GetString("SaveState").Split('|');

        // pref skin
        coins = int.Parse(loading[1]);
        experience = int.Parse(loading[2]);
        // weapon level
    }
}
