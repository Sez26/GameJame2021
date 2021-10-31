using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)  {
        if(other.gameObject.tag == player)  {
            Application.LoadLevel((Application.LoadedLevel + 1) % 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
