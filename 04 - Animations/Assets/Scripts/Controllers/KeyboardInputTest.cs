using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) {
            Debug.Log("Get Right Key Down");
        }

        if (Input.GetKey(KeyCode.D)) {
            Debug.Log("Get Right Key");
        }

        if (Input.GetKeyUp(KeyCode.D)) {
            Debug.Log("Gey Right Key up");
        }
    }
}
