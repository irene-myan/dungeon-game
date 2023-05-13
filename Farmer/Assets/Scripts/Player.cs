using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private SpriteRenderer spriteRenderer;
    public int selectedSkin = 0;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitpoint = GameManager.instance.GetMaxHealth();
        GameManager.instance.charMenu.UpdateMenu();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDelta = new Vector3(x, y, 0);

        animator.SetFloat("Speed", moveDelta.sqrMagnitude);

        UpdateMotor(moveDelta);
    }

    public void SwapSprite(int skinId)
    {
        selectedSkin = skinId;

        if (!spriteRenderer)
        {
            Start();
        }
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
        animator.runtimeAnimatorController = GameManager.instance.playerAnimators[skinId];
    }
}
