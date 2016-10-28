using UnityEngine;
using System.Collections;

public class ThrusterManager : MonoBehaviour {

	public ThrusterScript AftPortTop;
	public ThrusterScript AftStbdTop;
	public ThrusterScript AftPortBot;
	public ThrusterScript AftStbdBot;
	public ThrusterScript FwdStbdBot;
	public ThrusterScript FwdPortBot;
	public ThrusterScript FwdStbdTop;
	public ThrusterScript FwdPortTop;

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

}
