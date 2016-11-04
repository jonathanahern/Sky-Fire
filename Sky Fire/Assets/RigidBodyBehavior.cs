using UnityEngine;
using System.Collections;

public class RigidBodyBehavior : MonoBehaviour {

    Rigidbody myRB;

    //static int secondsOfPrediction = 10;

    private Vector3 myAngVel;
    //public Vector3 myAngVelPublic;
    private Vector3 myAngVelOld;
    private Vector3 myAngVelOldOld;
    private Vector3 myAngAccelInst;
    //public Vector3 myAngAccelPublic;

    private Vector3 myVel;
    //public Vector3 myVelPublic;
    private Vector3 myVelOld;
    private Vector3 myVelOldOld;
    private Vector3 myAccelInst;
    //public Vector3 myAccelPublic;

    private Vector3 myAccelOT;

    public float smoothRate;

    public Vector3[] deadReckoning = new Vector3[11];
    public Vector3[] dRVel = new Vector3[11];
    public Vector3[] dRAccel = new Vector3[11];

    Quaternion myAngVelApplied;
    Quaternion myAngAccelApplied;

    // Use this for initialization
    void Start () {

        myRB = GetComponent<Rigidbody>();
        for (int i = 0; i < deadReckoning.Length; i++)
        {
            deadReckoning[i] = Vector3.zero;
        }

	}
	
    void FixedUpdate()
    {
        myAngVelOldOld = myAngVelOld;
        myAngVelOld = myAngVel;
        myAngVel = myRB.angularVelocity * Mathf.Rad2Deg;

        myAngAccelInst = (myAngVel - myAngVelOldOld) / (2* (Time.fixedDeltaTime));

        //myAngAccelPublic = new Vector3(Mathf.Round(myAngAccelInst.x * 100) / 100, Mathf.Round(myAngAccelInst.y * 100) / 100, Mathf.Round(myAngAccelInst.z * 100) / 100);
        //myAngVelPublic = new Vector3(Mathf.Round(myAngVel.x * 100) / 100, Mathf.Round(myAngVel.y * 100) / 100, Mathf.Round(myAngVel.z * 100) / 100);

        myVelOldOld = myVelOld;
        myVelOld = myVel;
        myVel = myRB.velocity;

        myAccelInst = (myVel - myVelOldOld) / (2 * (Time.fixedDeltaTime));
        myAccelOT = Vector3.Lerp(myAccelOT, myAccelInst, .01f);
        Debug.Log(Vector3.Magnitude(myAccelOT));

        //myAccelPublic = new Vector3(Mathf.Round(myAccelInst.x * 100) / 100, Mathf.Round(myAccelInst.y * 100) / 100, Mathf.Round(myAccelInst.z * 100) / 100);
        //myVelPublic = new Vector3(Mathf.Round(myVel.x * 100) / 100, Mathf.Round(myVel.y * 100) / 100, Mathf.Round(myVel.z * 100) / 100);

        deadReckoning[0] = transform.position;
        dRVel[0] = myVel;
        dRAccel[0] = myAccelInst;

        myAngVelApplied = Quaternion.Euler(myAngVel);
        myAngAccelApplied = Quaternion.Euler(myAngAccelInst * 0.5f);

        Debug.Log(myAngVelApplied);

        for (int i = 1; i < deadReckoning.Length; i++)
        {
            dRVel[i] = dRVel[i - 1] + dRAccel[i - 1];
            dRAccel[i] = myAngAccelApplied * (myAngVelApplied * dRAccel[i - 1]);

            deadReckoning[i] = deadReckoning[i - 1] + myAccelInst;
        }
    }
}
