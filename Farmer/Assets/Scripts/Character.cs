using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Animator animator;
    public float xSpeed = 1.0f;
    public float ySpeed = 0.8f;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    private Vector2 scale;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        scale.x = Mathf.Abs(transform.localScale.x);
        scale.y = transform.localScale.y;
    }


    protected virtual void UpdateMotor(Vector3 input)
    {

        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        // Swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = new Vector2(scale.x, scale.y);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector2(-scale.x, scale.y);

        // Push vector
        moveDelta += pushDir;

        // Reduce pushDir every frame, based off recovery speed
        pushDir = Vector3.Lerp(pushDir, Vector3.zero, pushRecoverySpeed);

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
