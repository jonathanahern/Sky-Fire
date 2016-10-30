using UnityEngine;
using System.Collections;

public class RigidBodyBehavior : MonoBehaviour {

    Rigidbody myRB;
    public Vector3 myAngVel;
    public Vector3 myVel;
    public Vector3 myVelPublic;
    private Vector3 myVelOld;
    private Vector3 myAccelInst;
    private Vector3 myAccelSmoothPass1;
    public Vector3 myAccel;

    public float smoothRate;


    public GameObject pt0;
    public GameObject pt1;
    public GameObject pt2;
    public GameObject pt3;

	// Use this for initialization
	void Start () {

        myRB = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        myAngVel = new Vector3 (Mathf.Round(myRB.angularVelocity.x * Mathf.Rad2Deg * 100) / 100, Mathf.Round(myRB.angularVelocity.y * Mathf.Rad2Deg * 100) / 100, Mathf.Round(myRB.angularVelocity.z * Mathf.Rad2Deg * 100) / 100);
        myVelOld = myVel;
        myVel = myRB.velocity;

        myAccelInst = (myVel - myVelOld) / Time.fixedDeltaTime;

        myAccelSmoothPass1 = Vector3.Lerp(myAccelSmoothPass1, myAccelInst, .08f);

        myAccel = new Vector3(Mathf.Round(myAccelSmoothPass1.x * 100) / 100, Mathf.Round(myAccelSmoothPass1.y * 100) / 100, Mathf.Round(myAccelSmoothPass1.z * 100) / 100);
        myVelPublic = new Vector3(Mathf.Round(myVel.x * 100) / 100, Mathf.Round(myVel.y * 100) / 100, Mathf.Round(myVel.z * 100) / 100);


        pt0.transform.position = transform.position;
        pt0.transform.localRotation = Quaternion.Euler(myAngVel);

        pt1.transform.position = Vector3.Lerp(pt1.transform.position, pt0.transform.position + myVel + (.5f * myAccel), smoothRate);
        pt1.transform.localRotation = Quaternion.Euler(myAngVel);

        pt2.transform.position = Vector3.Lerp(pt2.transform.position, pt1.transform.position + myVel + (.5f * myAccel * 4), smoothRate);
        pt2.transform.localRotation = Quaternion.Euler(myAngVel);

        pt3.transform.position = Vector3.Lerp(pt3.transform.position, pt2.transform.position + myVel + (.5f * myAccel * 9), smoothRate);
        pt3.transform.localRotation = Quaternion.Euler(myAngVel);



    }
}
