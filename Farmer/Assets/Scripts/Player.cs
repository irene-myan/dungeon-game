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
        maxHitpoint = hitpoint;
        GameManager.instance.charMenu.UpdateMenu();

        DontDestroyOnLoad(gameObject);
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

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint) return;

        hitpoint += healingAmount;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 20, Color.green, transform.position, Vector3.up * 50, 1.2f);
        GameManager.instance.charMenu.UpdateMenu();
    }

    protected override void Death()
    {
        base.Death();
        UnityEngine.SceneManagement.SceneManager.LoadScene("bedroom");
        GameManager.instance.coins = 0;
        GameManager.instance.level = 1;
        GameManager.instance.experience = 0;
        GameManager.instance.weapon.weaponLevel = 0;
        hitpoint = GameManager.instance.GetMaxHealth();
        maxHitpoint = hitpoint;
        GameManager.instance.SaveState();
    }
}
