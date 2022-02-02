using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDelta = new Vector3(x, y, 0);

        animator.SetFloat("Speed", moveDelta.sqrMagnitude);

        UpdateMotor(moveDelta);
    }
}
