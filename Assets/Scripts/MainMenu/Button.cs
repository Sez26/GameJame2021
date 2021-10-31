using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    
    void OnMouseUp()    {
        if(isStart) {
            Debug.Log("running");
            Application.LoadLevel(1);
        }
        
       
    }
}
