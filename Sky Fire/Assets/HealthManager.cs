using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    private float _health;
    private float health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value != _health)
            {
                _health = health;
                healthText.text = "H: " + (int)_health;
                if ((int)health == 0)
                {
                    Death();
                }
            }
        }
    }

    private Text healthText;

    void Awake()
    {
        healthText = transform.Find("Canvas").transform.Find("Health Text").GetComponent<Text>();


    }
    // Use this for initialization
    void Start () {
        health = 100;
	}
	
    public void ModifyHealth(float amount)
    {
        health += amount;
    }

    private void Death()
    {

    }
}
