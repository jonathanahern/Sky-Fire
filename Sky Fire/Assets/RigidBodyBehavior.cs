using UnityEngine;
using System.Collections;

public class RigidBodyBehavior : MonoBehaviour {

    Rigidbody myRB;

    //static int secondsOfPrediction = 10;

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

    public Vector3[] deadReckoning = new Vector3[11];

    public float dRTimer;

	// Use this for initialization
	void Start () {

        myRB = GetComponent<Rigidbody>();
        for (int i = 0; i < deadReckoning.Length; i++)
        {
            deadReckoning[i] = Vector3.zero;
        }

	}
	
	// Update is called once per frame
    void Update()
    {
        myTorqueSmoothPass1 = Vector3.Lerp(myTorqueSmoothPass1, myTorqueInst, smoothRate);
        myAccelSmoothPass1 = Vector3.Lerp(myAccelSmoothPass1, myAccelInst, smoothRate);
    }

    void FixedUpdate () {

        dRTimer += Time.fixedDeltaTime;
        dRTimer %= 1;

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


        for (int i = 0; i < deadReckoning.Length; i++)
        {
            float timeMod = (float)i - dRTimer;
            Quaternion myAngVelApplied = Quaternion.Euler(myAngVel * timeMod);
            Quaternion myAngAccelApplied = Quaternion.Euler(myTorqueSmoothPass1 * timeMod * timeMod * 0.5f);

            deadReckoning[i] = transform.position + (myAngVelApplied * (myVel * timeMod)) + (myAngAccelApplied * (myAccelSmoothPass1 * timeMod * timeMod * 0.5f));
        }

    }
}
