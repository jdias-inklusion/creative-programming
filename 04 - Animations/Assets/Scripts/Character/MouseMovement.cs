using UnityEngine;
using System.Collections;

public class MouseMovement : MonoBehaviour
{
    public Transform characterTransform;
    public float rotationSpeed;

    // Use this for initialization
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        float horizontalRotation = Input.GetAxis("Mouse X") *
            rotationSpeed;
        characterTransform.Rotate(0, horizontalRotation, 0);
    }
}

