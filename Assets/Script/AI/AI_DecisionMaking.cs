using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DecisionMaking : MonoBehaviour {
    public LayerArmy_.TeamType teamType;
    private Option.TypeOption typeOp;
    private Manager_ refManager;
    private LayerArmy_ refLayerArmy;
    private LayerPath_ refLayerPath;
    private List<ArmyForAI> listArmy;
    private Option refOption;
    private UserInterface refUserInterface;
    private ConnectPython refPython;
    LayerIM refLayerIM;
    AI_DataRow refDataRows;

    Position2D posFrom, posTo;
    CellArmy_ armySelected;
    [SerializeField]
    bool waitForAmim, waitForPython;
    static bool waitFeedbackPython;
    public static bool result;
    List<Position2D> listPosArea_forMove;
    public GameObject IMMapIU;
    public void MyAwake()
    {
        refManager = GameObject.Find("Manager").GetComponent<Manager_>();
        refLayerArmy = GameObject.Find("LayerArmy").GetComponent<LayerArmy_>();
        refLayerPath = GameObject.Find("LayerPath").GetComponent<LayerPath_>();
        refOption = GameObject.Find("Option").GetComponent<Option>();
        refUserInterface = GameObject.Find("CanvasMain").GetComponent<UserInterface>();
        refDataRows = GameObject.Find("AI_Traning").GetComponent<AI_DataRow>();
        refLayerIM = GameObject.Find("LayerIM").GetComponent<LayerIM>();
        refPython = GameObject.Find("Manager").GetComponent<ConnectPython>();
        IMMapIU = GameObject.Find("ToggInfluenceMap");
        //if (refUserInterface.recordeStep.isOn)
        //{
        //    refDataRows.CreateNew();
        //}

       
    }

    IEnumerator T()
    {
        yield  return  new WaitForSeconds(3);
        if (teamType == LayerArmy_.TeamType.Red)
        {
            StartCoroutine(Play());
        }
    }
    void Start()
    {
        if (teamType == LayerArmy_.TeamType.Red && refUserInterface.comVsCom.isOn)
        {
            StartCoroutine(Play());
        }
    }
  
    IEnumerator Play()
    {
        IMMapIU.gameObject.SetActive(false);
        while (true)
        {
            //if (!refUserInterface.isAIQuick.isOn)
            //    yield return new WaitForSeconds(delay);
            float delay = refUserInterface.speedUp == 1 ? .5f : (refUserInterface.speedUp == 5 ? .3f : .1f);
            _StartAutoPlay();
            if (!_SelectArmy())
            {
                //print("end day");
                listArmy.Clear();
                if (refUserInterface.speed_10x.isOn)
                    yield return new WaitForSeconds(delay);
                refOption.End();
                IMMapIU.gameObject.SetActive(true);
                yield break;
            }
            else
            {
                if (!refUserInterface.speed_10x.isOn)
                    yield return new WaitForSeconds(delay);
                if (refUserInterface.level0.isOn)
                {
                    _Move_AI_random();
                }
                else
                {
                    waitForPython = true;
                    StartCoroutine(Move_ConnecPython());
                }
                while (waitForPython)
                {
                    yield return null;
                }
                while (waitForAmim)
                {
                    yield return null;
                }               
                ChoiceOption();
                while (waitForAmim)
                {
                    yield return null;
                }
            }
            
        }
        IMMapIU.gameObject.SetActive(true);
    }
    public void StartAutoPlay()
    {
        StartCoroutine(Play());
    }
    public void FinishAmim()
    {
        waitForAmim = false;
    }
    void _Update()
    {
        //if (teamType == LayerArmy_.TeamType.Blue)
        //{
        //    if (Input.GetKeyDown("8"))
        //        StartCoroutine(Play());
        //}
        if (Input.GetKeyDown("s"))
        {
            refPython.CallMLP("0,-2,-2,-2,0,0,0,0,-26,0,0,-2,-32,-56,0,0,-2,-18,-56,0,0,0,0,0,0,10,170,84,1,1");
        }

        if (teamType == LayerArmy_.TeamType.Red)
        {
            if (Input.GetKeyDown("9"))
                StartCoroutine(Play());
            //if (Input.GetKeyDown("1"))
            //    _StartAutoPlay();
            //if (Input.GetKeyDown("2"))
            //    if (!_SelectArmy())
            //    {
            //        print("end day");

            //        listArmy.Clear();
            //    }
            //if (Input.GetKeyDown("3"))
            //    _Move();
            //if (Input.GetKeyDown("4"))
            //    StopAllCoroutines();
                
        }
        
    }
    //---
    public void _StartAutoPlay()
    {
        listArmy = refLayerArmy.GetListArmy(teamType);
        
        //for (int i = 0; i < listArmy.Count; i++)
        //{
        //    print(listArmy[i].army.name + " x: " + listArmy[i].army.transform.position.x + " y: " + listArmy[i].army.transform.position.y);
        //}
        //Select();
    }
    public bool _SelectArmy()
    {
        for (int i = 0; i < listArmy.Count; i++)
        {
            if (listArmy[i].army.isMoved)
                continue;
            else
            {

                refManager.Physic_OnMouseEnter(listArmy[i].x, listArmy[i].y);
                refManager.Physic_OnClick(listArmy[i].x, listArmy[i].y);
                posFrom = new Position2D(listArmy[i].x, listArmy[i].y);
                armySelected = listArmy[i].army;

                listPosArea_forMove = refLayerPath.GetListArea();

               
                return true;
            }
        }
        return false;
    }

    public static void Dec(bool ok)
    {
        waitFeedbackPython = false;
        result = ok;
        //print(result);
    }
    public IEnumerator Move_ConnecPython()
    {
        int r = -1;
        //connect
        for (int k = 0; k < listPosArea_forMove.Count; k++)
        {
            waitFeedbackPython = true;
            refPython.CallMLP(refLayerArmy.GetDataRowCurrent(listPosArea_forMove[k].x, listPosArea_forMove[k].y, Manager_.selectedX, Manager_.selectedY));
            while (waitFeedbackPython)
            {
                yield return null;
            }
            if (result)
            {
                r = k;
                break;
            }
        }
        //print("r = " + r);
        if (r == -1)//not move
        {
            refManager.Physic_OnMouseEnter(Manager_.selectedX, Manager_.selectedY);
            refManager.Physic_OnClick(Manager_.selectedX, Manager_.selectedY);
        }
        else
        {
            refManager.Physic_OnMouseEnter(listPosArea_forMove[r].x, listPosArea_forMove[r].y);
            refManager.Physic_OnClick(listPosArea_forMove[r].x, listPosArea_forMove[r].y);
            waitForAmim = true;
        }
        waitForPython = false;
        yield return null;
    }
    public void _Move_AI_MLP_Python()
    {
        waitForPython = true;
        //
        for (int k = 0; k < listPosArea_forMove.Count; k++)
        {
            refPython.CallMLP(refLayerArmy.GetDataRowCurrent(listPosArea_forMove[k].x, listPosArea_forMove[k].y, Manager_.selectedX, Manager_.selectedY));

        }
        waitForAmim = true;
//        


        //

       
    }
    

    public void _Move_AI_random()
    {
        bool ok = false;
        
        for (int i = 0; i < listPosArea_forMove.Count; i++)
        {
            waitForAmim = true;
            int r = Random.Range(0, listPosArea_forMove.Count);
            refManager.Physic_OnMouseEnter(listPosArea_forMove[r].x, listPosArea_forMove[r].y);
            refManager.Physic_OnClick(listPosArea_forMove[r].x, listPosArea_forMove[r].y);
            ok = true;
            
        }
        if (!ok)
        {
            refManager.Physic_OnMouseEnter(Manager_.selectedX, Manager_.selectedY);
            refManager.Physic_OnClick(Manager_.selectedX, Manager_.selectedY);
        }
    }
    
    public void SetChoiceOption(Option.TypeOption typeOp)
    {
        this.typeOp = typeOp;
    }
    public void ChoiceOption2()
    {
        if (typeOp == Option.TypeOption.fire)
        {
            //fire when hp >= hp enemy
            List<ArmyForAI> listFire = refLayerArmy.GetListCanFire();
            for (int i = 0; i < listFire.Count; i++)
            {
                if (Manager_.savedArmy.hp > listFire[i].army.hp)
                {
                    refOption.Fire();
                    refManager.Physic_OnMouseEnter(listFire[i].x, listFire[i].y);
                    refManager.Physic_OnClick(listFire[i].x, listFire[i].y);
                    waitForAmim = true;
                }
            }
        }
    }
    private void ChoiceOption()
    {
        int r = 0;
        switch (typeOp)
        {
            case Option.TypeOption.wait:
                Wait();
                break;
            case Option.TypeOption.fire:
                r = Random.Range(0, 2);
                if (r == 0)
                {
                     if(!Fire())
                        Wait();
                }                   
                else
                        Wait();
                break;
            case Option.TypeOption.capt:
                 r = Random.Range(0, 2);
                if (r == 0)
                    Capt();
                else
                    Wait();
                break;
            case Option.TypeOption.captAndFire:
                r = Random.Range(0, 3);
                if (r == 0)
                    Capt();
                else
                    if (r == 1)
                        Wait();
                    else
                        if (!Fire())
                            Wait();
                break;
        }
    }
    private void Capt()
    {
        refOption.Capt();
        waitForAmim = true;
    }
    private bool Fire()
    {
        //if (typeOp == Option.TypeOption.fire)
        //{
            //fire when hp >= hp enemy
            List<ArmyForAI> listEnemy = refLayerArmy.GetListCanFire();
            for (int i = 0; i < listEnemy.Count; i++)
            {
                if (Manager_.savedArmy.hp >= listEnemy[i].army.hp)
                {
                    refOption.Fire();
                    waitForAmim = true;
                    refManager.Physic_OnMouseEnter(listEnemy[i].x, listEnemy[i].y);
                    refManager.Physic_OnClick(listEnemy[i].x, listEnemy[i].y);
                    return true;

                }
            }
        //}
        return false;
    }
    private void Wait()
    {
        refOption.Wait();
    }
    private bool ShouldMoveToHere(int xHere, int yHere)
    {
        //return true;
        bool result = false;
        if (Random.Range(0, 5) == 0)
        {
            result = true;
        }
        return result;
    }
    private bool ShouldFireWith(int xEnemy, int yEnemy)
    {
        bool result = false;
        if (Random.Range(0, 2) == 0)
        {
            result = true;
        }
        return result;
    }
    private bool ShouldCaptureHere(int xHere, int yHere)
    {
        bool result = false;
        if (Random.Range(0, 2) == 0)
        {
            result = true;
        }
        return result;
    }


	
	
	
}
