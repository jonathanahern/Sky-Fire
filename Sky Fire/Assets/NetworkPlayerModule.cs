using UnityEngine;
using System.Collections;

public class NetworkPlayerModule : Photon.MonoBehaviour
{

    public GameObject myCamera;

    private bool isAlive = true;
    public Vector3 netPos;
    public Vector3 netRot;
    public Vector3 netVel;
    public Vector3 netAccel;
    public Vector3 netAngVel;
    public Vector3 netAngAccel;
    public double recTime;
    public double tDelta;

    private float smoother = 5;

    public Vector3 ToSendVel;
    public Vector3 ToSendAccel;
    public Vector3 ToSendAngVel;
    public Vector3 ToSendAngAccel;
    public double xmitTime;

    public Rigidbody myRB;

    void Awake()
    {
        myRB = GetComponent<Rigidbody>();
    }

    void Start()
    {

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

            transform.Find("CameraPivot").Find("Main Camera").GetComponent<Camera>().enabled = false;

            GetComponent<MainEngineScript>().enabled = false;
            GetComponent<RigidBodyBehavior>().enabled = false;

            transform.Find("PositionTrackers").gameObject.SetActive(false);

            StartCoroutine("Alive");
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(myRB.position);
            stream.SendNext(myRB.rotation);
            stream.SendNext(ToSendVel);
            stream.SendNext(ToSendAccel);
            stream.SendNext(ToSendAngVel);
            stream.SendNext(ToSendAngAccel);
            stream.SendNext(PhotonNetwork.time);
        }
        else
        {
            netPos = (Vector3)stream.ReceiveNext();
            netRot = (Vector3)stream.ReceiveNext();
            netVel = (Vector3)stream.ReceiveNext();
            netAccel = (Vector3)stream.ReceiveNext();
            netAngVel = (Vector3)stream.ReceiveNext();
            netAngAccel = (Vector3)stream.ReceiveNext();
            recTime = (float)stream.ReceiveNext();

            tDelta = PhotonNetwork.time - recTime;
        }
    }


    // While alive - state machine
    IEnumerator Alive()
    {
        while (isAlive)
        {
            Vector3 newPos = GetComponent<Prediction>().PredictPos(netPos, netVel, netAccel, netAngVel, netAngAccel, tDelta);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smoother);

            Vector3 newRot = GetComponent<Prediction>().PredictRot(netRot, netAngVel, netAngAccel, tDelta);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRot), Time.deltaTime * smoother);

            yield return null;
        }
    }
}
