using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThrusterManager : MonoBehaviour {

	public ThrusterScript AftPortTop;
	public ThrusterScript AftStbdTop;
	public ThrusterScript AftPortBot;
	public ThrusterScript AftStbdBot;
	public ThrusterScript FwdStbdBot;
	public ThrusterScript FwdPortBot;
	public ThrusterScript FwdStbdTop;
	public ThrusterScript FwdPortTop;

	public Image noseUpButton;
	public Image noseDownButton;
	public Image clockwiseButton;
	public Image counterClockButton;
	public Image sideLeftButton;
	public Image sideRightButton;
	public Image upButton;
	public Image downButton;

	private Image lastButtonHit;

    private Rigidbody myRB;

	private Color offColor;
	private Color onColor;

	public bool autoMode = false;

    void Start()
    {
		offColor = new Color (1, 1, 1, 0.7f);
		onColor = new Color (0, 1, 1, 0.7f);

        myRB = transform.root.gameObject.GetComponent<Rigidbody>();

		noseUpButton.color = offColor;
		noseDownButton.color = offColor;
		clockwiseButton.color = offColor;
		counterClockButton.color = offColor;
		sideLeftButton.color = offColor;
		sideRightButton.color = offColor;
		upButton.color = offColor;
		downButton.color = offColor;

		lastButtonHit = noseUpButton;
    }

	public void AftTop () {
		if (AftStbdTop.onOff != AftPortTop.onOff) {
				AftPortTop.TurnOnFunction ();
				AftStbdTop.TurnOnFunction ();
		} else {
			AftPortTop.OnOffFunction ();
			AftStbdTop.OnOffFunction ();
		}
	}
	public void AftBot () {
		if (AftPortBot.onOff != AftStbdBot.onOff) {
			AftPortBot.TurnOnFunction ();
			AftStbdBot.TurnOnFunction ();
		} else {
			AftPortBot.OnOffFunction ();
			AftStbdBot.OnOffFunction ();
		}
	}
	public void AftPort () {
		if (AftPortTop.onOff != AftPortBot.onOff) {
			AftPortTop.TurnOnFunction ();
			AftPortBot.TurnOnFunction ();
		} else {
			AftPortTop.OnOffFunction ();
			AftPortBot.OnOffFunction ();
		}
	}
	public void AftStrb () {
		if (AftStbdTop.onOff != AftStbdBot.onOff) {
			AftStbdTop.TurnOnFunction ();
			AftStbdBot.TurnOnFunction ();
		} else {
			AftStbdTop.OnOffFunction ();
			AftStbdBot.OnOffFunction ();
		}
	}
	public void FwdTop () {
		if (FwdStbdTop.onOff != FwdPortTop.onOff) {
			FwdStbdTop.TurnOnFunction ();
			FwdPortTop.TurnOnFunction ();
		} else {
			FwdStbdTop.OnOffFunction ();
			FwdPortTop.OnOffFunction ();
		}
	}
	public void FwdBot () {
		if (FwdStbdBot.onOff != FwdPortBot.onOff) {
			FwdStbdBot.TurnOnFunction ();
			FwdPortBot.TurnOnFunction ();
		} else {
			FwdStbdBot.OnOffFunction ();
			FwdPortBot.OnOffFunction ();
		}
	}
	public void FwdPort () {
		if (FwdPortTop.onOff != FwdPortBot.onOff) {
			FwdPortTop.TurnOnFunction ();
			FwdPortBot.TurnOnFunction ();
		} else {
			FwdPortTop.OnOffFunction ();
			FwdPortBot.OnOffFunction ();
		}
	}
	public void FwdStrb () {
		if (FwdStbdTop.onOff != FwdStbdBot.onOff) {
			FwdStbdTop.TurnOnFunction ();
			FwdStbdBot.TurnOnFunction ();
		} else {
			FwdStbdTop.OnOffFunction ();
			FwdStbdBot.OnOffFunction ();
		}
	}

	public void NoseUp () {
		
		if (noseUpButton.color == offColor) {

			ThrusterReset ();
			FwdStbdBot.onOff = true;
			FwdPortBot.onOff = true;
			AftPortTop.onOff = true;
			AftStbdTop.onOff = true;
			noseUpButton.color = onColor;
			lastButtonHit = noseUpButton;
			return;
		} else {

			ThrusterReset ();

		}
	}

	public void NoseDown () {

		if (noseDownButton.color == offColor) {

			ThrusterReset ();
			AftStbdBot.onOff = true;
			AftPortBot.onOff = true;
			FwdPortTop.onOff = true;
			FwdStbdTop.onOff = true;
			noseDownButton.color = onColor;
			lastButtonHit = noseDownButton;
			return;
		} else {

			ThrusterReset ();

		}
	}

	public void Clockwise () {

		if (clockwiseButton.color == offColor) {

			ThrusterReset ();
			FwdPortTop.onOff = true;
			FwdPortBot.onOff = true;
			AftStbdBot.onOff = true;
			AftStbdTop.onOff = true;
			clockwiseButton.color = onColor;
			lastButtonHit = clockwiseButton;
			return;
		} else {

			ThrusterReset ();

		}
	}

	public void CounterClock () {

		if (counterClockButton.color == offColor) {

			ThrusterReset ();
			FwdStbdTop.onOff = true;
			FwdStbdBot.onOff = true;
			AftPortTop.onOff = true;
			AftPortBot.onOff = true;
			counterClockButton.color = onColor;
			lastButtonHit = counterClockButton;
			return;
		} else {

			ThrusterReset ();

		}
	}

	public void SideLeft () {

		if (sideLeftButton.color == offColor) {
			ThrusterReset ();
			FwdStbdBot.onOff = true;
			FwdStbdTop.onOff = true;
			AftStbdTop.onOff = true;
			AftStbdBot.onOff = true;
			sideLeftButton.color = onColor;
			lastButtonHit = sideLeftButton;
			return;
		} else {

			ThrusterReset ();

		}
	}

	public void SideRight () {

		if (sideRightButton.color == offColor) {
			ThrusterReset ();
			FwdPortTop.onOff = true;
			FwdPortBot.onOff = true;
			AftPortTop.onOff = true;
			AftPortBot.onOff = true;
			sideRightButton.color = onColor;
			lastButtonHit = sideRightButton;
			return;
		} else {

			ThrusterReset ();
		}
	}

	public void Up () {

		if (upButton.color == offColor) {
			ThrusterReset ();
			FwdStbdBot.onOff = true;
			FwdPortBot.onOff = true;
			AftPortBot.onOff = true;
			AftStbdBot.onOff = true;
			upButton.color = onColor;
			lastButtonHit = upButton;
			return;
		} else {

			ThrusterReset ();
		}
	}

	public void Down () {

		if (downButton.color == offColor) {
			ThrusterReset ();
			FwdStbdTop.onOff = true;
			FwdPortTop.onOff = true;
			AftPortTop.onOff = true;
			AftStbdTop.onOff = true;
			downButton.color = onColor;
			lastButtonHit = downButton;
			return;
		} else {

			ThrusterReset ();

		}
	}
		
	public void ThrusterReset () {


			AftPortTop.TurnOffColor();
			AftStbdTop.TurnOffColor();
			AftPortBot.TurnOffColor();
			AftStbdBot.TurnOffColor();
			FwdStbdBot.TurnOffColor();
			FwdPortBot.TurnOffColor();
			FwdStbdTop.TurnOffColor();
			FwdPortTop.TurnOffColor();
		

		
		lastButtonHit.color = offColor;
		AftPortTop.onOff = false;
		AftStbdTop.onOff = false;
		AftPortBot.onOff = false;
		AftStbdBot.onOff = false;
		FwdStbdBot.onOff = false;
		FwdPortBot.onOff = false;
		FwdStbdTop.onOff = false;
		FwdPortTop.onOff = false;
	
	}

	public void TurnOffDoubles () {
	
		AftPortTop.TurnOffYourDouble();
		AftStbdTop.TurnOffYourDouble();
		AftPortBot.TurnOffYourDouble();
		AftStbdBot.TurnOffYourDouble();
		FwdStbdBot.TurnOffYourDouble();
		FwdPortBot.TurnOffYourDouble();
		FwdStbdTop.TurnOffYourDouble();
		FwdPortTop.TurnOffYourDouble();
	
	}

		

    void Update ()
    {
        if (AftPortTop.onOff || AftStbdTop.onOff || AftPortBot.onOff || AftStbdBot.onOff || FwdStbdBot.onOff || FwdPortBot.onOff || FwdStbdTop.onOff || FwdPortTop.onOff)
        {
            myRB.angularDrag = .1f;
        }
        else
        {
            myRB.angularDrag = 0;
        }
    }

}
