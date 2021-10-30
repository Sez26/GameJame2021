using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraLockDistance = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.parent.GetComponent<PlayerController>().LookVector * cameraLockDistance
            + this.transform.parent.transform.position;
        this.transform.Translate(new Vector3(0, 0, -10));
    }
}
