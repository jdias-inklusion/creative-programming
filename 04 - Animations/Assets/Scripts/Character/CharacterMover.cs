using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float speed;
    public float jumpingForce;

    // Start is called before the first frame update
    void Start()
    {
    }

    // “The Update event function should be used for general updating and etc.”
    void Update()
    {
    }

    // “FixedUpdate should be used whenever we are dealing with the physics engine.”
    void FixedUpdate()
    {
        Vector3 direction = new Vector3();

        if (Input.GetKey("up"))
        {
            direction.z += 1;
        }

        if (Input.GetKey("down"))
        {
            direction.z -= 1;
        }

        if (Input.GetKey("left"))
        {
            direction.x -= 1;
        }

        if (Input.GetKey("right"))
        {
            direction.x += 1;
        }

        // “Now that we are using the physics engine we need to stop using Transform.position
        // directly since that will interfere with the physics engine.
        // Now we should instead use RigidBody.MovePosition in order to move.”
        GetComponent<Rigidbody>().MovePosition(transform.position + (direction * speed));

        // “Now that we have physics we can do cool things like having our character jump by
        // simply applying a force to it pointing upwards.
        // The animation will be dealt with perfectly by gravity and the physics engine.”

        if (Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * jumpingForce, ForceMode.Impulse);
        }

    }
}
