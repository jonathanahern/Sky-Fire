using UnityEngine;
using System.Collections;

public class NetworkPlayerModule : Photon.MonoBehaviour
{

    public GameObject myCamera;

    private bool isAlive = true;
    public Vector3 netPos;
    public Quaternion netRot;
    public Vector3 netVel;
    public Vector3 netAccel;
    public Vector3 netAngVel;
    public Vector3 netAngAccel;
    public double recTime;
    public double tDelta;

    private float smoother = 15;

    public Vector3 ToSendVel;
    public Vector3 ToSendAccel;
    public Vector3 ToSendAngVel;
    public Vector3 ToSendAngAccel;

    public Rigidbody myRB;

	public float stopTimer;
	public bool stopper = false;

    void Awake()
    {
        myRB = GetComponent<Rigidbody>();

        if (photonView.isMine)
        {
            gameObject.name = "Me";

            transform.Find("CameraPivot").Find("Main Camera").GetComponent<Camera>().enabled = true;

            GetComponent<MainEngineScript>().enabled = true;
            GetComponent<RigidBodyBehavior>().enabled = true;
            transform.Find("PositionTrackers").gameObject.SetActive(true);
        }
        else
        {
            gameObject.name = "NetworkPlayer";

            Destroy(transform.Find("CameraPivot").Find("Main Camera").gameObject);

            GetComponent<MainEngineScript>().enabled = false;
            GetComponent<RigidBodyBehavior>().enabled = false;

            transform.Find("PositionTrackers").gameObject.SetActive(false);

            StartCoroutine("Alive");
        }
    }

    void Start()
    {

        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(myRB.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(ToSendVel);
            stream.SendNext(ToSendAccel);
            stream.SendNext(ToSendAngVel);
            stream.SendNext(ToSendAngAccel);
            stream.SendNext(PhotonNetwork.time);
        }
        else
        {
            netPos = (Vector3)stream.ReceiveNext();
            netRot = (Quaternion)stream.ReceiveNext();
            netVel = (Vector3)stream.ReceiveNext();
            netAccel = (Vector3)stream.ReceiveNext();
            netAngVel = (Vector3)stream.ReceiveNext();
            netAngAccel = (Vector3)stream.ReceiveNext();
            recTime = (double)stream.ReceiveNext();

            tDelta = PhotonNetwork.time - recTime;
        }
    }

    void Update ()
    {
		
        if (stopper == true)
        {
           // GetComponent<MainEngineScript>().enabled = false;
           // transform.Find("ThrusterBank").gameObject.SetActive(false);
			stopTimer += Time.deltaTime;
            GetComponent<Rigidbody>().drag = 1000;
            GetComponent<Rigidbody>().angularDrag = 1000;
			if (stopTimer > 2.0f) {
				
				stopper = false;
				stopTimer = 0.0f;
			
			}
        }
//        else
//        {
//            GetComponent<MainEngineScript>().enabled = true;
//            transform.Find("ThrusterBank").gameObject.SetActive(true);
//        }
    }

	public void StoptoTrue () {
	
		stopper = true;
	
	}


    // While alive - state machine
    IEnumerator Alive()
    {
        while (isAlive)
        {
            Vector3 newPos = GetComponent<Prediction>().PredictPos(netPos, netVel, netAccel, netAngVel, netAngAccel, tDelta);
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, newPos, Time.deltaTime * smoother));
            GetComponent<Rigidbody>().velocity = netVel;
            //Vector3 newRot = GetComponent<Prediction>().PredictRot(netRot, netAngVel, netAngAccel, tDelta);
            transform.rotation = Quaternion.Slerp(transform.rotation, netRot, Time.deltaTime * smoother);
            GetComponent<Rigidbody>().angularVelocity = netAngVel;



            yield return null;
        }
    }
}
