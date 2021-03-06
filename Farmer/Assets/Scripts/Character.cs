using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Animator animator;
    protected float xSpeed = 1.0f;
    protected float ySpeed = 0.8f;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }


    protected virtual void UpdateMotor(Vector3 input)
    {

        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        // Swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = Vector2.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector2(-1, 1);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
                                Mathf.Abs(moveDelta.y * ySpeed * Time.deltaTime), LayerMask.GetMask("Character", "Blocking"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * ySpeed * Time.deltaTime, 0);
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
                                Mathf.Abs(moveDelta.x * xSpeed * Time.deltaTime), LayerMask.GetMask("Character", "Blocking"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x * xSpeed * Time.deltaTime, 0, 0);
    }
}
