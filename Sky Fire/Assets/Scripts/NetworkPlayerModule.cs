using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NetworkPlayerModule : Photon.MonoBehaviour
{

    public Dictionary<thrusters, ThrusterParticleController> thrusterToComponent = new Dictionary<thrusters, ThrusterParticleController>();

    public GameObject myCamera;

    private bool isAlive = true;
    private Vector3 netPosBkgd;
    public Vector3 netPos
    {
        get
        {
            return netPosBkgd;
        }
        set
        {
            if (value != netPosBkgd)
            {
                myRB.MovePosition(myPred.PredictPos(value, netVel, netAccel, netAngVel, netAngAccel, tDelta));
                netPosBkgd = value;
            }
        }
    }
    private Quaternion netRotBkgd;
    public Quaternion netRot
    {
        get
        {
            return netRotBkgd;
        }
        set
        {
            if (value != netRotBkgd)
            {
                transform.rotation = Quaternion.Euler(myPred.PredictRot(value.eulerAngles, netAngVel, netAngAccel, tDelta));
                netRotBkgd = value;
            }
        }
    }
    public Vector3 netVel;
    public Vector3 netAccel;
    public Vector3 netAngVel;
    public Vector3 netAngAccel;
    public double recTime;
    public double tDelta;

    public float linearSmoother;
    public float angularSmoother;

    public Vector3 ToSendVel;
    public Vector3 ToSendAccel;
    public Vector3 ToSendAngVel;
    public Vector3 ToSendAngAccel;

    public Rigidbody myRB;

	public float stopTimer;
	public bool stopper = false;
	private Color offColor;
	private Color onColor;
	public Image stopButton;
	private bool stopLight = false;

	public GameObject shipChecker;
	public bool shipPresent = false;

	//1st set in PlayerSetup
	public Vector3 lastCheckpointPos;
	public Vector3 lastCheckpointRot;

    private Prediction myPred;

    void Awake()
    {
		offColor = new Color (1, 1, 1, 0.42f);
		onColor = new Color (0, 1, 1, 0.5f);

        myPred = GetComponent<Prediction>();
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

            //StartCoroutine("Alive");
        }
    }

    void Start()
    {
        thrusterToComponent.Add(thrusters.FT1, transform.Find("ThrusterBank").Find("Thruster-FwdStbdTop").GetComponent<ThrusterParticleController>());
        thrusterToComponent.Add(thrusters.FT2, transform.Find("ThrusterBank").Find("Thruster-FwdPortTop").GetComponent<ThrusterParticleController>());
        thrusterToComponent.Add(thrusters.FT3, transform.Find("ThrusterBank").Find("Thruster-FwdStbdBot").GetComponent<ThrusterParticleController>());
        thrusterToComponent.Add(thrusters.FT4, transform.Find("ThrusterBank").Find("Thruster-FwdPortBot").GetComponent<ThrusterParticleController>());

        thrusterToComponent.Add(thrusters.AT1, transform.Find("ThrusterBank").Find("Thruster-AftStbdTop").GetComponent<ThrusterParticleController>());
        thrusterToComponent.Add(thrusters.AT2, transform.Find("ThrusterBank").Find("Thruster-AftPortTop").GetComponent<ThrusterParticleController>());
        thrusterToComponent.Add(thrusters.AT3, transform.Find("ThrusterBank").Find("Thruster-AftStbdBot").GetComponent<ThrusterParticleController>());
        thrusterToComponent.Add(thrusters.AT4, transform.Find("ThrusterBank").Find("Thruster-AftPortBot").GetComponent<ThrusterParticleController>());
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
        GetComponent<DragController>().SetDrag(thrusters.Stop, stopper);

        //if (stopper == true)
        //{
        //    //        // GetComponent<MainEngineScript>().enabled = false;
        //    //        // transform.Find("ThrusterBank").gameObject.SetActive(false);
        //    //stopTimer += Time.deltaTime;
        //    //         GetComponent<Rigidbody>().drag = 1000;
        //    //         GetComponent<Rigidbody>().angularDrag = 1000;
        //    //if (stopTimer > 2.0f) {

        //    //	stopper = false;
        //    //	stopTimer = 0.0f;

        //    //}

        //}
        //        else
        //        {
        //            GetComponent<MainEngineScript>().enabled = true;
        //            transform.Find("ThrusterBank").gameObject.SetActive(true);
        //        }
    }

    void FixedUpdate()
    {
        if(!photonView.isMine)
        {
            //Vector3 newPos = GetComponent<Prediction>().PredictPos(netPos, netVel, netAccel, netAngVel, netAngAccel, tDelta);
            //GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, newPos, Time.deltaTime * linearSmoother));
            ////GetComponent<Rigidbody>().velocity = netVel;
            ////Vector3 newRot = GetComponent<Prediction>().PredictRot(netRot.eulerAngles, netAngVel, netAngAccel, tDelta);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, netRot, angularSmoother);
            //GetComponent<Rigidbody>().angularVelocity = netAngVel;

            myRB.velocity = netVel;
            myRB.angularVelocity = netAngVel * Mathf.Deg2Rad;

        }

    }
		
	public void StoptoTrue () {	
		stopper = !stopper;
		if (stopLight == false) {
			stopButton.color = onColor;
			stopLight = true;
		} else {
			stopButton.color = offColor;
			stopLight = false;
		}

	
	}


	public void ReturnToCheckPoint () {
		shipChecker.transform.localPosition = lastCheckpointPos;
		Invoke ("Warp", .15f);
	}

	void Warp() {

		if (shipPresent == true) {
			//transform.position = new Vector3 (lastCheckpointPos.x, lastCheckpointPos.y, lastCheckpointPos.z - 125.0f);
		} else {
			transform.position = lastCheckpointPos;
			transform.eulerAngles = lastCheckpointRot;
		}
		stopper = true;
		shipPresent = false;
	}

    public void CallRPCManThstAnim (thrusters myThruster, bool status)
    {
        photonView.RPC("AnimateManeuveringThrusters", PhotonTargets.All, myThruster, status);
    }

    [PunRPC]
    void AnimateManeuveringThrusters(thrusters myThruster, bool status)
    {
        thrusterToComponent[myThruster].onOff = status;
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Checkpoint") {
			lastCheckpointPos = other.transform.position;
			lastCheckpointRot = other.transform.eulerAngles;
		}
	}

    //// While alive - state machine
    //IEnumerator Alive()
    //{
    //    while (isAlive)
    //    {
    //        Vector3 newPos = GetComponent<Prediction>().PredictPos(netPos, netVel, netAccel, netAngVel, netAngAccel, tDelta);
    //        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, newPos, Time.deltaTime * smoother));
    //        GetComponent<Rigidbody>().velocity = netVel;
    //        //Vector3 newRot = GetComponent<Prediction>().PredictRot(netRot, netAngVel, netAngAccel, tDelta);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, netRot, Time.deltaTime * smoother);
    //        GetComponent<Rigidbody>().angularVelocity = netAngVel;



    //        yield return null;
    //    }
    //}
}
