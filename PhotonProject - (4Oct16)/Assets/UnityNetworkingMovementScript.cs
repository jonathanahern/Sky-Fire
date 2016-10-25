using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UnityNetworkingMovementScript : NetworkBehaviour {

    public float myFloat;
    public double transitTime;
    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        float horizontalInput = 0.0F;
        if (stream.isWriting)
        {
            horizontalInput = transform.position.x;
            stream.Serialize(ref horizontalInput);
        }
        else
        {
            transitTime = Network.time - info.timestamp;
            stream.Serialize(ref horizontalInput);
            myFloat = horizontalInput;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
