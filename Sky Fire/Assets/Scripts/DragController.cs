using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class DragController : MonoBehaviour {

    private Dictionary<thrusters, ThrustBool> thrusterToBool = new Dictionary<thrusters, ThrustBool>();

    private class ThrustBool
    {
        public string myName;
        public bool myOnOff;

        public ThrustBool (string theName, bool theOnOffVal)
        {
            myName = theName;
            myOnOff = theOnOffVal;
        }
    }

    public float stopAngDrag;
    public float stopDrag;
    public float opAngDrag;
    public float opDrag;

    private Rigidbody myRB;

    ThrustBool FT1 = new ThrustBool("FT1", false);
    ThrustBool FT2 = new ThrustBool("FT2", false);
    ThrustBool FT3 = new ThrustBool("FT3", false);
    ThrustBool FT4 = new ThrustBool("FT4", false);
    ThrustBool AT1 = new ThrustBool("AT1", false);
    ThrustBool AT2 = new ThrustBool("AT2", false);
    ThrustBool AT3 = new ThrustBool("AT3", false);
    ThrustBool AT4 = new ThrustBool("AT4", false);
    ThrustBool ME = new ThrustBool("ME", false);
    ThrustBool Stop = new ThrustBool("Stop", false);

    void Start ()
    {
        myRB = GetComponent<Rigidbody>();

        thrusterToBool.Add(thrusters.FT1, FT1);
        thrusterToBool.Add(thrusters.FT2, FT2);
        thrusterToBool.Add(thrusters.FT3, FT3);
        thrusterToBool.Add(thrusters.FT4, FT4);

        thrusterToBool.Add(thrusters.AT1, AT1);
        thrusterToBool.Add(thrusters.AT2, AT2);
        thrusterToBool.Add(thrusters.AT3, AT3);
        thrusterToBool.Add(thrusters.AT4, AT4);

        thrusterToBool.Add(thrusters.ME, ME);

        thrusterToBool.Add(thrusters.Stop, Stop);
    }

    private void FixedUpdate ()
    {
        if(Stop.myOnOff)
        {
            myRB.angularDrag = stopAngDrag;
            myRB.drag = stopDrag;

        }
        else if (FT1.myOnOff || FT2.myOnOff || FT3.myOnOff || FT4.myOnOff || AT1.myOnOff || AT2.myOnOff || AT3.myOnOff || AT4.myOnOff || ME.myOnOff)
        {
            if (FT1.myOnOff || FT2.myOnOff || FT3.myOnOff || FT4.myOnOff || AT1.myOnOff || AT2.myOnOff || AT3.myOnOff || AT4.myOnOff)
                myRB.angularDrag = opAngDrag;
            if (ME.myOnOff)
                myRB.drag = Mathf.Lerp(0, opDrag, (Vector3.Magnitude(myRB.velocity) - 5) / 5); ;
        }
        else
        {
            myRB.angularDrag = 0;
            myRB.drag = 0;
        }
    }

    public void SetDrag (thrusters myThruster, bool status)
    {
        thrusterToBool[myThruster].myOnOff = status;

        //Debug.Log(thrusterToBool[myThruster].myName + " is set to " + thrusterToBool[myThruster].myOnOff);
    }

}
public enum thrusters { FT1, FT2, FT3, FT4, AT1, AT2, AT3, AT4, ME, Stop }
