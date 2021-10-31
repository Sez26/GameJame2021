using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask;
    public List<Vector2> path = new List<Vector2>();
    public int pathIndex = 0;

    public float speed = 2;

    private Rigidbody2D rb2d;
    private Vector2 lookVector = Vector2.right;
    private bool turning = true;

    public float viewAngle = 0.785f; // 45deg
    public float lookDist = 5f;

    public GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        path.Insert(0, new Vector2(this.transform.position.x, this.transform.position.y));

        rb2d = this.gameObject.AddComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.freezeRotation = true;
        rb2d.isKinematic = true;
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        player = GameObject.FindGameObjectsWithTag("Player")[0];

        this.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        var move = lookVector * Time.fixedDeltaTime * speed;
        var target = (path[pathIndex] - rb2d.position).normalized;

        if (Vector2.Dot(lookVector, target) > 0.999) {
            turning = false;
        } else {
            turning = true;
        }

        if (turning) {
            var target3 = new Vector3(target.x, target.y, 0);
            var look3 = new Vector3(lookVector.x, lookVector.y, 0);
            look3 = Vector3.RotateTowards(look3, target3, 0.02f, 0.0f);
            lookVector = new Vector2(look3.x, look3.y).normalized;
        } else {
            rb2d.MovePosition(rb2d.position + move);
        }

        if (this.rb2d.OverlapPoint(path[pathIndex])) {
            turning = true;
            pathIndex++;

            if (pathIndex >= path.Count) {
                pathIndex = 0;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if (Application.isPlaying) {
            if (CanSee(player)) {
                Gizmos.color = Color.green;
            }
        }

        Gizmos.DrawRay(transform.position, lookVector * 2);
    }

    public bool CanSee(GameObject obj) {
        var otherPos = obj.GetComponent<Rigidbody2D>().position;
        var diff = (otherPos - rb2d.position);
        var cosTheta = Vector2.Dot(diff.normalized, lookVector);
        if (cosTheta >= Mathf.Cos(viewAngle) && (Vector2.Dot(diff, diff) <= lookDist*lookDist)) {
            var hit = Physics2D.Raycast(rb2d.position, diff.normalized, lookDist, layerMask);
            if (hit.collider == null) {
                return true;
            }

            if ((hit.point - rb2d.position).magnitude >= diff.magnitude) {
                return true;
            }
        }

        return false;
    }
}
