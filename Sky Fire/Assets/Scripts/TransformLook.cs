using UnityEngine;
using System.Collections;

public class TransformLook : MonoBehaviour {

    public Transform cam;

	// Use this for initialization
	void Start () {
        cam = transform.root.GetComponentInChildren<Camera>().transform;
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(cam);
	
	}
}
