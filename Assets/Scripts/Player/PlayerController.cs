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


    public GameObject bullet;

    public float shootCooldown = 5;
    private float shootTimer = 0;

    private Vector2 jumpTo;
    private bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.gameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.freezeRotation = true;
        rb2d.isKinematic = false;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        this.gameObject.tag = "Player";
    }

    public void JumpTo(Vector2 pos) {
        jumpTo = pos;

        Debug.Log("JuMP!");

        jumping = true;
    }

    public void Fire() {
        Debug.Log("banG!");
        GameObject b = Instantiate(bullet, this.transform.position + lookVector, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().velocity = lookVector * speed * 1.5f;
        b.GetComponent<BulletBehaviour>().player = this.gameObject;
        b.GetComponent<BulletBehaviour>().startingVelocity =  lookVector * speed * 1.5f;
        shootTimer = shootCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        lookVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookVector.z = 0;
        lookVector = Vector3.Normalize(lookVector);

        if (Input.GetAxis("Fire1") + Input.GetAxis("Jump") >= 1 && shootTimer <= 0) {
            Fire();
        } else if (shootTimer > 0 ) {
            shootTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized
            * speed * Time.fixedDeltaTime;

        rb2d.MovePosition(rb2d.position + movementVector);

        if (jumping) {
            jumping = false;
            rb2d.MovePosition(jumpTo);
        }
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
