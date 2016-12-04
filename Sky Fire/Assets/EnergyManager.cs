using UnityEngine;
using System.Collections;

public class EnergyManager : MonoBehaviour {

    public float energyPercent;
    public MainEngineScript myMES; 

    private void Start ()
    {
        myMES = GetComponent<MainEngineScript>();
    }

    private void Update ()
    {
        myMES.DisplayEnergy(energyPercent/100);
        EnergyStore(1 * Time.deltaTime);
    }

    public bool EnergyConsume (float consumed)
    {
        energyPercent -= consumed;
        if (energyPercent < 0)
        {
            energyPercent = 0;
            return false;
        }
        else
            return true;
    }

    public void EnergyStore (float store)
    {
        energyPercent += store;
        if (energyPercent > 100)
            energyPercent = 100;
    }
}
