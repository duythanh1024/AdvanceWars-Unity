using System.Collections.Generic;
using UnityEngine;
public class LayerPath_ : MonoBehaviour {
    [SerializeField]
    private Transform cellRed;
    [SerializeField]
    Sprite iconSelect, iconSelectFire;
    private Transform select;
    CellPath_[,] layerPath;
    LayerTerrain_ refLayerTerrain;
    LayerArmy_ refLayerArmy;
    private bool iconFire;
    List<List<Vector2>> listListPaths;
    public List<Vector2> listArmyMove;
	void Awake () {
        refLayerArmy = GameObject.Find("LayerArmy").GetComponent<LayerArmy_>();
        refLayerTerrain = GameObject.Find("LayerTerrain").GetComponent<LayerTerrain_>();
        select = GameObject.Find("IconSelect").GetComponent<Transform>();
        layerPath = new CellPath_[15, 10];
        listListPaths = new List<List<Vector2>>();
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
                layerPath[r,c] = new CellPath_();
        }
	}
    public void Wait()
    {
        ClearPathAndArea();
    }
    public void Capt()
    {
        ClearPathAndArea();
    }
    public void Fire()
    {
        ClearPathAndArea();
    }
    public bool IsCheck(int row, int col)
    {
        return layerPath[row, col].isArea;
    }
    public void SelectFire(bool isFire)
    {
        select.position = new Vector3(Manager_.currentX, Manager_.currentY);
        if (isFire)
        {
            if (!iconFire)
            {
                select.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = iconSelectFire;
                iconFire = true;
            }
        }
        else
                if (iconFire)
                {
                    select.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = iconSelect;
                    iconFire = false;
                }
    }
    public void ClearPathAndArea()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                layerPath[r, c].isArea = false;
                layerPath[r, c].checkDraw = false;
            }
        }
        for (int i = 1; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
    }
    public void ClearAreaCheck()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                layerPath[r, c].isArea = false;
            }
        }
    }
    public void ClearPath()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                layerPath[r, c].checkDraw = false;
            }
        }
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
        listArmyMove.Clear();
    }
    public void DrawArea()
    {
        
        FindArea(Manager_.savedArmy.type, Manager_.savedArmy.move, Manager_.savedX, Manager_.savedY);
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (layerPath[r, c].isArea)
                {
                    Instantiate((Resources.Load("CellRed") as GameObject), transform).transform.position = new Vector2(r, c);
                }
            }
        }
    }
    public void SelectArmy()
    {
        select.position = new Vector3(Manager_.currentX, Manager_.currentY);
    }
    
    public void DrawPath()
    {
        listListPaths.Clear();
        ClearPath();
        if (layerPath[Manager_.currentX, Manager_.currentY].isArea)
        {
            if (Manager_.savedX == Manager_.currentX && Manager_.savedY == Manager_.currentY)
            {
                listArmyMove.Add(new Vector2(Manager_.currentX, Manager_.currentY));
                return;
            }
            else
            {
                if (!refLayerArmy.IsNull(Manager_.currentX, Manager_.currentY))
                    return;
            }
            List<Vector2> l = new List<Vector2>();
            FindPath(Manager_.savedX, Manager_.savedY, Manager_.savedArmy.move, l);
            int min = 100;//find path min
            for (int i = 0; i < listListPaths.Count; i++)
            {
                if (min > listListPaths[i].Count)
                {
                    min = listListPaths[i].Count;
                }
            }
            for (int i = 0; i < listListPaths.Count; i++)//map check draw rom list has min count to layer path
            {
                if (listListPaths[i].Count == min)
                {
                    listArmyMove = new List<Vector2>();
                    for (int j = 0; j < min; j++)
                    {
                        listArmyMove.Add(listListPaths[i][j]);
                        layerPath[(int)listListPaths[i][j].x, (int)listListPaths[i][j].y].checkDraw = true;
                    }
                    break;
                }
            }
            for (int c = 0; c < 10; c++)//draw path
            {
                for (int r = 0; r < 15; r++)
                {
                    if (layerPath[r, c].checkDraw)
                    {
                        Instantiate((Resources.Load("CellGreen") as GameObject), transform.GetChild(0)).transform.position = new Vector2(r, c);
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
            path.Add(_path[i]);//add with parent path
        }
        path.Add(new Vector2(_xStart, _yStart));
        if (_xStart == Manager_.currentX && _yStart == Manager_.currentY)
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
                if (layerPath[_xStart, _yStart + 1].isArea)
                {
                    FindPath(_xStart, _yStart + 1, move - 1, path);
                }
            }
            if (_yStart - 1 >= 0)//down
            {
                if (layerPath[_xStart, _yStart - 1].isArea)
                {
                    FindPath(_xStart, _yStart - 1, move - 1, path);
                }
            }
            if (_xStart - 1 >= 0)//left
            {
                if (layerPath[_xStart - 1, _yStart].isArea)
                {
                    FindPath(_xStart - 1, _yStart, move - 1, path);
                }
            }
            if (_xStart + 1 <= 14)//right
            {
                if (layerPath[_xStart + 1, _yStart].isArea)
                {
                    FindPath(_xStart + 1, _yStart, move - 1, path);
                }
            }
        }

    }

    public void FindArea(Manager_.TypeArmy _type, int _move, int _x, int _y)
    {
        if (_move < 0 /*|| mapTerrain[_x, _y].check*/)
            return;
        else
        {
            Manager_.TypeArmy t = refLayerTerrain.layerTerrain[_x, _y].type;
            switch (_type)
            {
                case Manager_.TypeArmy.redTank:
                case Manager_.TypeArmy.blueTank:
                    if (t == Manager_.TypeArmy.grass || t == Manager_.TypeArmy.road || t == Manager_.TypeArmy.bridge || t == Manager_.TypeArmy.city || t == Manager_.TypeArmy.redCity || t == Manager_.TypeArmy.blueCity || t == Manager_.TypeArmy.blueHeadquater || t == Manager_.TypeArmy.redHeadquater)
                    {
                        layerPath[_x, _y].isArea = true;
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
                case Manager_.TypeArmy.redInfantry:
                case Manager_.TypeArmy.blueInfantry:
                case Manager_.TypeArmy.redMech:
                case Manager_.TypeArmy.blueMech:
                    if (t != Manager_.TypeArmy.blueTank || t != Manager_.TypeArmy.redTank || t != Manager_.TypeArmy.redInfantry || t != Manager_.TypeArmy.blueInfantry || t != Manager_.TypeArmy.blueMech || t != Manager_.TypeArmy.redMech)
                    {
                        layerPath[_x, _y].isArea = true;
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
   
}
