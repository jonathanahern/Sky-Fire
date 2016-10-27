using UnityEngine;
using System.Collections;

public class ThrusterScript : MonoBehaviour {

    public KeyCode inputKey;

    public bool onOff;
    public float forceScalar;
    public int emitInverse;
    private ParticleSystem myPS;
    private Rigidbody myRB;
    private SpriteRenderer mySR;
    private int counter;

	// Use this for initialization
	void Start () {
        myPS = GetComponentInChildren<ParticleSystem>();
        myRB = transform.root.gameObject.GetComponent<Rigidbody>();
        mySR = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(inputKey))
        {
            onOff = !onOff;
        }

        if (onOff)
        {
            myRB.AddForceAtPosition(-transform.up * forceScalar, transform.position, ForceMode.Force);

            counter++;
            if (counter >= emitInverse)
            {
                myPS.Emit(1);
                counter = 0;
            }

            mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, Random.Range(.2f, 1f));
        }
        else
        {
            mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0);
        }

    }
}
