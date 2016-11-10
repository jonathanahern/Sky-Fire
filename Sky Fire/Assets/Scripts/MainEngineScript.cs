using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainEngineScript : MonoBehaviour {

    public float mainEngineFactor;
    public float boostFactor;

    private float mEFactorApplied;

    private Rigidbody myRB;
    public ParticleSystem myPS1;
    public ParticleSystem myPS2;

    public RectTransform myMPMeter;

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        myMPMeter = GameObject.Find("Canvas").transform.Find("Main Propulsion Meter").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {

        var delta = Input.GetAxis("Mouse ScrollWheel");

        if (delta > 0f || Input.GetKeyDown(KeyCode.UpArrow))
        {
            mainEngineFactor += .05f;
        }
        else if (delta < 0f || Input.GetKeyDown(KeyCode.DownArrow))
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
        myRB.AddForce(transform.forward * (mEFactorApplied) * boostFactor);
        myRB.drag = Mathf.Lerp(0, .2f, (Vector3.Magnitude(myRB.velocity) - 5)  / 5);

        myMPMeter.localScale = new Vector3(.5f, mainEngineFactor * .5f, 1);
    }

    void FixedUpdate ()
    {
        //Debug.Log(Vector3.Magnitude(myRB.velocity));
        if (mEFactorApplied > 0)
            myPS1.Emit((int) (20 * mEFactorApplied));
        else if (mEFactorApplied < 0)
            myPS2.Emit((int)(20 * - mEFactorApplied));

    }
}
