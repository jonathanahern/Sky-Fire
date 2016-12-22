using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCannon : MonoBehaviour {

    public float cannonRad;
    private RaycastHit[] myRCH;

	// Use this for initialization
	void Start () {
		
	}
	
    void Fire ()
    {
        Ray myLOF = new Ray(transform.position, transform.forward);
        myRCH = Physics.SphereCastAll(myLOF, cannonRad);

        foreach (RaycastHit hit in myRCH)
        {
            if (hit.transform.gameObject.GetComponent<EnergyManager>() != null)
            {
                Debug.Log("Do something.");
            }
        }
    }
}
