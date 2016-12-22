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
                if (Vector3.Distance(transform.position, value) >= 5)
                {
                    myRB.MovePosition(myPred.PredictPos(value, netVel, netAccel, netAngVel, netAngAccel, tDelta));
                }
                netPosBkgd = value;
            }
        }
    }

	bool hasRot = false;

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
				if(!hasRot){
					transform.rotation = value;
					hasRot = true;
				} else if (Vector3.Angle(transform.rotation.eulerAngles, value.eulerAngles) > 15) {
                    transform.rotation = Quaternion.Euler(myPred.PredictRot(value.eulerAngles, netAngVel, netAngAccel, tDelta));
                    Debug.Log("Reset Ang");
                }
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
	public GameObject stopShield;

	public GameObject shipChecker;
	public bool shipPresent = false;

	//1st set in PlayerSetup
	public Vector3 lastCheckpointPos;
	public Vector3 lastCheckpointRot;

    private Prediction myPred;
    private MEPartController myMEPC;

    void Awake()
    {
		offColor = new Color (1, 1, 1, 0.42f);
		onColor = new Color (0, 1, 1, 0.5f);

        myPred = GetComponent<Prediction>();
        myRB = GetComponent<Rigidbody>();
        myMEPC = GetComponentInChildren<MEPartController>();

        if (photonView.isMine)
        {

            gameObject.name = "Me";

            transform.Find("CameraPivot").Find("Main Camera").GetComponent<Camera>().enabled = true;

			GameObject lobbyCamera = GameObject.FindGameObjectWithTag ("LobbyCamera");
			Destroy (lobbyCamera);

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
    }

    void FixedUpdate()
    {
        if(!photonView.isMine)
        {
            myRB.velocity = myPred.CompVel(transform.position, netPos, netVel);
            myRB.angularVelocity = myPred.CompRotVel(transform.rotation, Quaternion.Euler(myPred.PredictRot(netRot.eulerAngles, netAngVel, netAngAccel, tDelta)), netAngVel) * Mathf.Deg2Rad;
        }

    }
		
	public void StopOnOff () {	

		if (stopper == false) {
			stopButton.color = onColor;
			stopLight = true;
			stopShield.SetActive (true);
		} else {
			stopButton.color = offColor;
			stopLight = false;
			stopShield.SetActive (false);
		}
        stopper = !stopper;
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
		//stopper = true;
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

    public void CallRPCManMEAnim(float factor)
    {
        photonView.RPC("AnimateMainEngines", PhotonTargets.All, factor);
    }

    [PunRPC]
    void AnimateMainEngines(float factor)
    {
        myMEPC.setAnimFactor(factor);
    }

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Checkpoint") {
			lastCheckpointPos = other.transform.position;
			lastCheckpointRot = other.transform.eulerAngles;
		}
	}
}
