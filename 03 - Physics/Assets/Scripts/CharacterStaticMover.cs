using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStaticMover : MonoBehaviour
{
    private Transform characterTransform;

    // Start is called before the first frame update
    void Start()
    {
        characterTransform = GetComponent<Transform>();
        Debug.Log("Character Position -> " + characterTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = characterTransform.position;

        position.x = position.x + 0.005f;

        characterTransform.position = position;
    }
}
