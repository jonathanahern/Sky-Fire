using UnityEngine;
using System.Collections;

public class ShipCheckerScript : MonoBehaviour {

	NetworkPlayerModule playerScript;

	// Use this for initialization
	void Start () {
	
		playerScript = this.transform.parent.GetComponent<NetworkPlayerModule> ();
		if (transform.root.name == "Me")
			transform.parent = null;

	}


	void OnTriggerEnter(Collider other) {
		
		if (other.name == "NetworkPlayer") {
			Debug.Log ("Enter");
			playerScript.shipPresent = true;

		}
	}

	void OnTriggerExit(Collider other) {
		
		if (other.name == "NetworkPlayer") {
			Debug.Log ("Exit");
			playerScript.shipPresent = false;

		}
	}



}
