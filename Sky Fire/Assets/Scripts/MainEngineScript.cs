using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainEngineScript : MonoBehaviour {

    public float mainEngineFactor;
    public float boostFactor;
    public float maxEnergyConsumeRate;

    private float mEFABkgd;
    private float mEFactorApplied

    {
        get
        {
            return mEFABkgd;
        }
        set
        {
            if (value != mEFABkgd)
            {
                myNPM.CallRPCManMEAnim(mEFactorApplied);
            }
            mEFABkgd = value;
        }
    }

    private Rigidbody myRB;

    public RectTransform myMPMeter;


    private DragController myDC;
    private EnergyManager myEM;
    private NetworkPlayerModule myNPM;
	public MEPartController myMEPC;

	private bool enginesOff = false;

	// Use this for initialization
	void Awake () {
        myRB = GetComponent<Rigidbody>();
        myDC = GetComponent<DragController>();
        myEM = GetComponent<EnergyManager>();
        myNPM = GetComponent<NetworkPlayerModule>();
        myMPMeter = transform.Find("Canvas").transform.Find("Main Propulsion Meter").GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update () {

//		if (enginesOff == true) {
//		
//			return;
//
//		}

        var delta = Input.GetAxis("Mouse ScrollWheel");

		if (enginesOff == false && delta > 0f || Input.GetKeyDown(KeyCode.UpArrow))
        {
            mainEngineFactor += .05f;
        }
		else if (enginesOff == false && delta < 0f || Input.GetKeyDown(KeyCode.DownArrow))
        {
            mainEngineFactor -= .05f;
        }

		if (enginesOff == false && mainEngineFactor >= 1.0f)
        {
            mainEngineFactor = 1.0f;
        }
		else if (enginesOff == false && mainEngineFactor <= -1.0f)
        {
            mainEngineFactor = -1.0f;
        }

        if (Mathf.Abs(mainEngineFactor) > .02f)
        {
            myDC.SetDrag(thrusters.ME, true);
        }
        else
        {
            myDC.SetDrag(thrusters.ME, false);
        }

        mEFactorApplied = Mathf.Lerp(mEFactorApplied, mainEngineFactor, .05f);
        myMPMeter.localScale = new Vector3(.5f, mainEngineFactor * .5f, 1);
//
//		Debug.Log ("me: " + mEFactorApplied);
//		Debug.Log ("main: " + mainEngineFactor);
    }

    void FixedUpdate ()
    {
        myRB.AddForce(transform.forward * (mEFactorApplied) * boostFactor);
        myEM.EnergyConsume(Mathf.Abs(mEFactorApplied) * maxEnergyConsumeRate * Time.fixedDeltaTime);
        //Debug.Log(mEFactorApplied * maxEnergyConsumeRate * Time.fixedDeltaTime);
    }

	public void EngineShutOff () {
		
		mainEngineFactor = 0.0f;
		myMEPC.EnginesOnOff();

		if (enginesOff == false) {
			enginesOff = true;
		} else {
			enginesOff = false;
		}
	}


}
