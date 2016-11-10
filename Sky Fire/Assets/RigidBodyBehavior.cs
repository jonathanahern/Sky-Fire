using UnityEngine;
using System.Collections;

public class RigidBodyBehavior : MonoBehaviour {

    Rigidbody myRB;

    //static int secondsOfPrediction = 10;

    public float kVal;
    public float dKVal;

    private Vector3 myAngVel;
    //public Vector3 myAngVelPublic;
    private Vector3 myAngVelOld;
    private Vector3 myAngVelOldOld;
    private Vector3 myAngAccelInst;
    public Vector3 myAngAccelPublic;

    public Vector3 myAvgAngAccel;

    private Vector3 myVel;
    //public Vector3 myVelPublic;
    private Vector3 myVelOld;
    private Vector3 myVelOldOld;
    private Vector3 myAccelInst;
    //public Vector3 myAccelPublic;

    public Vector3 myAvgAccel;

    public Vector3[] deadReckoning = new Vector3[11];
    public Vector3[] dRVel = new Vector3[11];
    public Vector3[] dRAccel = new Vector3[11];
    public Vector3[] dRAngVel = new Vector3[11];

    Quaternion myAngVelApplied;
    Quaternion myAngAccelApplied;
    Quaternion myAngNewt;

    private float dRTimer;

    public NetworkPlayerModule myNPM;

    // Use this for initialization
    void Start () {

        myNPM = GetComponent<NetworkPlayerModule>();

        myRB = GetComponent<Rigidbody>();
        for (int i = 0; i < deadReckoning.Length; i++)
        {
            deadReckoning[i] = Vector3.zero;
        }



	}

    void Update()
    {
        myAvgAccel = Vector3.Lerp(myAvgAccel, myAccelInst, .005f);
        myAvgAngAccel = Vector3.Lerp(myAvgAngAccel, myAngAccelInst, .005f);
    }
	
    void FixedUpdate()
    {
        dRTimer += Time.fixedDeltaTime;
        dRTimer %= 1;

        myAngVelOldOld = myAngVelOld;
        myAngVelOld = myAngVel;
        myAngVel = myRB.angularVelocity * Mathf.Rad2Deg;

        myAngAccelInst = (myAngVel - myAngVelOld) / Time.fixedDeltaTime;

        myAngAccelPublic = new Vector3(Mathf.Round(myAngAccelInst.x * 100) / 100, Mathf.Round(myAngAccelInst.y * 100) / 100, Mathf.Round(myAngAccelInst.z * 100) / 100);
        //myAngVelPublic = new Vector3(Mathf.Round(myAngVel.x * 100) / 100, Mathf.Round(myAngVel.y * 100) / 100, Mathf.Round(myAngVel.z * 100) / 100);

        myVelOldOld = myVelOld;
        myVelOld = myVel;
        myVel = myRB.velocity;

        myAccelInst = (myVel - myVelOld) / Time.fixedDeltaTime;
        //Debug.Log(myAccelInst);

        //myAccelPublic = new Vector3(Mathf.Round(myAccelInst.x * 100) / 100, Mathf.Round(myAccelInst.y * 100) / 100, Mathf.Round(myAccelInst.z * 100) / 100);
        //myVelPublic = new Vector3(Mathf.Round(myVel.x * 100) / 100, Mathf.Round(myVel.y * 100) / 100, Mathf.Round(myVel.z * 100) / 100);

        deadReckoning[0] = transform.position;
        dRVel[0] = myVel;
        dRAccel[0] = myAvgAccel;
        dRAngVel[0] = myAngVel;

        myAngAccelApplied = Quaternion.Euler(myAvgAngAccel);

        for (int i = 1; i < deadReckoning.Length; i++)
        {
            dRAngVel[i] = myAngAccelApplied * dRAngVel[i - 1];
            dRAccel[i] = myAngAccelApplied * (Quaternion.Euler(dRAngVel[i] * (kVal + (dKVal * i))) * dRAccel[i - 1]);

            dRVel[i] = dRVel[i - 1] + dRAccel[i];

            deadReckoning[i] = deadReckoning[i - 1] + dRVel[i] + (dRAccel[i] * .5f);
        }


        myNPM.ToSendVel = dRVel[0];
        myNPM.ToSendAccel = dRAccel[0];
        myNPM.ToSendAngVel = dRAngVel[0];
        myNPM.ToSendAngAccel = myAngAccelInst;
    }
}
