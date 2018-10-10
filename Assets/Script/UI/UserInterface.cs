using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {
    public Toggle recordeStep, pVsCom, comVsCom, speed_1x, speed_10x, speed_5x, level0, level1;
    public int speedUp;
    AI_DecisionMaking aiDecisionBlue, aiDecisionRed;
    public void MyAwake()
    {
        aiDecisionBlue = GameObject.Find("AI_Blue").GetComponent<AI_DecisionMaking>();
        aiDecisionRed = GameObject.Find("AI_Red").GetComponent<AI_DecisionMaking>();
        speedUp = 1;
    }
    public void IsPlayAnim()
    {

    }
    public void PsCom()
    {
        if (pVsCom.isOn)
        {
            comVsCom.isOn = false;
            aiDecisionBlue._StartAutoPlay();
        }
        else
        {
            //aiDecisionBlue.StopAutoPlay();
        }
    }
    public void ComVcCom()
    {
        if (comVsCom.isOn)
        {
            pVsCom.isOn = false;
        }
        else
        {

        }
    }
    public void Speed1x()
    {
        if (!speed_10x.isOn && !speed_5x.isOn)
        {
            speed_1x.isOn = true;
            return;
        }
        if (speed_1x.isOn)
        {
            speedUp = 1;
            speed_10x.isOn = false;
            speed_5x.isOn = false;
        }
    }
    public void Speed5x()
    {
        if (!speed_10x.isOn && !speed_1x.isOn)
        {
            speed_1x.isOn = true;
            return;
        }
        if (speed_5x.isOn)
        {
            speedUp = 5;
            speed_10x.isOn = false;
            speed_1x.isOn = false;
        }
    }
    public void Speed10x()
    {
        if (!speed_1x.isOn && !speed_5x.isOn)
        {
            speed_10x.isOn = true;
            return;
        }
        if (speed_10x.isOn)
        {
            speedUp = 10;
            speed_1x.isOn = false;
            speed_5x.isOn = false;
        }
    }
    public void Level0()
    {
        if (level0.isOn)
        {
            level1.isOn = false;
        }
        else
        {
            level1.isOn = true;
        }
    }
    public void Level1()
    {
        if (level1.isOn)
        {
            level0.isOn = false;
        }
        else
        {
            level0.isOn = true;
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
