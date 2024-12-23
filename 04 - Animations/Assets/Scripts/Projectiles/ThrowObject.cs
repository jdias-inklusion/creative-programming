using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    // “We can spawn prefabs from our scripts by using the Instantiate() function.
    // Just tell which prefab you want and where you want it to be spawned.”

    public Transform spawnPosition;
    public GameObject prefab;
    public float throwForce;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject spawnedObject = (GameObject)Instantiate(prefab, spawnPosition.position,
                spawnPosition.transform.rotation, null);

            // “In order to throw the newly created object all that we need to do is apply some
            // force to it in the direction we want to throw it.”

            Rigidbody rigidbody = spawnedObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(spawnPosition.transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}
