using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    private Rigidbody2D rb2d;

    public GameObject player;

    public Vector2 startingVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.freezeRotation = true;
        rb2d.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        player.GetComponent<PlayerController>().JumpTo(this.transform.position);

        Destroy(this.gameObject);

        if (other.gameObject.tag == "Enemy") {
            Destroy(other.gameObject);
        }
    }
}
