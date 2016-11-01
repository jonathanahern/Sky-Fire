using UnityEngine;
using System.Collections;

public class RigidBodyBehavior : MonoBehaviour {

    Rigidbody myRB;

    public static int secondsOfPrediction;

    private Vector3 myAngVel;
    public Vector3 myAngVelPublic;
    private Vector3 myAngVelOld;
    private Vector3 myTorqueInst;
    private Vector3 myTorqueSmoothPass1;
    public Vector3 myTorquePublic;

   


    private Vector3 myVel;
    public Vector3 myVelPublic;
    private Vector3 myVelOld;
    private Vector3 myAccelInst;
    private Vector3 myAccelSmoothPass1;
    public Vector3 myAccelPublic;

    public float smoothRate;

    public Vector3[] myPredictions = new Vector3[secondsOfPrediction + 1];
    //public GameObject pt0;
    //public GameObject pt1;
    //public GameObject pt2;
    //public GameObject pt3;

	// Use this for initialization
	void Start () {

        myRB = GetComponent<Rigidbody>();
        for (int i = 0; i < myPredictions.Length; i++)
        {
            myPredictions[i] = Vector3.zero;
        }

	}
	
	// Update is called once per frame
    void Update()
    {
        myTorqueSmoothPass1 = Vector3.Lerp(myTorqueSmoothPass1, myTorqueInst, smoothRate);
        myAccelSmoothPass1 = Vector3.Lerp(myAccelSmoothPass1, myAccelInst, smoothRate);
    }

    void FixedUpdate () {

        myAngVelOld = myAngVel;
        myAngVel = myRB.angularVelocity;

        myTorqueInst = (myAngVel - myAngVelOld) / Time.fixedDeltaTime;

        myTorquePublic = new Vector3(Mathf.Round(myTorqueSmoothPass1.x * Mathf.Rad2Deg * 100) / 100, Mathf.Round(myTorqueSmoothPass1.y * Mathf.Rad2Deg * 100) / 100, Mathf.Round(myTorqueSmoothPass1.z * Mathf.Rad2Deg * 100) / 100);
        myAngVelPublic = new Vector3(Mathf.Round(myAngVel.x * Mathf.Rad2Deg * 100) / 100, Mathf.Round(myAngVel.y * Mathf.Rad2Deg * 100) / 100, Mathf.Round(myAngVel.z * Mathf.Rad2Deg * 100) / 100);


        myVelOld = myVel;
        myVel = myRB.velocity;

        myAccelInst = (myVel - myVelOld) / Time.fixedDeltaTime;

        myAccelPublic = new Vector3(Mathf.Round(myAccelSmoothPass1.x * 100) / 100, Mathf.Round(myAccelSmoothPass1.y * 100) / 100, Mathf.Round(myAccelSmoothPass1.z * 100) / 100);
        myVelPublic = new Vector3(Mathf.Round(myVel.x * 100) / 100, Mathf.Round(myVel.y * 100) / 100, Mathf.Round(myVel.z * 100) / 100);


        for (int i = 0; i < myPredictions.Length; i++)
        {
            Quaternion myAngVelApplied = Quaternion.Euler(myAngVel * i);
            Quaternion myAngAccelApplied = Quaternion.Euler(myTorqueSmoothPass1 * i * i * 0.5f);

            myPredictions[i] = transform.position + (myAngVelApplied * (myVel * i)) + (myAngAccelApplied * (myAccelSmoothPass1 * i * i * 0.5f));
        }

    }
}
