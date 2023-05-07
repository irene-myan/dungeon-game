using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText, healthText, coinsText, upgradeCostText, xpText;

    private int curCharSelected = 0;
    public Image charSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;
    public RectTransform healthBar;

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
            upgradeCostText.text = "END";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[curWeaponLevel + 1].ToString();
        }


        healthText.text = GameManager.instance.player.hitpoint.ToString();
        healthBar.localScale = new Vector3(0.5f, 0, 0);

        coinsText.text = GameManager.instance.coins.ToString() + " coins";

        levelText.text = "NA";
        xpText.text = "NA";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
    }

}
