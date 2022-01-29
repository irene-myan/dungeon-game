using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite noCoins;
    public int numCoins = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            base.OnCollect();
            Debug.Log("COIN " + numCoins);
            GameManager.instance.coins += numCoins;
            GetComponent<SpriteRenderer>().sprite = noCoins;
            GameManager.instance.ShowText("+" + numCoins + " coins!", 20, Color.magenta, transform.position, Vector3.up * 50, 1.2f);
        }
    }
}
