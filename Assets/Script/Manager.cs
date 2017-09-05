using System.Collections.Generic;
using UnityEngine;
public class Manager : MonoBehaviour {
    public int xSelected, ySelected, xStart, yStart, currentMove;
    public GameObject cellPath, cellAreaMove, listPath;
    public GameObject[] t;
    List<List<Vector2>> listListPaths;
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
        armyWaitSelect,//on click
        armyWaitSelectMoveTo,//select position for move to
        armyMoving,
        armyWaitChoiceOption,//opt: fire, capt, cancel
    }
    public OnMoving onMoving = OnMoving.red;
    public State state = State.armyWaitSelect;
	void Awake () {
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
        if (Input.GetMouseButtonDown(1))
        {
            if (state == State.armyWaitSelectMoveTo)
            {
                for (int x = 0; x < 15; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        mapTerrain[x, y].check = false;
                    }
                }
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
    public void DrawPath(int xEnd, int yEnd)
    {
        xSelected = xEnd;
        ySelected = yEnd;
        listListPaths.Clear();
        if (state == State.armyWaitSelectMoveTo && mapTerrain[xEnd, yEnd].check)
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
            List<Vector2> l = new List<Vector2>();
            FindPath(xStart, yStart, currentMove, l);
            int min = 100;
            print(listListPaths.Count);
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
                    for (int j = 0; j < min; j++)
                    {
                        mapTerrain[(int)listListPaths[i][j].x, (int)listListPaths[i][j].y].checkPath = true; print("true");
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
        }
    }
    public void FindPath(int _xStart, int _yStart, int move, List<Vector2> _path)
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
            //for (int i = 0; i < path.Count; i++)
            //{
            //    mapTerrain[(int)path[i].x, (int)path[i].y].checkPath = true;
            //}
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
   
    public void SetForDrawPath(int _xStart, int _yStart, int _currMove)
    {
        xStart = _xStart;
        yStart = _yStart;
        currentMove = _currMove;
        state = State.armyWaitSelectMoveTo;
    }
    public void DrawArea()
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
    public void FindArea(Type _type, int _move, int _x, int _y)
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
    public void RequestSelect(Type type, int _move, int _x, int _y)
    {
        //if (onMoving == OnMoving.red)
        //{
        //    for (int i = 0; i < 60; i++)
        //    {
        //        transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.red;
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < 60; i++)
        //    {
        //        transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.blue;
        //    }
        //}
        //if (onMoving == OnMoving.red)
        //{
        //    switch (type)
        //    {
        //        case Type.blueTank:
        //        case Type.blueInfantry:
        //        case Type.blueMech:
        //        case Type.blueCity:
        //        case Type.blueHeadquater:
        //            return;
        //    }
        //}
        //else
        //{
        //    switch (type)
        //    {
        //        case Type.redTank:
        //        case Type.redInfantry:
        //        case Type.redMech:
        //        case Type.redCity:
        //        case Type.redHeadquater:
        //            return;
        //    }
        //}
        
        if (state == State.armyWaitSelect)
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
}
