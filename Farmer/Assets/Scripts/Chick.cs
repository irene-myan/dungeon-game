using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : Collectable
{
    protected override void OnCollect()
    {
        if (!collected)
        {
            base.OnCollect();
            Debug.Log("CHIKENC");
        }
    }
}
