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
    public RectTransform myNRGMeter;
    public Text myNRGText;

    private DragController myDC;
    private EnergyManager myEM;
    private NetworkPlayerModule myNPM;

	// Use this for initialization
	void Awake () {
        myRB = GetComponent<Rigidbody>();
        myDC = GetComponent<DragController>();
        myEM = GetComponent<EnergyManager>();
        myNPM = GetComponent<NetworkPlayerModule>();
        myMPMeter = transform.Find("Canvas").transform.Find("Main Propulsion Meter").GetComponent<RectTransform>();
        myNRGMeter = transform.Find("Canvas").transform.Find("Power Meter").GetComponent<RectTransform>();
        myNRGText = transform.Find("Canvas").transform.Find("Power Text").GetComponent<Text>();
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
    }

    void FixedUpdate ()
    {
        myRB.AddForce(transform.forward * (mEFactorApplied) * boostFactor);
        myEM.EnergyConsume(Mathf.Abs(mEFactorApplied) * maxEnergyConsumeRate * Time.fixedDeltaTime);
        //Debug.Log(mEFactorApplied * maxEnergyConsumeRate * Time.fixedDeltaTime);
    }

	public void EngineShutOff () {
		mainEngineFactor = 0.0f;
	}

    public void DisplayEnergy (float percent)
    {
        myNRGMeter.localScale = new Vector3(1, percent, 1);
        myNRGText.text = ((int)(percent * 100)).ToString() + "%";

        myNRGText.color = Color.Lerp(Color.red, Color.white, percent / .2f);
    }
}
