using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour {
    [SerializeField]
    Sprite selectFire, selectNormal,
    one, two, three, four, five, six, seven, eight, nine, zero;
    private Transform select;//icon select on the map
    public GameObject cellPath, cellAreaMove, listPath;
    public Option option;
    private Transform trFocus;
    private int xNewArm, yNewArm;
    public GameObject[] t;
    private CellArmy armyCurrent;
    public int xSelected, ySelected, xStart, yStart, currentMove;
    private int xSavePosArmy, ySavePosArmy;
    private Type typeSaveArmy;
    List<List<Vector2>> listListPaths;
    List<Vector2> listArmyMove;
    [SerializeField]
    Transform layerTerrainAndPhysic;
    public  CellTerrain[,] mapTerrain;
    public CellArmy[,] mapArmy;

    public enum Type
    {
        none,

        redTank,
        redInfantry,
        redMech,
        redCity,
        redHeadquater,

        blueTank,
        blueInfantry,
        blueMech,
        blueCity,
        blueHeadquater,

        river,
        road,
        moutain,
        city,
        wood,
        grass,
        bridge,
    }
    public enum OnMoving
    {
        red,
        blue
    }
    public enum State
    {
        WaitSelectArmy,//on click
        WaitSelectPointForMoveTo,//select position for move to
        AnimationMoving,//army is moving - animtion
        WaitChoiceOption,//opt: fire, capt, cancel
        SelectArmtForFire
    }
    public OnMoving onMoving = OnMoving.red;
    public State state = State.WaitSelectArmy;
	void Awake () {
        select = GameObject.Find("Select").transform;
        Screen.SetResolution(1090, 590, false);
        mapTerrain = new CellTerrain[15, 10];
        mapArmy = new CellArmy[15, 10];
        int l = layerTerrainAndPhysic.childCount;
        listListPaths = new List<List<Vector2>>();
	}
	void _Start () {
        for (int i = 0; i < 60; i++)
        {
            Instantiate(cellAreaMove, transform);
        }
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//cancel
        {
            if (state == State.WaitSelectPointForMoveTo)
            {
                ClearArea();
                ClearPath();
                state = State.WaitSelectArmy;
            }
        }
    }
    private void NextOnMove()
    {
        //check win?
        //next
        if (onMoving == OnMoving.red)
            onMoving = OnMoving.blue;
        else
            onMoving = OnMoving.red;
        state = State.WaitSelectArmy;
    }
    public void MoveArmyTo(int x, int y, Type type)
    {
        if (state == State.WaitSelectPointForMoveTo)
        {
            if (mapTerrain[x, y].checkPath && (type != Type.redInfantry && type != Type.redMech && type != Type.redTank && type != Type.blueInfantry && type != Type.blueMech && type != Type.blueTank))
            {
                state = State.AnimationMoving;
                StartCoroutine(MoveTo(type));
            }
        }
        
    }
    public void ShowOption(Option.TypeOption typeOp)
    {
        
    }
    IEnumerator MoveTo(Type type)
    {
        for (int i = 0; i < listArmyMove.Count; i++)
        {
            Vector2 to = listArmyMove[i];
            while (Vector2.Distance(to, trFocus.position) > 0)
            {
                trFocus.transform.position = Vector2.MoveTowards(trFocus.position, to, .1f);
                yield return null;
            }
        }
        trFocus.transform.position = new Vector3(trFocus.transform.position.x, trFocus.transform.position.y, -1f);
        xNewArm = (int)listArmyMove[listArmyMove.Count - 1].x;
        yNewArm = (int)listArmyMove[listArmyMove.Count - 1].y;
        CheckOption(type);
    }
    private bool CheckAround()
    {
        bool isFire = false;
        int x, y;
        x = xNewArm - 1; y = yNewArm;//left
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Type t = mapArmy[x, y].type;
            if (onMoving == OnMoving.blue)//blue
            {
                if (t == Type.redInfantry || t == Type.redMech || t == Type.redTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
            else//red
            {
                if (t == Type.blueInfantry || t == Type.blueMech || t == Type.blueTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
        }
        x = xNewArm + 1; y = yNewArm;//right
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Type t = mapArmy[x, y].type;
            if (onMoving == OnMoving.blue)//blue
            {
                if (t == Type.redInfantry || t == Type.redMech || t == Type.redTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
            else//red
            {
                if (t == Type.blueInfantry || t == Type.blueMech || t == Type.blueTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
        }
        x = xNewArm ; y = yNewArm + 1;//up
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Type t = mapArmy[x, y].type;
            if (onMoving == OnMoving.blue)//blue
            {
                if (t == Type.redInfantry || t == Type.redMech || t == Type.redTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
            else//red
            {
                if (t == Type.blueInfantry || t == Type.blueMech || t == Type.blueTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
        }
        x = xNewArm; y = yNewArm - 1;//down
        if (y <= 9 && y >= 0 && x <= 14 && x >= 0)
        {
            Type t = mapArmy[x, y].type;
            if (onMoving == OnMoving.blue)//blue
            {
                if (t == Type.redInfantry || t == Type.redMech || t == Type.redTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
            else//red
            {
                if (t == Type.blueInfantry || t == Type.blueMech || t == Type.blueTank)
                {
                    mapArmy[x, y].checkFire = true;
                    isFire = true;
                }
            }
        }
        
        return isFire;
    }
    private void ChoiceArmtForFire()
    {
        
    }
    private void CheckOption(Type t)
    {
        Option.TypeOption typeOption = Option.TypeOption.wait;
        bool canFire = false;
        bool canCapt = false;
        if (onMoving == OnMoving.blue)
        {
            switch (t)
            {
                case Type.redCity:
                case Type.redHeadquater:
                case Type.city:
                    canFire = CheckAround();
                    canCapt = true;
                    break;
                case Type.blueCity:
                case Type.blueHeadquater:
                    canFire = CheckAround();                    
                    break;
                case Type.river:
                case Type.road:
                case Type.moutain:
                case Type.wood:
                case Type.grass:
                case Type.bridge:
                    canFire = CheckAround();
                    break;
            }
        }
        else//on move red
        {
            switch (t)
            {
                case Type.blueCity:
                case Type.blueHeadquater:
                case Type.city:
                    canFire = CheckAround();
                    canCapt = true;
                    break;
                case Type.redCity:
                case Type.redHeadquater:
                    canFire = CheckAround();
                    break;
                case Type.river:
                case Type.road:
                case Type.moutain:
                case Type.wood:
                case Type.grass:
                case Type.bridge:
                    canFire = CheckAround();
                    break;
            }
        }//end else if
        if (canFire && canCapt)
            typeOption = Option.TypeOption.captAndFire;
        else
            if (canCapt)
                typeOption = Option.TypeOption.capt;
            else
                if (canFire)
                    typeOption = Option.TypeOption.fire;
                else
                    typeOption = Option.TypeOption.wait;
        state = State.WaitChoiceOption;
        option.Show(typeOption);
    }
    public void SetPosSelectIcon(float x, float y, bool isFireCheck = false)
    {
        if(state == State.WaitSelectArmy)
            select.transform.position = new Vector3(x, y);
        else
        {
            if (state == State.SelectArmtForFire && isFireCheck)
            {
                select.transform.position = new Vector3(x, y);
            }
        }
    }
    public void OnClickArmy(CellArmy army)
    {
        if (state != State.WaitSelectArmy)
            return;
        if (onMoving == OnMoving.red)
        {
            armyCurrent = army;
            Type type = army.type;
            if (type == Type.redInfantry || type == Type.redTank || type == Type.redMech)//red move if onMove = red
                xSavePosArmy = army.x;
                ySavePosArmy = army.y;
                typeSaveArmy = army.type;
                trFocus = army.transform;
                FindArea(army.type, army.move + 1, army.x, army.y);
                DrawArea();
                SetForDrawPath(army);
        }
        else
        {
            armyCurrent = army;
            Type type = army.type;
            if (type == Type.blueInfantry || type == Type.blueMech || type == Type.blueTank)//blue move if onMove = blue
                if (state == State.WaitSelectArmy)
                {
                    xSavePosArmy = army.x;
                    ySavePosArmy = army.y;
                    typeSaveArmy = army.type;
                    trFocus = army.transform;
                    FindArea(army.type, army.move + 1, army.x, army.y);
                    DrawArea();
                    SetForDrawPath(army);
                }
        }
        
    }
  
    private void ClearPath()
    {
        for (int i = 0; i < listPath.transform.childCount; i++)
        {
            Destroy(listPath.transform.GetChild(i).gameObject);
        }
        for (int x = 0; x < 15; x++)//reset false
        {
            for (int y = 0; y < 10; y++)
            {
                mapTerrain[x, y].checkPath = false;
            }
        }
    }
    private void ClearArea(bool clearFire = true)
    {
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                mapTerrain[x, y].check = false;
                if(clearFire)
                    mapArmy[x, y].checkFire = false;
            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void DrawPath(int xEnd, int yEnd)
    {
        if (state == State.WaitSelectPointForMoveTo && mapTerrain[xEnd, yEnd].check)
        {
            xSelected = xEnd;
            ySelected = yEnd;
            listListPaths.Clear();
            ClearPath();
            List<Vector2> l = new List<Vector2>();
            FindPath(xStart, yStart, currentMove, l);
            int min = 100;//find path min
            for (int i = 0; i < listListPaths.Count; i++)
			{
                if (min > listListPaths[i].Count)
                {
                    min = listListPaths[i].Count;
                }
			}
            for (int i = 0; i < listListPaths.Count; i++)
            {
                if (listListPaths[i].Count == min)
                {
                    listArmyMove = new List<Vector2>();
                    for (int j = 0; j < min; j++)
                    {
                        listArmyMove.Add(listListPaths[i][j]);
                        mapTerrain[(int)listListPaths[i][j].x, (int)listListPaths[i][j].y].checkPath = true;
                    }
                    break;
                }
            }
            for (int x = 0; x < 15; x++)//draw path
            {
                for (int y = 0; y < 10; y++)
                {
                    if (mapTerrain[x, y].checkPath)
                    {
                        Instantiate(cellPath, listPath.transform).transform.position = new Vector3(x, y);
                    }
                }
            }
        }//end if
    }
    private void FindPath(int _xStart, int _yStart, int move, List<Vector2> _path)
    {
        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < _path.Count; i++)
        {
            path.Add(_path[i]);
        }
        path.Add(new Vector2(_xStart, _yStart));
        if (_xStart == xSelected && _yStart == ySelected)
        {
            listListPaths.Add(path);
            return;
        }
        if (move == 0)
        {
            return;
        }
        else
        {
            if (_yStart + 1 <= 9)//up
            {
                if (mapTerrain[_xStart, _yStart + 1].check)
                {
                    FindPath(_xStart, _yStart + 1, move - 1, path);
                }
            }
            if (_yStart - 1 >= 0)//down
            {
                if (mapTerrain[_xStart, _yStart - 1].check)
                {
                    FindPath(_xStart, _yStart - 1, move - 1, path);
                }
            }
            if (_xStart - 1 >= 0)//left
            {
                if (mapTerrain[_xStart - 1, _yStart].check)
                {
                    FindPath(_xStart - 1, _yStart, move - 1, path);
                }
            }
            if (_xStart + 1 <= 14)//right
            {
                if (mapTerrain[_xStart + 1, _yStart].check)
                {
                    FindPath(_xStart + 1, _yStart, move - 1, path);
                }
            }
        }

    }

    private void SetForDrawPath(CellArmy army)
    {
        xStart = army.x;
        yStart = army.y;
        currentMove = army.move;
        state = State.WaitSelectPointForMoveTo;
    }
    private void DrawArea()
    {
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (mapTerrain[x, y].check)
                {
                    Instantiate(cellAreaMove, transform).transform.position = new Vector3(x, y);
                }
            }
        }
    }
    private void FindArea(Type _type, int _move, int _x, int _y)
    {
        if (_move == 0 /*|| mapTerrain[_x, _y].check*/)
            return;
        else
	    {
            Type t = mapTerrain[_x, _y].type;
            switch (_type)
            {
                case Type.redTank:
                case Type.blueTank:
                    if (t == Type.grass || t == Type.road || t == Type.bridge)
                    {
                        mapTerrain[_x, _y].check = true;
                        if (_x + 1 <= 14)
                            FindArea(_type, _move - 1, _x + 1, _y);//right
                        if (_y + 1 <= 9)
                            FindArea(_type, _move - 1, _x, _y + 1);//up
                        if (_y - 1 >= 0)
                            FindArea(_type, _move - 1, _x, _y - 1);//down
                        if (_x - 1 >= 0)
                            FindArea(_type, _move - 1, _x - 1, _y);//left
                    }
                    break;
                case Type.redInfantry:
                case Type.blueInfantry:
                case Type.redMech:
                case Type.blueMech:
                    if (t != Type.blueTank || t != Type.redTank || t != Type.redInfantry || t != Type.blueInfantry || t != Type.blueMech || t != Type.redMech)
                    {
                        mapTerrain[_x, _y].check = true;
                    }
                    if (_x + 1 <= 14)
                        FindArea(_type, _move - 1, _x + 1, _y);//right
                    if (_y + 1 <= 9)
                        FindArea(_type, _move - 1, _x, _y + 1);//up
                    if (_y - 1 >= 0)
                        FindArea(_type, _move - 1, _x, _y - 1);//down
                    if (_x - 1 >= 0)
                        FindArea(_type, _move - 1, _x - 1, _y);//left
                    break;

            }
            
	    }//end else if
        
    }
   
    private void RequestSelect(Type type, int _move, int _x, int _y)
    {
        if (state == State.WaitSelectArmy)
        {
            switch (_move)
            {
                case 2:
                    for (int i = 0; i < 60; i++)
                    {
                        transform.GetChild(i).transform.position = new Vector3(100, 100);
                    }
                    if (_x - 1 >= 0 && _y + 1 <= 9)
                    {
                        transform.GetChild(0).position = new Vector3(_x - 1, _y + 1);
                    }
                    if (_y + 1 <= 9)
                    {
                        transform.GetChild(1).position = new Vector3(_x, _y + 1);
                    }
                    if (_x + 1 <= 14 && _y + 1 <= 9)
                    {
                        transform.GetChild(2).position = new Vector3(_x + 1, _y + 1);
                    }
                    if (_x - 1 >= 0)
                    {
                        transform.GetChild(3).position = new Vector3(_x - 1, _y);
                    }
                    transform.GetChild(4).position = new Vector3(_x, _y);
                    if (_x + 1 <= 14)
                    {
                        transform.GetChild(5).position = new Vector3(_x + 1, _y);
                    }
                    if (_x - 1 >= 0 && _y - 1 >= 0)
                    {
                        transform.GetChild(6).position = new Vector3(_x - 1, _y - 1);
                    }
                    if (_y - 1 >= 0)
                    {
                        transform.GetChild(7).position = new Vector3(_x, _y - 1);
                    }
                    if (_x + 1 <= 14 && _y - 1 >= 0)
                    {
                        transform.GetChild(8).position = new Vector3(_x + 1, _y - 1);
                    }
                    if (_x - 2 >= 0)
                    {
                        transform.GetChild(9).position = new Vector3(_x - 2, _y);
                    }
                    if (_y + 2 <= 9)
                    {
                        transform.GetChild(10).position = new Vector3(_x, _y + 2);
                    }
                    if (_x + 2 <= 14)
                    {
                        transform.GetChild(11).position = new Vector3(_x + 2, _y);
                    }
                    if (_y - 2 >= 0)
                    {
                        transform.GetChild(12).position = new Vector3(_x, _y - 2);
                    }
                    break;
                case 5:

                    break;
            }
        }
    }
    public void FireWith(CellArmy army)
    {
        if (state == State.SelectArmtForFire && army.checkFire)
        {
            armyCurrent.x = (int)listArmyMove[listArmyMove.Count - 1].x;
            armyCurrent.y = (int)listArmyMove[listArmyMove.Count - 1].y;
            mapArmy[(int)listArmyMove[listArmyMove.Count - 1].x, (int)listArmyMove[listArmyMove.Count - 1].y] = armyCurrent;

            army.name = "~";
            print("name "  + armyCurrent.name);
            int i = army.hp - (armyCurrent.dame - mapTerrain[army.x, army.y].def);
            army.UpdatePro(i, army.gas - listArmyMove.Count);
            print("hp " + army.hp);
            print("cur dame " + armyCurrent.dame);
            print(i);
            print("def " + mapTerrain[army.x, army.y].def);
            //armyCurrent.UpdatePro(army.dame - mapTerrain[])

            select.GetComponent<SpriteRenderer>().sprite = selectNormal;
            NextOnMove();
        }

        
    }
   
    //option
    public void Cancel()
    {
        trFocus.transform.position = new Vector3(xSavePosArmy, ySavePosArmy, -1f);
        ClearArea();
        ClearPath();
        state = State.WaitSelectArmy;
    }
    public void Fire()
    {
        state = State.SelectArmtForFire;
        select.GetComponent<SpriteRenderer>().sprite = selectFire;
        ClearArea(false);
        ClearPath();
    }
    public void Capt()
    {

    }
    public void Wait()
    {
        armyCurrent.x = (int)listArmyMove[listArmyMove.Count - 1].x;
        armyCurrent.y = (int)listArmyMove[listArmyMove.Count - 1].y;
        //mapArmy[xSavePosArmy, ySavePosArmy].type = Type.none;
        //mapArmy[(int)listArmyMove[listArmyMove.Count - 1].x, (int)listArmyMove[listArmyMove.Count - 1].y] .type = typeSaveArmy;
        mapArmy[(int)listArmyMove[listArmyMove.Count - 1].x, (int)listArmyMove[listArmyMove.Count - 1].y] = armyCurrent;
        ClearArea();
        ClearPath();
        NextOnMove();
    }
}
