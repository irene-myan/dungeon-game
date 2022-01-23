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
            GetComponent<SpriteRenderer>().sprite = noCoins;
        }
    }
}
