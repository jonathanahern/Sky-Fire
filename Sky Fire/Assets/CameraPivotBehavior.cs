using UnityEngine;
using System.Collections;

public class CameraPivotBehavior : MonoBehaviour {

    public float turnRate;
    public float turnPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            turnPos -= 45;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            turnPos += 45;
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, turnPos, 0), turnRate);

    }
}
