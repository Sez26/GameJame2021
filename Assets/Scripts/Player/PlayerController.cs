using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;

    private Vector2 movementVector = Vector2.zero;
    private Vector3 lookVector = new Vector3(1, 0, 0);

    private Rigidbody2D rb2d;

    public Vector3 LookVector {
        get { return lookVector; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.gameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        lookVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookVector.z = 0;
        lookVector = Vector3.Normalize(lookVector);
    }

    private void FixedUpdate() {
        movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized
            * speed * Time.fixedDeltaTime;

        rb2d.MovePosition(rb2d.position + movementVector);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Hit?");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, new Vector3(movementVector.x, movementVector.y, 0) * 120);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, lookVector * 2);
    }
}