using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpike : EnemyWeapon
{
    protected Animator animator;

    private bool open = false;
    private float timer = 0f;

    protected override void Update()
    {
        // Collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null || !open)
            {
                continue;
            }

            OnCollide(hits[i]);

            hits[i] = null;
        }

        if (open)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if 2 seconds have passed
            if (timer >= 5f)
            {
                // Reset the timer and waiting flag
                timer = 0f;
                animator.SetBool("Open", false);
                open = false;
            }
        }
        else if (!open)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if 2 seconds have passed
            if (timer >= 5f)
            {
                // Reset the timer and waiting flag
                timer = 0f;
                animator.SetBool("Open", true);
                open = true;
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
}

