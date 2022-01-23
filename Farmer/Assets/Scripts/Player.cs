using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Animator animator;
    public float moveSpeed = 1f;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    // Start is called before the first frame update
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", moveDelta.sqrMagnitude);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Reset MoveDelta


        // Swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = Vector2.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector2(-1, 1);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
                                Mathf.Abs(moveDelta.y * moveSpeed * Time.deltaTime), LayerMask.GetMask("Character", "Blocking"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * moveSpeed * Time.deltaTime, 0);
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
                                Mathf.Abs(moveDelta.x * moveSpeed * Time.deltaTime), LayerMask.GetMask("Character", "Blocking"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x * moveSpeed * Time.deltaTime, 0, 0);

    }
}
