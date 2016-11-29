using UnityEngine;
using System.Collections;

public class UnChild : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if (transform.root.name == "Me")
            transform.SetParent(null);
        else
            Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
