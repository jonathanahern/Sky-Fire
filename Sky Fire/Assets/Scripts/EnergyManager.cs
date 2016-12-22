using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {

    public float energyPercent;
    public MainEngineScript myMES;

    public RectTransform myNRGMeter;
    public Text myNRGText;

    private void Awake ()
    {
        myNRGMeter = transform.Find("Canvas").transform.Find("Power Meter").GetComponent<RectTransform>();
        myNRGText = transform.Find("Canvas").transform.Find("Power Text").GetComponent<Text>();
    }

    private void Start ()
    {
        myMES = GetComponent<MainEngineScript>();
    }

    private void Update ()
    {
        DisplayEnergy(energyPercent/100);
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

    public void DisplayEnergy(float percent)

    {
        if (myNRGMeter != null)
        {
            myNRGMeter.localScale = new Vector3(1, percent, 1);
        }
        myNRGText.text = ((int)(percent * 100)).ToString() + "%";

        myNRGText.color = Color.Lerp(Color.red, Color.white, percent / .2f);
    }
}
