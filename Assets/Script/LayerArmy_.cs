using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Text;
public class ArmyForAI{
    public CellArmy_ army;
    public int x, y;
    public ArmyForAI(CellArmy_ army, int x, int y)
    {
        this.army = army;
        this.x = x;
        this.y = y;
    }
}

public class LayerArmy_ : MonoBehaviour {
    public enum TeamType
    {
        Red,
        Blue
    }
    public bool r_Moved, b_Moved;
    private LayerPath_ refLayerPath;
    private CellArmy_[,] layerArmy;
    //AI
    TeamType teamType;
    private List<ArmyForAI> listArmy;
    LayerIM refLayerIM;
    AI_Training refTraining;
    AI_DataRow refDataRows;
    List<ArmyForAI> listEnemyCanFire;
    //UI
    UserInterface refUserInterface;

    Manager_ manager;
    [SerializeField]
    SpriteRenderer sprFire;
    [SerializeField]
    Sprite[] fire;
   
    public void DestroyArmy()
    {
        layerArmy = new CellArmy_[15, 10];
        listArmy = new List<ArmyForAI>();
        r_Moved = false;
        b_Moved = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
    }
    public void MyAwake()
    {
        layerArmy = new CellArmy_[15, 10];
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        refLayerPath = GameObject.Find("LayerPath").GetComponent<LayerPath_>();
        refLayerIM = GameObject.Find("LayerIM").GetComponent<LayerIM>();
        refTraining = GameObject.Find("AI_Traning").GetComponent<AI_Training>();
        refDataRows = GameObject.Find("AI_Traning").GetComponent<AI_DataRow>();
        refUserInterface = GameObject.Find("CanvasMain").GetComponent<UserInterface>();
        listArmy = new List<ArmyForAI>();
        if (refUserInterface.recordeStep.isOn)
        {
            refDataRows.CreateNew();
        }
    }
    //void Update()
    //{
    //    //test
    //    //if (Input.GetKeyDown("b"))
    //    //{
    //    //    StartCoroutine(AnimFire(1,1));
            
    //    //}
    //}
    public List<ArmyForAI> GetListArmy(TeamType type)
    {
        ArmyForAI army;
        listArmy.Clear();
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (!IsNull(r, c))
                {
                    Manager_.TypeArmy t = layerArmy[r, c].type;
                    if (type == TeamType.Blue)
                    {
                        if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                        {
                            army = new ArmyForAI(layerArmy[r, c], r, c);
                            listArmy.Add(army);
                        }
                    }
                    else
                    {
                        if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                        {
                            army = new ArmyForAI(layerArmy[r, c], r, c);
                            listArmy.Add(army);
                        }
                    }
                }
            }
        }
        return listArmy;
    }
    public List<ArmyForAI> GetListCanFire()
    {
        return listEnemyCanFire;
    }
    public int[] AI_GetUnitBlueMaxHp()
    {
        int[] pos2dUnitBlueMaxHp = new int[2];
        int maxHp = -1;
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (!IsNull(r, c))
                {
                    if (layerArmy[r, c].isMoved)
                        continue;
                   Manager_.TypeArmy t = layerArmy[r, c].type;
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        if (layerArmy[r, c].hp > maxHp)
                        {
                            maxHp = layerArmy[r, c].hp;
                            pos2dUnitBlueMaxHp[0] = r;
                            pos2dUnitBlueMaxHp[1] = c;
                        }
                    }
                }
            }
        }
        return pos2dUnitBlueMaxHp;
    }
    public void AddValueToInluenceMap2(Manager_.TypeArmy currentType)
    {
        if (currentType == Manager_.TypeArmy.redTank || currentType == Manager_.TypeArmy.redMech || currentType == Manager_.TypeArmy.redInfantry)
        {
            AddValueIM_Red2();
            AddValueIM_Blue2(currentType);
        }
        else
        {
            AddValueIM_Red2(currentType);
            AddValueIM_Blue2();
        }
    }
    public void AddValueToInluenceMap(Manager_.TypeArmy currentType)
    {
        if (currentType == Manager_.TypeArmy.redTank || currentType == Manager_.TypeArmy.redMech || currentType == Manager_.TypeArmy.redInfantry)
        {
            AddValueIM_Red();
            AddValueIM_Blue2(currentType);
        }
        else
        {
            AddValueIM_Red2(currentType);
            AddValueIM_Blue();
        }
    }
    
    public void AddValueIM_Blue(Manager_.TypeArmy typeEnemy = Manager_.TypeArmy.none)
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (!IsNull(r, c))
                {
                    Manager_.TypeArmy t = layerArmy[r, c].type;
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        refLayerPath.FindArea(t, layerArmy[r, c].move, r, c);
                        for (int cc = 0; cc < 10; cc++)
                        {
                            for (int rr = 0; rr < 15; rr++)
                            {
                                if (refLayerPath.IsCheck(rr, cc))
                                {
                                    if (typeEnemy != Manager_.TypeArmy.none)
                                    {
                                        manager.refIM.SetBlue(manager.onMove, rr, cc, layerArmy[r, c].GetDame(typeEnemy) + layerArmy[r, c].hp);
                                    }
                                    else
                                    {
                                        manager.refIM.SetBlue(manager.onMove, rr, cc, (2 + layerArmy[r, c].hp));
                                    }
                                }
                            }
                        }
                        refLayerPath.ResetNull();
                    }
                }
            }
        }
    }

    //2
    public void AddValueIM_Blue2(Manager_.TypeArmy typeEnemy = Manager_.TypeArmy.none)
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (!IsNull(r, c))
                {
                    Manager_.TypeArmy t = layerArmy[r, c].type;
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        refLayerPath.FindArea(t, layerArmy[r, c].move, r, c);
                        for (int cc = 0; cc < 10; cc++)
                        {
                            for (int rr = 0; rr < 15; rr++)
                            {
                                if (refLayerPath.IsCheck(rr, cc))
                                {
                                    if (typeEnemy != Manager_.TypeArmy.none)
                                    {
                                        manager.refIM.SetBlue(manager.onMove, rr, cc, layerArmy[r, c].GetDame(typeEnemy) + layerArmy[r, c].hp);
                                    }
                                    else
                                    {
                                        manager.refIM.SetBlue(manager.onMove, rr, cc, (2 + layerArmy[r, c].hp));
                                    }
                                }
                            }
                        }
                        //refLayerPath.ResetNull();
                    }
                }
            }
        }
    }
    //2
    public void AddValueIM_Red2(Manager_.TypeArmy type = Manager_.TypeArmy.none)
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (!IsNull(r, c))
                {
                    Manager_.TypeArmy t = layerArmy[r, c].type;
                    if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                    {
                        refLayerPath.FindArea(t, layerArmy[r, c].move, r, c);
                        for (int cc = 0; cc < 10; cc++)
                        {
                            for (int rr = 0; rr < 15; rr++)
                            {
                                if (refLayerPath.IsCheck(rr, cc))
                                {
                                    if (type != Manager_.TypeArmy.none)
                                    {
                                        manager.refIM.SetRed(manager.onMove, rr, cc, layerArmy[r, c].GetDame(type) + layerArmy[r, c].hp);
                                    }
                                    else
                                    {
                                        manager.refIM.SetRed(manager.onMove, rr, cc, (2 + layerArmy[r, c].hp));
                                    }
                                }
                            }
                        }
                        //refLayerPath.ResetNull();
                    }
                }
            }
        }

    }

    public void AddValueIM_Red(Manager_.TypeArmy type = Manager_.TypeArmy.none)
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (!IsNull(r, c))
	            {
                    Manager_.TypeArmy t = layerArmy[r, c].type;
                    if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                    {
                        refLayerPath.FindArea(t, layerArmy[r, c].move, r, c);
                        for (int cc = 0; cc < 10; cc++)
                        {
                            for (int rr = 0; rr < 15; rr++)
                            {
                                if (refLayerPath.IsCheck(rr, cc))
                                {
                                    if (type != Manager_.TypeArmy.none)
                                    {
                                        manager.refIM.SetRed(manager.onMove, rr, cc, layerArmy[r, c].GetDame(type) + layerArmy[r, c].hp);
                                    }
                                    else
                                    {
                                        manager.refIM.SetRed(manager.onMove, rr, cc, (2 + layerArmy[r, c].hp));
                                    }
                                }
                            }
                        }
                        refLayerPath.ResetNull();
                    }
	            }
            }
        }
       
    }
    public void SetIM(Manager_.OnMove  oM)
    {
        if(oM == Manager_.OnMove.Red)
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (!IsNull(r, c))
                { 
                    
                }
            }
        }
    }
    private IEnumerator AnimFire(int x, int y, bool waitA2 = false){
        if (waitA2)
        {
           yield return new WaitForSeconds(.5f);
        }
        sprFire.transform.position = new Vector2(x, y);
        sprFire.gameObject.SetActive(true);
        sprFire.sprite = fire[0];
        int i = 1;
        float delay = refUserInterface.speedUp == 1 ? .1f : (refUserInterface.speedUp == 5 ? .05f : .01f);
        float t = Time.time + delay;
        while (true)
        {
            if (t < Time.time)
            {
                if (i > 2)
                {
                    sprFire.gameObject.SetActive(false);
                    manager.FeedBack();
                    yield break;
                }
                sprFire.sprite = fire[i];
                i++;
                t = Time.time + delay;
            }
            yield return null;
        }
    }
   
    /// <summary>
    /// return 0 -> not win; 
    /// return -1 -> red win; 
    /// return 1 -> blue win
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    public int IsWin(ref int numRed, ref int numBlue)
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (layerArmy[r, c] != null)
                    if (layerArmy[r, c].type == Manager_.TypeArmy.redInfantry || layerArmy[r, c].type == Manager_.TypeArmy.redMech || layerArmy[r, c].type == Manager_.TypeArmy.redTank)
                        numRed++;
                    else
                    {
                        if (layerArmy[r, c].type == Manager_.TypeArmy.blueInfantry || layerArmy[r, c].type == Manager_.TypeArmy.blueMech || layerArmy[r, c].type == Manager_.TypeArmy.blueTank)
                            numBlue++;
                    }
            }
        }
        if (numRed == 0)
            return 1;
        if (numBlue == 0)
            return -1;
        return 0;
    }
    public bool IsFire(int x, int y)
    {
        return (layerArmy[x, y] != null && layerArmy[x, y].checkFire);
    }
    public bool Select(Manager_.OnMove onMove)
    {
        bool success = false;
        if (IsNull(Manager_.selectedX, Manager_.selectedY))
            return false;
        Manager_.savedArmy = layerArmy[Manager_.selectedX, Manager_.selectedY];// print(layerArmy[Manager_.selectedX, Manager_.selectedY].name);
        if (Manager_.savedArmy != null && !Manager_.savedArmy.isMoved)
        {
            Manager_.TypeArmy t = Manager_.savedArmy.type;
            if (onMove == Manager_.OnMove.Red)
            {
                if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    return false;
            }
            else//onmove blue
                if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                    return false;
            //refLayerPath.DrawArea();
            success = true;
        }
        return success;
    }
    public void Reset()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (layerArmy[r, c])
                {
                    layerArmy[r, c].Reset();
                }
            }
        }
    }
    //public bool IsArmyLive(Manager_.OnMove oM , int x, int y)
    //{
    //    if (layerArmy[x, y] != null && !layerArmy[x, y].isMoved)
    //    {
    //        Manager_.TypeArmy t = layerArmy[x, y].type;
    //        if (oM == Manager_.OnMove.Blue)
    //        {
    //            if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
    //                return true;                    
    //        }
    //        else
    //        {
    //            if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
    //                return true;
    //        }
    //    }
    //    return false;
    //}
    public void New(CellArmy_ cell, int x, int y)
    {
        layerArmy[x, y] = cell;
        layerArmy[x, y].transform.position = new Vector2(x, y);
    }
    public bool IsNull(int x, int y)
    {
        return (layerArmy[x, y] == null);
    }
    public void AddHp(int x, int y, int value)
    {
        layerArmy[x, y].UpdateHp(layerArmy[x, y].hp + value, true);
    }
    public Manager_.TypeArmy GetType(int x, int y)
    {
        return layerArmy[x, y].type;
    }
    public int GetHp(int x, int y)
    {
        return layerArmy[x, y].hp;
    }
    public void ConfirmPosition()
    {
        if (Manager_.newX == Manager_.selectedX && Manager_.newY == Manager_.selectedY)
        {
            layerArmy[Manager_.newX, Manager_.newY].Moved();
        }
        else
        {
            layerArmy[Manager_.newX, Manager_.newY] = Manager_.savedArmy;
            layerArmy[Manager_.newX, Manager_.newY].Moved();
            layerArmy[Manager_.selectedX, Manager_.selectedY] = null;
        }
    }
    public void Capt()
    {
        float speed = refUserInterface.speedUp == 1 ? 1 : (refUserInterface.speedUp == 5 ? 3 : 10);
        layerArmy[Manager_.newX, Manager_.newY].Capt(speed);
        StartCoroutine(WaitCapt());
    }
    private IEnumerator WaitCapt()
    {
        float time = Manager_.savedArmy.transform.GetChild(0).GetComponent<Animation>().GetClip("Capt").length / Manager_.savedArmy.transform.GetChild(0).GetComponent<Animation>()["Capt"].speed;
        yield return new WaitForSeconds(time);
        manager.FeedBack();
    }
    public void Wait()
    {
        manager.FeedBack();
    }
    public bool Fire(int xEnemy, int yEnemy, int defEnemy, int defCurrent)
    {
        bool isDie = false;
        bool wait = false;
        layerArmy[xEnemy, yEnemy].checkFire = false;
        Manager_.savedArmy.checkFire = false;
        if (!layerArmy[xEnemy, yEnemy].UpdateHp(UnityEngine.Random.Range(0, 2) + layerArmy[xEnemy, yEnemy].hp - (Manager_.savedArmy.GetDame(layerArmy[xEnemy, yEnemy].type) - defEnemy)))
        {
            StartCoroutine(AnimFire(xEnemy, yEnemy));
            isDie = true;
            wait = true;
        }
        if (!wait)
        {
            if (!Manager_.savedArmy.UpdateHp(UnityEngine.Random.Range(0, 2) + Manager_.savedArmy.hp - (layerArmy[xEnemy, yEnemy].GetDame(Manager_.savedArmy.type) - defCurrent)))
            {
                StartCoroutine(AnimFire(Manager_.selectedX, Manager_.selectedY));
                isDie = true;
            }
        }
        else
        {
            if (!Manager_.savedArmy.UpdateHp(UnityEngine.Random.Range(0, 2) + Manager_.savedArmy.hp - (layerArmy[xEnemy, yEnemy].GetDame(Manager_.savedArmy.type) - defCurrent)))
            {
                StartCoroutine(AnimFire(Manager_.selectedX, Manager_.selectedY, true));
                isDie = true;
            }
        }
        return isDie;
    }
    public void Cancel()
    {
        Manager_.savedArmy.transform.position = new Vector2(Manager_.selectedX, Manager_.selectedY);
    }
    public void AnimMove(bool isAnim = true)
    {
        if (refLayerPath.listArmyMove.Count == 1)//not move
        {
            //manager.FeedBack();
            MoveWithoutAnim();// print("@");

            return;
        }
        if (refLayerPath.listArmyMove.Count != 0)//move
        {
            if (isAnim)
            {
                StartCoroutine(MoveWithAnim());
            }
            else
            {

                MoveWithoutAnim();
                //return false;
            }
        }
    }
    public string GetDataRowCurrent(int xCenter, int yCenter, int xStat, int yStart)
    {
        manager.SetInluenceMapForWriteFile2();
        StringBuilder builder = new StringBuilder();
        float[] influenceMap = refLayerIM.GetIM5x5(xCenter, yCenter);
        for (int i = 0; i < 25; i++)
        {
            builder.Append(influenceMap[i]);
            builder.Append(",");
        }
        builder.Append(GetHp(Manager_.selectedX, Manager_.selectedY));
        builder.Append(",");
        builder.Append(15 * xCenter + yCenter);//end
        builder.Append(",");
        builder.Append(15 * xStat + yStart);//start
        builder.Append(",");
        switch (GetType(Manager_.selectedX, Manager_.selectedY))
        {
            case Manager_.TypeArmy.redTank:
            case Manager_.TypeArmy.blueTank:
                builder.Append(0);
                break;
            case Manager_.TypeArmy.redInfantry:
            case Manager_.TypeArmy.blueInfantry:
                builder.Append(1);
                break;
            case Manager_.TypeArmy.redMech:
            case Manager_.TypeArmy.blueMech:
                builder.Append(2);
                break;
        }
        //print(builder.ToString());
        return builder.ToString();

    }
    private void MoveWithoutAnim()
    {
        
        //AI:
        if (refUserInterface.recordeStep.isOn)
        {
            manager.SetInluenceMapForWriteFile();
            List<Position2D> listPosArea = refLayerPath.GetListArea(GetType(Manager_.selectedX, Manager_.selectedY), Manager_.savedArmy.move, Manager_.selectedX, Manager_.selectedY);
            int t_newX = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].x;
            int t_newY = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].y;
            if (manager.onMove == Manager_.OnMove.Red)
            {
                for (int i = 0; i < listPosArea.Count; i++)
                {
                    float[] t = refLayerIM.GetIM5x5(listPosArea[i].x, listPosArea[i].y);
                    if (listPosArea[i].x == t_newX && listPosArea[i].y == t_newY)
                        refDataRows.AddDataRow(true, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY +t_newX, 15 * Manager_.selectedX + Manager_.selectedY, true);
                    else
                        refDataRows.AddDataRow(true, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY + t_newX, 15 * Manager_.selectedX + Manager_.selectedY, false);
                }
            }
            else
            {
                for (int i = 0; i < listPosArea.Count; i++)
                {
                    float[] t = refLayerIM.GetIM5x5(listPosArea[i].x, listPosArea[i].y);
                    if (listPosArea[i].x == t_newX && listPosArea[i].y == t_newY)
                        refDataRows.AddDataRow(false, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY + t_newX, 15 * Manager_.selectedX + Manager_.selectedY, true);
                    else
                        refDataRows.AddDataRow(false, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY + t_newX, 15 * Manager_.selectedX + Manager_.selectedY, false);
                }
            } 
        }
        //move
        Manager_.savedArmy.transform.position = refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1];//move now
        Manager_.newX = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].x;
        Manager_.newY = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].y;
        manager.FeedBack();
    }
    IEnumerator MoveWithAnim()
    {

        //AI:
        if (refUserInterface.recordeStep.isOn)
        {
            manager.SetInluenceMapForWriteFile();
            List<Position2D> listPosArea = refLayerPath.GetListArea(GetType(Manager_.selectedX, Manager_.selectedY), Manager_.savedArmy.move, Manager_.selectedX, Manager_.selectedY);
            int t_newX = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].x;
            int t_newY = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].y;
            if (manager.onMove == Manager_.OnMove.Red)
            {
                for (int i = 0; i < listPosArea.Count; i++)
                {
                    float[] t = refLayerIM.GetIM5x5(listPosArea[i].x, listPosArea[i].y);
                    if (listPosArea[i].x == t_newX && listPosArea[i].y == t_newY)
                        refDataRows.AddDataRow(true, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY + t_newX, 15 * Manager_.selectedX + Manager_.selectedY, true);
                    else
                        refDataRows.AddDataRow(true, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY + t_newX, 15 * Manager_.selectedX + Manager_.selectedY, false);
                }
            }
            else
            {
                for (int i = 0; i < listPosArea.Count; i++)
                {
                    float[] t = refLayerIM.GetIM5x5(listPosArea[i].x, listPosArea[i].y);
                    if (listPosArea[i].x == t_newX && listPosArea[i].y == t_newY)
                        refDataRows.AddDataRow(false, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY + t_newX, 15 * Manager_.selectedX + Manager_.selectedY, true);
                    else
                        refDataRows.AddDataRow(false, GetType(Manager_.selectedX, Manager_.selectedY), t, GetHp(Manager_.selectedX, Manager_.selectedY), 15 * t_newY + t_newX, 15 * Manager_.selectedX + Manager_.selectedY, false);
                }
            }
        }
        //move:
        float delta = refUserInterface.speedUp == 1 ? .15f : (refUserInterface.speedUp == 5 ? .2f : .5f);
        for (int i = 0; i < refLayerPath.listArmyMove.Count; i++)
        {
            Vector2 to = refLayerPath.listArmyMove[i];
            while (Vector2.Distance(to, Manager_.savedArmy.transform.position) > 0)
            {
                Manager_.savedArmy.transform.position = Vector2.MoveTowards(Manager_.savedArmy.transform.position, to, delta);
                yield return null;
            }
        }
        Manager_.newX = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].x;
        Manager_.newY = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].y;
       
        manager.FeedBack();
    }
    public bool CheckFire(Manager_.OnMove oM)
    {
        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
            listEnemyCanFire = new List<ArmyForAI>();
        bool isFire = false;
        int x, y;
        x = Manager_.newX - 1; y = Manager_.newY;//left
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Manager_.TypeArmy t;
            if (layerArmy[x, y] != null)
            {
                t = layerArmy[x, y].type;
                if (oM == Manager_.OnMove.Blue)//blue
                {
                    if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        isFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                        isFire = true;
                    }
                }
            }
        }
        x = Manager_.newX + 1; y = Manager_.newY;//right
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Manager_.TypeArmy t;
            if (layerArmy[x, y] != null){
                t = layerArmy[x, y].type;
                if (oM == Manager_.OnMove.Blue)//blue
                {
                    if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                        isFire = true;
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                        isFire = true;
                    }
                }
            }
        }
        x = Manager_.newX; y = Manager_.newY + 1;//up
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Manager_.TypeArmy t;
            if (layerArmy[x, y] != null)
            {
                t = layerArmy[x, y].type;
                if (oM == Manager_.OnMove.Blue)//blue
                {
                    if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                        isFire = true;
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                        isFire = true;
                    }
                }
            }
        }
        x = Manager_.newX; y = Manager_.newY - 1;//down
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Manager_.TypeArmy t;
            if (layerArmy[x, y] != null)
            {
                t = layerArmy[x, y].type;
                if (oM == Manager_.OnMove.Blue)//blue
                {
                    if (t == Manager_.TypeArmy.redInfantry || t == Manager_.TypeArmy.redMech || t == Manager_.TypeArmy.redTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                        isFire = true;
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        if (refUserInterface.comVsCom.isOn || refUserInterface.pVsCom.isOn)
                        {
                            ArmyForAI a = new ArmyForAI(layerArmy[x, y], x, y);
                            listEnemyCanFire.Add(a);
                        }
                        isFire = true;
                    }
                }
            }
        }
        return isFire;
    }
}
