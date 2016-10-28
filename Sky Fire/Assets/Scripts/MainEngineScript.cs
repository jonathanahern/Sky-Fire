using UnityEngine;
using System.Collections;

public class MainEngineScript : MonoBehaviour {

    public float mainEngineFactor;
    public float boostFactor;

    private float mEFactorApplied;

    private Rigidbody myRB;
    public ParticleSystem myPS1;
    public ParticleSystem myPS2;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {

        var delta = Input.GetAxis("Mouse ScrollWheel");

        if (delta > 0f)
        {
            mainEngineFactor += .05f;
        }
        else if (delta < 0f)
        {
            mainEngineFactor -= .05f;
        }

        if (mainEngineFactor >= 1.0f)
        {
            mainEngineFactor = 1.0f;
        }
        else if (mainEngineFactor <= -1.0f)
        {
            mainEngineFactor = -1.0f;
        }

        mEFactorApplied = Mathf.Lerp(mEFactorApplied, mainEngineFactor, .05f);
        myRB.AddForce(transform.forward * mEFactorApplied * boostFactor);
        if (Vector3.Magnitude(myRB.velocity) > 10)
        {
            myRB.velocity = myRB.velocity.normalized * 10;
        }
	}

    void FixedUpdate ()
    {
        Debug.Log(Vector3.Magnitude(myRB.velocity));
        if (mEFactorApplied > 0)
            myPS1.Emit((int) (20 * mEFactorApplied));
        else if (mEFactorApplied < 0)
            myPS2.Emit((int)(20 * - mEFactorApplied));

    }
}
