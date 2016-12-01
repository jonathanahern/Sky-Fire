using UnityEngine;
using System.Collections;

public class ThrusterParticleController : MonoBehaviour {

    private ParticleSystem myPS;
    public bool onOff;

    private int counter;
    public int emitInverse;

	// Use this for initialization
	void Start () {
        myPS = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if (onOff)
        {
            counter++;
            if (counter >= emitInverse)
            {
                myPS.Emit(1);
                counter = 0;
            }
        }

    }
}
