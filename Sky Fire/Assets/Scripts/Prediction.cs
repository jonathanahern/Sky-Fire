using UnityEngine;
using System.Collections;

public class Prediction : MonoBehaviour {

    public float minApplyCorrectionDist;
    public float maxApplyCorrectionDist;

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

    public Vector3 CompVel (Vector3 posCurrent, Vector3 posTrue, Vector3 velCurrent)
    {
        float currentVelContribution = 1.5f * Vector3.Magnitude(velCurrent);
        float deltaPosContribution = 0.25f * Vector3.Distance(posCurrent, posTrue);

        Vector3 correctionVel = Vector3.Normalize(posTrue - posCurrent) * ((1.5f * currentVelContribution) + (0.25f * deltaPosContribution));

        float correctionFactor = (Vector3.Distance(posCurrent, posTrue) - minApplyCorrectionDist) / (maxApplyCorrectionDist - minApplyCorrectionDist);

        return Vector3.Lerp(velCurrent, correctionVel, correctionFactor);
    }
}
