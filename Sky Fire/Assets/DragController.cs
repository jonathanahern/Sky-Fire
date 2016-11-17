using UnityEngine;
using System.Collections;

public class DragController : MonoBehaviour {

    public bool FT1;
    public bool FT2;
    public bool FT3;
    public bool FT4;
    public bool AT1;
    public bool AT2;
    public bool AT3;
    public bool AT4;

    public bool ME;

    public bool Stop;

    public float stopAngDrag;
    public float stopDrag;

    public float TAngDrag;

    public float MEDrag;

    private Rigidbody myRB;

    void Start ()
    {
        myRB = GetComponent<Rigidbody>();
    }

	void Update () {

        if (Stop)
        {

        }
	
	}
}
