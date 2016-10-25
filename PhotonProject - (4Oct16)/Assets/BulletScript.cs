using UnityEngine;
using System.Collections;

public class BulletScript : Photon.MonoBehaviour {

    private Rigidbody rb;
    public float speed;

    public double timeStamp;
    private float timer;
    private double deltaNetworkTime;

	void Start () {
        rb = GetComponent<Rigidbody>();
        deltaNetworkTime = (PhotonNetwork.time - timeStamp);
	}
	
	// Update is called once per frame
	void Update () {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        timer += Time.deltaTime;
        //Debug.Log(deltaNetworkTime + "=" + PhotonNetwork.time + "-" + timeStamp + " tesing" + (PhotonNetwork.time - timeStamp));

        if (timer >= 5 - (float)deltaNetworkTime)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.name == "NetworkPlayer")
        {
            Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }
        else if (other.gameObject.name == "LocalPlayer")
        {
            Instantiate(Resources.Load("Explosion"), transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        }

        Destroy(this.gameObject);
    }
}
