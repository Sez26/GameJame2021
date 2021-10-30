using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5;

    private Vector3 movementVector = Vector3.zero;
    private Vector3 lookVector = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementVector = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0))
            * speed * Time.deltaTime;

        lookVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        lookVector.z = 0;
        lookVector = Vector3.Normalize(lookVector);

        this.transform.position = this.transform.position + movementVector;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, movementVector * 120);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, lookVector * 2);
    }
}
