using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText, healthText, coinsText, upgradeCostText, xpText;

    private int curCharSelected;
    public Image charSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;
    public RectTransform healthBar;

    private void Start()
    {
        curCharSelected = GameManager.instance.player.selectedSkin;
        charSelectionSprite.sprite = GameManager.instance.playerSprites[curCharSelected];
    }

    // Character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            curCharSelected++;
            if (curCharSelected == GameManager.instance.playerSprites.Count)
            {
                curCharSelected = 0;
            }
        }
        else
        {
            curCharSelected--;
            if (curCharSelected == -1)
            {
                curCharSelected = GameManager.instance.playerSprites.Count - 1;
            }
        }

        charSelectionSprite.sprite = GameManager.instance.playerSprites[curCharSelected];
        GameManager.instance.player.SwapSprite(curCharSelected);
    }

    // Weapon upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.UpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    // Char info
    public void UpdateMenu()
    {
        int curWeaponLevel = GameManager.instance.weapon.weaponLevel;
        weaponSprite.sprite = GameManager.instance.weaponSprites[curWeaponLevel];
        if (curWeaponLevel == GameManager.instance.weaponPrices.Count - 1)
        {
            upgradeCostText.text = "---";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[curWeaponLevel + 1].ToString();
        }


        healthText.text = GameManager.instance.player.hitpoint.ToString() + "/" + GameManager.instance.GetMaxHealth().ToString();
        healthBar.localScale = new Vector3((float)GameManager.instance.player.hitpoint / GameManager.instance.GetMaxHealth(), 1f, 0);

        coinsText.text = GameManager.instance.coins.ToString() + " coins";

        levelText.text = GameManager.instance.level.ToString();
        xpText.text = GameManager.instance.experience.ToString() + "/" + GameManager.instance.xpTable[GameManager.instance.level - 1];
        xpBar.localScale = new Vector3((float)GameManager.instance.experience / GameManager.instance.xpTable[GameManager.instance.level - 1], 1f, 0);
    }
}
