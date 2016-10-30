using UnityEngine;
using System.Collections;

public class RingCollisionBehavior : MonoBehaviour {

    public AudioSource alertSound;
    public ParticleSystem burst;

	// Use this for initialization
	void Start () {

        alertSound = GetComponent<AudioSource>();
        burst = transform.parent.gameObject.GetComponentInChildren<ParticleSystem>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            burst.Emit(100);
        }
	
	}

    void OnTriggerEnter ()
    {
        alertSound.Play();
        burst.Emit(100);
    }
}
