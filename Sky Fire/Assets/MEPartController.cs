﻿using UnityEngine;
using System.Collections;

public class MEPartController : MonoBehaviour {

    private float mEFactorApplied;
    private ParticleSystem[] myPS = new ParticleSystem [10];

    private int partCounter;


	// Use this for initialization
	void Start () {
        for (int i = 0; i < myPS.Length; i++)
        {
            myPS[i] = transform.GetChild(i).GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (mEFactorApplied > 0)
        {
            myPS[0].Emit((int)(10 + 10 * mEFactorApplied));
            myPS[1].Emit((int)(5 + 15 * mEFactorApplied));
        }

        else if (mEFactorApplied < 0)
        {
            partCounter++;

            if (partCounter >= 2)
            {
                partCounter = 0;
                myPS[Random.Range(2,9)].Emit((int)(5 * -mEFactorApplied));
            }
        }
    }

    public void setAnimFactor(float factor)
    {
        mEFactorApplied = factor;
    }
}
