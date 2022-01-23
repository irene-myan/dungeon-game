using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Collectable
{
    public Sprite noHarvest;
    public int numTomat = 2;

    protected override void OnCollect()
    {
        if (!collected)
        {
            base.OnCollect();
            Debug.Log("TOMAT " + numTomat);
            GetComponent<SpriteRenderer>().sprite = noHarvest;
        }
    }
}
