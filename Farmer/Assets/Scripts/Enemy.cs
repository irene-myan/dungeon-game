using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // XP
    public int xpValue = 1;

    // Logic
    public float triggerLength = 0.5f;
    public float chaseLength = 7;
    private bool chasing;
    private bool collidingWPlayer;
    private Transform playerTransform;
    private Vector3 startPos;

    // Hitbox
    private BoxCollider2D hitbox;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;

        startPos = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {

        if (Vector3.Distance(playerTransform.position, startPos) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startPos) < triggerLength)
            {
                chasing = true;
            }

            if (chasing && !collidingWPlayer)
            {
                UpdateMotor((playerTransform.position - transform.position).normalized);
            }
            else if (!chasing)
            {
                UpdateMotor(startPos - transform.position);
            }
        }
        else
        {
            UpdateMotor(startPos - transform.position);
            chasing = false;
        }


        collidingWPlayer = false;
        // Collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWPlayer = true;
            }

            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.AddXP(xpValue);
        GameManager.instance.ShowText("+" + xpValue.ToString() + " XP", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        GameManager.instance.charMenu.UpdateMenu();
    }
}
