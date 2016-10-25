using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ForwardThrust : NetworkBehaviour {

    private Rigidbody rb;
    public float thrust;
    public float torque;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        rb.AddForce(transform.forward * thrust * .75f, ForceMode.Force);

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * thrust, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.forward * -thrust, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.up * -torque, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * torque, ForceMode.Force);
        }

    }
}
