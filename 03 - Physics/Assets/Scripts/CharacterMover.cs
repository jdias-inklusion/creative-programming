using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private Transform characterTransform;

    // Start is called before the first frame update
    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = characterTransform.position;

        if (Input.GetKey(KeyCode.D))
        {
            position.x = position.x + 0.005f;
        }

        if (Input.GetKey(KeyCode.A)) {
            position.x = position.x - 0.005f;
        }

        characterTransform.position = position;

    }
}
