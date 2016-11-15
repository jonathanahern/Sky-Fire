using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutomaticScript : MonoBehaviour {

	public GameObject frontButtons;
	public GameObject backButtons;
	public GameObject autoButtons;

	public ThrusterManager thrusterManager;

	public Color offColor;
	public Color onColor;

	private Image buttonImage;

	// Use this for initialization
	void Start () {

		buttonImage = GetComponent<Image> ();

		offColor = new Color (1, 1, 1, 0.42f);
		onColor = new Color (0, 1, 1, 0.5f);
	
	}


	public void TurnOnOffAuto () {
		
		//Debug.Log (frontButtons.activeSelf);

		if (frontButtons.activeSelf == true) {
			thrusterManager.autoMode = true;
			frontButtons.SetActive (false);
			backButtons.SetActive (false);
			autoButtons.SetActive (true);
			buttonImage.color = onColor;
			thrusterManager.ThrusterReset ();
			thrusterManager.autoMode = true;
			return;
		
		}

		if (frontButtons.activeSelf == false) {

			frontButtons.SetActive (true);
			backButtons.SetActive (true);
			autoButtons.SetActive (false);
			buttonImage.color = offColor;
			thrusterManager.ThrusterReset ();
			thrusterManager.autoMode = false;
		}
	
	
	}

}
