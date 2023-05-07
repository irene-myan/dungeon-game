using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    public float pushForce = 2.0f;

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;
    private Animator anim;

    // Hit
    public float cooldown = 0.5f;
    public float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name != "Player")
        {
            Damage dmg = new Damage
            {
                dmgAmount = GameManager.instance.weapondDmg[weaponLevel],
                origin = transform.position,
                pushForce = weaponLevel < 4 ? pushForce : pushForce + 1.5f
            };

            coll.SendMessage("RecieveDamage", dmg);
        }

    }
    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int lvl)
    {
        weaponLevel = lvl;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[lvl];
    }

}
