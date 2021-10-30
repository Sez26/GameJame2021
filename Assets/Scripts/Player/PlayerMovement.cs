using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5;

    private Vector3 movementVector = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized
            * speed * Time.deltaTime;

        this.transform.position = this.transform.position + movementVector;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, movementVector * 120);
    }
}
