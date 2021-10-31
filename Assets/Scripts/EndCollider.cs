using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    private void OnCollisionEnter2D(Collision2D other)  {
        Debug.Log("yes");
        if(other.gameObject.tag == "Player")  {
            Application.LoadLevel((int)((Application.loadedLevel + 1) % 5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
