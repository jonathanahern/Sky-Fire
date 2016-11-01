using UnityEngine;
using System.Collections;

public class FollowTracker : MonoBehaviour {

    public int elementNumber;
    private Vector3[] theList;

	// Use this for initialization
	void Start () {

        theList = transform.root.gameObject.GetComponent<RigidBodyBehavior>().myPredictions;

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = theList[elementNumber];
	
	}
}
