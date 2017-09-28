using System.Collections;
using UnityEngine;
public class LayerArmy_ : MonoBehaviour {
    public bool r_Moved, b_Moved;
    private LayerPath_ refLayerPath;
    private LayerIM refLayerIM;
    private CellArmy_[,] layerArmy;
    Manager_ manager;
    [SerializeField]
    SpriteRenderer sprFire;
    [SerializeField]
    Sprite[] fire;
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            StartCoroutine(AnimFire(1,1));
            
        }
    }
    public void AddValueIM_Blue(Manager_.TypeArmy type = Manager_.TypeArmy.none)
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
                                    if (type != Manager_.TypeArmy.none)
                                    {
                                        manager.refIM.SetBlue(rr, cc, layerArmy[r, c].GetDame(type));
                                    }
                                    else
                                    {
                                        manager.refIM.SetBlue(rr, cc, 2);
                                    }
                                }
                            }
                        }
                        refLayerPath.ClearAreaCheck();
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
                                        manager.refIM.SetRed(rr, cc, layerArmy[r, c].GetDame(type));
                                    }
                                    else
                                    {
                                        manager.refIM.SetRed(rr, cc, 2);
                                    }
                                }
                            }
                        }
                        refLayerPath.ClearAreaCheck();
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
        float t = Time.time + .1f;
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
                t = Time.time + .1f;
            }
            yield return null;
        }
    }
    void Awake()
    {
        layerArmy = new CellArmy_[15, 10];
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        refLayerPath = GameObject.Find("LayerPath").GetComponent<LayerPath_>();
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
                        if (layerArmy[r, c].type == Manager_.TypeArmy.blueInfantry || layerArmy[r, c].type == Manager_.TypeArmy.blueMech || layerArmy[r, c].type == Manager_.TypeArmy.blueTank)
                        numBlue++;
            }
        }
        if (numRed == 0)
            return -1;
        if (numBlue == 0)
            return 1;
        return 0;
    }
    public bool IsFire(int x, int y)
    {
        return (layerArmy[x, y] != null && layerArmy[x, y].checkFire);
    }
    public bool Select(Manager_.OnMove onMove)
    {
        bool success = false;
        Manager_.savedArmy = layerArmy[Manager_.savedX, Manager_.savedY];//print(layerArmy[Manager_.savedX, Manager_.savedY].name);
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
            refLayerPath.DrawArea();
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
    public void ConfirmPosition()
    {
        if (Manager_.newX == Manager_.savedX && Manager_.newY == Manager_.savedY)
        {
            layerArmy[Manager_.newX, Manager_.newY].Moved();
        }
        else
        {
            layerArmy[Manager_.newX, Manager_.newY] = Manager_.savedArmy;
            layerArmy[Manager_.newX, Manager_.newY].Moved();
            layerArmy[Manager_.savedX, Manager_.savedY] = null;
        }
    }
    public void Capt()
    {
        layerArmy[Manager_.newX, Manager_.newY].Capt();
        StartCoroutine(WaitCapt());
    }
    private IEnumerator WaitCapt()
    {
        yield return new WaitForSeconds(1f);
        manager.FeedBack();
    }
    public void Wait()
    {
        manager.FeedBack();
    }
    public bool Fire(int xFoscus, int yFocus, int defFocus, int defCurrent)
    {
        bool isDie = false;
        bool wait = false;
        layerArmy[xFoscus, yFocus].checkFire = false;
        Manager_.savedArmy.checkFire = false;
        if (!layerArmy[xFoscus, yFocus].UpdateHp(Random.Range(0, 2) + layerArmy[xFoscus, yFocus].hp - (Manager_.savedArmy.GetDame(layerArmy[xFoscus, yFocus].type) - defFocus)))
        {
            StartCoroutine(AnimFire(xFoscus, yFocus));
            isDie = true;
            wait = true;
        }
        if (!wait)
        {
            if (!Manager_.savedArmy.UpdateHp(Random.Range(0, 2) + Manager_.savedArmy.hp - (layerArmy[xFoscus, yFocus].GetDame(Manager_.savedArmy.type) - defCurrent)))
            {
                StartCoroutine(AnimFire(Manager_.savedX, Manager_.savedY));
                isDie = true;
            }
        }
        else
        {
            if (!Manager_.savedArmy.UpdateHp(Random.Range(0, 2) + Manager_.savedArmy.hp - (layerArmy[xFoscus, yFocus].GetDame(Manager_.savedArmy.type) - defCurrent)))
            {
                StartCoroutine(AnimFire(Manager_.savedX, Manager_.savedY, true));
                isDie = true;
            }
        }
        return isDie;
    }
    public void Cancel()
    {
        Manager_.savedArmy.transform.position = new Vector2(Manager_.savedX, Manager_.savedY);
    }
    public bool CanMove()
    {
        if (refLayerPath.listArmyMove.Count == 1)//not move
        {
            manager.FeedBack();
            return false;
        }
        if (refLayerPath.listArmyMove.Count != 0)//cannot move
        {
            StartCoroutine(AnimMove());
            return true;
        }
        return false;
        
    }
    IEnumerator AnimMove()
    {
        for (int i = 0; i < refLayerPath.listArmyMove.Count; i++)
        {
            Vector2 to = refLayerPath.listArmyMove[i];
            while (Vector2.Distance(to, Manager_.savedArmy.transform.position) > 0)
            {
                Manager_.savedArmy.transform.position = Vector2.MoveTowards(Manager_.savedArmy.transform.position, to, .1f);
                yield return null;
            }
        }
        Manager_.newX = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].x;
        Manager_.newY = (int)refLayerPath.listArmyMove[refLayerPath.listArmyMove.Count - 1].y;
        manager.FeedBack();
    }
    public bool CheckFire(Manager_.OnMove oM)
    {
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
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
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
                        isFire = true;
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
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
                        isFire = true;
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
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
                        isFire = true;
                    }
                }
                else//red
                {
                    if (t == Manager_.TypeArmy.blueInfantry || t == Manager_.TypeArmy.blueMech || t == Manager_.TypeArmy.blueTank)
                    {
                        layerArmy[x, y].checkFire = true;
                        isFire = true;
                    }
                }
            }
        }
        return isFire;
    }
}
