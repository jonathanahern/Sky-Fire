using UnityEngine;
using System.Collections;

public class CameraPivotBehavior : MonoBehaviour {

    public float turnRate;
    //public float turnPos;

    private float yawAng;
    private float pitchAng;

    public float xDeltaFactor;
    public float yDeltaFactor;

    private float oldMouseX;
    private float oldMouseY;

    private float resetFlagTimer;
    public float resetTimeInterval;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    turnPos -= 45;
        //}
        //else if (Input.GetKeyDown(KeyCode.X))
        //{
        //    turnPos += 45;
        //}
        resetFlagTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            if (resetFlagTimer <= resetTimeInterval)
            {
                yawAng = 0;
                pitchAng = 0;
            }
            Vector2 mousePos = Input.mousePosition;
            oldMouseX = mousePos.x;
            oldMouseY = mousePos.y;
            resetFlagTimer = 0;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector2 mousePos = Input.mousePosition;
            if (mousePos.x != oldMouseX)
            {
                yawAng += (mousePos.x - oldMouseX) * xDeltaFactor;
            }
            if (mousePos.y != oldMouseY)
            {
                pitchAng += (mousePos.y - oldMouseY) * yDeltaFactor;
            }
            oldMouseX = mousePos.x;
            oldMouseY = mousePos.y;
        }

        if (Mathf.Abs(pitchAng) >= 60)
        {
            pitchAng = (pitchAng / Mathf.Abs(pitchAng)) * 60;
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(pitchAng, yawAng, 0), turnRate);

    }
}
