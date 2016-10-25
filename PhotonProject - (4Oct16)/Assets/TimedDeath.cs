using UnityEngine;
using System.Collections;

public class TimedDeath : MonoBehaviour {

    public float timer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            Destroy(this.gameObject);
        }
	}
}
