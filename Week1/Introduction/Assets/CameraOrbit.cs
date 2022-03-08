using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public Vector2 orbitAngles = new Vector2(0f, 0f);
    public Vector3 offset = new Vector2(0f, 0f);
    public float rotationSpeed = 1.0f;
    public float distanceFromTarget = 50.0f;

    void ManualRotation() {
		Vector2 input = new Vector2(
			Input.GetAxis("Vertical"),
			Input.GetAxis("Horizontal")
		);
        orbitAngles += rotationSpeed * input;
	}
    
    void LateUpdate () {
        ManualRotation();
		Quaternion lookRotation = Quaternion.Euler(orbitAngles);
		Vector3 lookDirection = lookRotation * Vector3.forward;
		Vector3 lookPosition = (target.position - lookDirection * distanceFromTarget) + offset;
		transform.SetPositionAndRotation(lookPosition, lookRotation);
	}
}
