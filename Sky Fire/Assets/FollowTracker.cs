using UnityEngine;
using System.Collections;

public class FollowTracker : MonoBehaviour {

    public int elementNumber;
    private Vector3[] theList;

    private float timer;

	// Use this for initialization
	void Start () {

        theList = transform.root.gameObject.GetComponent<RigidBodyBehavior>().deadReckoning;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        timer += Time.fixedDeltaTime;
        if (timer >=1)
        {
            timer -= 1;
            elementNumber--;
            if (elementNumber <= 0)
            {
                elementNumber = 10;
                transform.position = Vector3.Lerp(theList[elementNumber], theList[elementNumber - 1], timer);
            }
        }

        Vector3 myPos = Vector3.Lerp(theList[elementNumber], theList[elementNumber - 1], timer);
        transform.position = Vector3.Lerp(transform.position, myPos, .1f);
	}
}
