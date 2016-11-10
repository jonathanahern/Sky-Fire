using UnityEngine;
using System.Collections;

public class Prediction : MonoBehaviour {

    public Vector3 PredictPos (Vector3 posInput, Vector3 velInput, 
    Vector3 accelInput, Vector3 angVelInput, Vector3 angAccelInput, double time)
    {
        Vector3 velWAccel = velInput + accelInput;
        Vector3 accelWRot = Quaternion.Euler(Quaternion.Euler (0.5f * angAccelInput) * angVelInput) * accelInput;

        Vector3 prediction1Sec = posInput + velWAccel + accelWRot;

        Vector3 posOutput = Vector3.Lerp(posInput, prediction1Sec, (float)time);

        return posOutput;
    }

    public Vector3 PredictRot (Vector3 rotInput, Vector3 angVelInput, Vector3 angAccelInput, double time)
    {
        Vector3 prediction1Sec = Quaternion.Euler(0.5f * angAccelInput) * angVelInput + rotInput;

        Vector3 rotOutput = Vector3.Lerp(rotInput, prediction1Sec, (float)time);

        return rotOutput;
    }
}
