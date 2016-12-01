using UnityEngine;
using System.Collections;

/// <summary>
/// Just a simple class to continually rotate an object
/// </summary>
public class Rotate : MonoBehaviour
{
	public float RotationSpeed;
	public Vector3 Axis;

	void Update()
	{
		transform.Rotate( Axis, RotationSpeed * Time.deltaTime );
	}
}