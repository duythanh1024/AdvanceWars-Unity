using UnityEngine;
public class Manager : MonoBehaviour {
    public int xSelect, ySelect;
    public GameObject cellAreaMove, ListArea;
    public GameObject[] t;
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
    public void FindPath(int x, int y, int move, Vector2 parent)
    {
        if (move == 0 || (x == xSelect && y == ySelect))
            return;
        int dis;
        if (true)
        {
            
        }        
    }
    public void DrawPath()
    {
        for (int x = 0; x < 15; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (mapTerrain[x, y].check)
                {
                    Instantiate(cellAreaMove, transform).transform.position = new Vector3(x, y); ;
                }
            }
        }
        state = State.armyWaitSelectMoveTo;
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
                    break;
            }
            if (_x + 1 <= 14)
                FindArea(_type, _move - 1, _x + 1, _y);//right
            if (_y + 1 <= 9)
                FindArea(_type, _move - 1, _x, _y + 1);//up
            if (_y - 1 >= 0)
                FindArea(_type, _move - 1, _x, _y - 1);//down
            if (_x - 1 >= 0)
                FindArea(_type, _move - 1, _x - 1, _y);//left
           

            /*
            switch (_type)
            {
                case Type.redTank:
                case Type.blueTank:
                    if (_y + 1 <= 9 && mapTerrain[_x, _y + 1].type == Type.grass)//up
                    {
                        FindArea(_type, _move - 1, _x, _y + 1);
                    }
                    if (_x + 1 <= 14 && mapTerrain[_x + 1, _y].type == Type.grass)//right
                    {
                        FindArea(_type, _move - 1, _x + 1, _y);
                    }
                    if (_y - 1 >= 0 && mapTerrain[_x, _y - 1].type == Type.grass)//down
                    {
                        FindArea(_type, _move - 1, _x, _y - 1);
                    }
                    if (_x - 1 >= 0 && mapTerrain[_x - 1, _y].type == Type.grass)//left
                    {
                        FindArea(_type, _move - 1, _x + 1, _y);
                    }
                    break;
                case Type.redInfantry:
                case Type.blueInfantry:
                case Type.redMech:
                case Type.blueMech:
                    Type t;
                    if (_y + 1 <= 9)//up
                    {
                        t = mapTerrain[_x, _y + 1].type;
                        if (t != Type.blueTank || t != Type.redTank || t != Type.redInfantry || t != Type.blueInfantry || t != Type.blueMech || t != Type.redMech)
                        {
                            FindArea(_type, _move - 1, _x, _y + 1);
                        }
                    }
                    if (_x + 1 <= 14)//right
                    {
                        t = mapTerrain[_x + 1, _y].type;
                        if (t != Type.blueTank || t != Type.redTank || t != Type.redInfantry || t != Type.blueInfantry || t != Type.blueMech || t != Type.redMech)
                        {
                            FindArea(_type, _move - 1, _x + 1, _y);
                        }
                    }
                    if (_y - 1 >= 0)//down
                    {
                        t = mapTerrain[_x, _y - 1].type;
                        if (t != Type.blueTank || t != Type.redTank || t != Type.redInfantry || t != Type.blueInfantry || t != Type.blueMech || t != Type.redMech)
                        {
                            FindArea(_type, _move - 1, _x, _y - 1);
                        }
                    }
                    if (_x - 1 >= 0)//left
                    {
                        t = mapTerrain[_x - 1, _y].type;
                        if (t != Type.blueTank || t != Type.redTank || t != Type.redInfantry || t != Type.blueInfantry || t != Type.blueMech || t != Type.redMech)
                        {
                            FindArea(_type, _move - 1, _x - 1, _y);
                        }
                    }
                    break;
            }*/
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
