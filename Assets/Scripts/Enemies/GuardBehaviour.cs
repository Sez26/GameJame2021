using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{

    public List<Vector2> path = new List<Vector2>();
    public int pathIndex = 0;

    public float speed = 2;

    private Rigidbody2D rb2d;
    private Vector2 lookVector = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        path.Insert(0, new Vector2(this.transform.position.x, this.transform.position.y));

        rb2d = this.gameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.freezeRotation = true;
        rb2d.isKinematic = false;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        this.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        lookVector = (path[pathIndex] - rb2d.position).normalized;

        var move = lookVector * Time.fixedDeltaTime * speed;

        rb2d.MovePosition(rb2d.position + move);

        if (this.rb2d.OverlapPoint(path[pathIndex])) {
            pathIndex++;

            if (pathIndex >= path.Count) {
                pathIndex = 0;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, lookVector * 12);
    }
}
