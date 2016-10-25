using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UnityNetworkClient : NetworkBehaviour{

    [SyncVar]
    Vector3 position;

    float speed = 25;

    public override void OnStartLocalPlayer()
    {
        transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().followTgt = transform.Find("CameraTarget");
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().lookTgt = transform;
    }

    // Update is called once per frame
    void Update () {
       
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFireBullet();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            position = transform.position;
            Debug.Log(position);
        }
	
	}
    // Server calls RPCs
    // Command are called by clients
    [Command]
    void CmdFireBullet()
    {
        double currentNetTime = Network.time;
        Debug.Log(currentNetTime + " is current net time");
        Vector3 currentPos = transform.position + transform.forward * 4;
        Quaternion currentRot = transform.rotation;
        double currentTime = Network.time;
        RpcFireBullet(currentPos, currentRot, currentTime);
    }

    [ClientRpc]
    void RpcFireBullet (Vector3 where, Quaternion whereTo, double when)
    {

        Vector3 tempFwd = Vector3.Normalize(new Vector3(Mathf.Cos(whereTo.eulerAngles.y * Mathf.Deg2Rad), 0, Mathf.Sin(whereTo.eulerAngles.y * Mathf.Deg2Rad)));

        double deltaT = Network.time - when;
        Debug.Log("When is " + when + "  and the delta is " + deltaT);


        GameObject myBullet = (GameObject) Instantiate(Resources.Load("Bullet"), where /* + (tempFwd * deltaT * speed)*/, transform.rotation);
        //NetworkServer.Spawn(myBullet);
    }
}
