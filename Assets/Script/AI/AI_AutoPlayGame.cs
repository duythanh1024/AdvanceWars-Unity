using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AutoPlayGame : MonoBehaviour {
    private Manager_ refManager;
    private AI_Training refAI_Traning;
    private LayerPhycis_ refLayerPhysic;
    private LayerArmy_ refLayerArmy;
    private int[] pos2d;
    void Awake()
    {
        pos2d = new int[2];
        refManager = GameObject.Find("Manager").GetComponent<Manager_>();
        refAI_Traning = GameObject.Find("AI").GetComponent<AI_Training>();
        refLayerPhysic = GameObject.Find("LayerPhysic").GetComponent<LayerPhycis_>();
        refLayerArmy = GameObject.Find("LayerArmy").GetComponent<LayerArmy_>();
    }
    public void SelectUnit()
    {
        pos2d = refLayerArmy.AI_GetUnitBlueMaxHp();
        refLayerPhysic.ClickOnMap(pos2d[0], pos2d[1]);
    }
    public void AutoMoveTo()
    {
        pos2d = refAI_Traning.GetOutNeuralNetRealTime();
        refLayerPhysic.OnMouseEnter(pos2d[0], pos2d[1]);
        refLayerPhysic.ClickOnMap(pos2d[0], pos2d[1]);
    }
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (refManager.onMove == Manager_.OnMove.Blue)
            {
                SelectUnit();
            }
        }
        if (Input.GetKeyDown("2"))
        {
            if (refManager.onMove == Manager_.OnMove.Blue)
            {
                AutoMoveTo();
            }
        }
    }
}
