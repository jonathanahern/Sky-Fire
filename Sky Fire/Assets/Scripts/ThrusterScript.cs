using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThrusterScript : MonoBehaviour {

    public KeyCode inputKey;
	public Image myButton;
	public Image doubleLeftButton;
	public Image doubleRightButton;
	public ThrusterScript thrusterLeft;
	public ThrusterScript thrusterRight;

    public bool onOff;
    public float forceScalar;
    public int emitInverse;
    private ParticleSystem myPS;
    private Rigidbody myRB;
    private SpriteRenderer mySR;
    private int counter;

	public Color offColor;
	public Color onColor;

	// Use this for initialization
	void Start () {
        myPS = GetComponentInChildren<ParticleSystem>();
        myRB = transform.root.gameObject.GetComponent<Rigidbody>();
        mySR = GetComponentInChildren<SpriteRenderer>();

		offColor = new Color (1, 1, 1, 0.42f);
		onColor = new Color (0, 1, 1, 0.5f);

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

	public void OnOffFunction() {
		if (!onOff) {
			onOff = true;
			myButton.color = onColor;
			NeighborCheck ();

		} else {
			onOff = false;
			myButton.color = offColor;
			NeighborCheck ();

		}
	}

	public void TurnOnFunction(){

		if (!onOff) {
			onOff = true;
			myButton.color = onColor;
			NeighborCheck ();
		}
	}

	public void TurnOffColor () {
	
		myButton.color = offColor;
	
	}


	void NeighborCheck (){
	
		if (onOff == thrusterLeft.onOff && onOff == true && doubleLeftButton.color != onColor) {
			
			doubleLeftButton.color = onColor;
		}

		if (onOff == thrusterRight.onOff && onOff == true && doubleRightButton.color != onColor) {

			doubleRightButton.color = onColor;
		}

		if (onOff == thrusterLeft.onOff && onOff == false && doubleLeftButton.color != offColor) {

			doubleLeftButton.color = offColor;
		}

		if (onOff == thrusterRight.onOff && onOff == false && doubleRightButton.color != offColor) {
			
			doubleRightButton.color = offColor;
		}

		if (onOff != thrusterRight.onOff && doubleRightButton.color != offColor) {
		
			doubleRightButton.color = offColor;		

		}

		if (onOff != thrusterLeft.onOff && doubleLeftButton.color != offColor) {

			doubleLeftButton.color = offColor;		

		}
	}

	public void TurnOffYourDouble () {
	
		doubleLeftButton.color = offColor;
	
	}
}
