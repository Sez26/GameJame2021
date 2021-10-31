using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraLockDistance = 4;
    public float cameraMoveSpeed = 10;

    private Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var target = this.transform.parent.GetComponent<PlayerController>().LookVector * cameraLockDistance
            + this.transform.parent.transform.position;
        this.transform.Translate(new Vector3(0, 0, 10));
        this.transform.position = Vector2.MoveTowards(prevPos, target, cameraLockDistance * cameraMoveSpeed * Time.deltaTime);
        this.transform.Translate(new Vector3(0, 0, -10));
        prevPos = this.transform.position;
    }
}
