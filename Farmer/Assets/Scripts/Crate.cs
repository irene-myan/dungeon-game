﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    protected override void Death()
    {

        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
