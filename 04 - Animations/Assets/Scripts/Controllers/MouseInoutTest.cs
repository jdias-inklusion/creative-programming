using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInoutTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Mouse X: " + Input.GetAxis("Mouse X"));
        Debug.Log("Mouse Y: " + Input.GetAxis("Mouse Y"));
    }
}
