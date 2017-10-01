using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class CreateMap_ : MonoBehaviour {
    Transform listArmy, listTerrain;
    [SerializeField]
    Text notifi;
    LayerArmy_ refLayerArmy;
    LayerTerrain_ refLayerTerrain;
   
    public CellTerrain_ cel;
    void Start()
    {
        refLayerArmy = GameObject.Find("LayerArmy").GetComponent<LayerArmy_>();
        listArmy = refLayerArmy.transform;
        refLayerTerrain = GameObject.Find("LayerTerrain").GetComponent<LayerTerrain_>();
        refLayerTerrain.layerTerrain = new CellTerrain_[15, 10];
        listTerrain = refLayerTerrain.transform;
        //GameObject go = Instantiate(grass);
        //CellArmy_ t = Instantiate(grass).GetComponent<CellArmy_>();
        //t.transform.position = new Vector3(44,4);
        //t.SetPosition(4, 5);
        //print(cel.name);
        //cel.transform.position = new Vector3(12,21);

        //GameObject c = Instantiate(Resources.Load("RedHQ")) as GameObject;
        //c.GetComponent<CellTerrain_>().SetPos(7, 7);
        StartCoroutine(LoadMap());
    }
    IEnumerator LoadMap()
    {
        //dataSaveMap_LayerArmy = new int[150];
        //dataSaveMap_LayerTerrain = new int[150];
        string fileName = "map";
        string line = null;
        StreamReader reader = null;
        try
        {
            reader = new StreamReader(Application.dataPath + @"\" + fileName);
        }
        catch (Exception)
        {
            notifi.text = "Could not find " + Application.dataPath + @"\" + fileName;
            yield break;
        }
        line = reader.ReadLine();
        string[] txtCell = line.Split(',');
        for (int i = 0; i < 150; i++)
        {
            InStance((CreateMap.Type)(Int32.Parse(txtCell[i])), i % 15, i / 15);
            //yield return null;            
        }
        for (int i = 150, k = 0; i < 300; i++, k++)
        {
            InStance((CreateMap.Type)(Int32.Parse(txtCell[i])), k % 15, k / 15);
            //if (txtCell[i] != "-1")
            //    yield return null;
        }
        //for (int c = 0; c < 10; c++)
        //{
        //    for (int r = 0; r < 15; r++)
        //    {
        //        print(refLayerTerrain.layerTerrain[r, c].name + " name");
        //    }
        //}
        yield break;
    }
    void InStance(CreateMap.Type type, int x, int y)
    {
        GameObject t;
        switch (type)
        {
            case CreateMap.Type.redTank:
                refLayerArmy.New((Instantiate(Resources.Load("RedTank"), listArmy) as GameObject).GetComponent<CellArmy_>(), x, y);
                break;
            case CreateMap.Type.redInfantry:
                refLayerArmy.New((Instantiate(Resources.Load("RedInfantry"), listArmy) as GameObject).GetComponent<CellArmy_>(), x, y);
                break;
            case CreateMap.Type.redMech:
                refLayerArmy.New((Instantiate(Resources.Load("RedMech"), listArmy) as GameObject).GetComponent<CellArmy_>(), x, y);
                break;
            case CreateMap.Type.blueTank:
                refLayerArmy.New((Instantiate(Resources.Load("BlueTank"), listArmy) as GameObject).GetComponent<CellArmy_>(), x, y);
                break;
            case CreateMap.Type.blueInfantry:
                refLayerArmy.New((Instantiate(Resources.Load("BlueInfantry"), listArmy) as GameObject).GetComponent<CellArmy_>(), x, y);
                break;
            case CreateMap.Type.blueMech:
                refLayerArmy.New((Instantiate(Resources.Load("BlueMech"), listArmy) as GameObject).GetComponent<CellArmy_>(), x, y);
                break;
            case CreateMap.Type.blueHeadquater:
                t = Instantiate(Resources.Load("BlueHQ"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.redHeadquater:
                t = Instantiate(Resources.Load("RedHQ"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.river:
                t = Instantiate(Resources.Load("River"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.streetHor:
                t = Instantiate(Resources.Load("StreetHor"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.streetVer:
                t = Instantiate(Resources.Load("StreetVer"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.streetTopRight:
                t = Instantiate(Resources.Load("StreetUpToLeft"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.streetTopLeft:
                t = Instantiate(Resources.Load("StreetUpToRight"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.streetBottomRight:
                t = Instantiate(Resources.Load("StreetDownToRight"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.streetBottomLeft:
                t = Instantiate(Resources.Load("StreetDownToLeft"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.moutain:
                t = Instantiate(Resources.Load("Mountain"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.city:
                t = Instantiate(Resources.Load("City"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.wood:
                t = Instantiate(Resources.Load("Wood"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.grass:
                refLayerTerrain.layerTerrain[x, y] = (Instantiate(Resources.Load("Grass"), listTerrain) as GameObject).GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            case CreateMap.Type.bridge:
                t = Instantiate(Resources.Load("Bridge"), listTerrain) as GameObject;
                refLayerTerrain.layerTerrain[x, y] = t.GetComponent<CellTerrain_>();
                refLayerTerrain.layerTerrain[x, y].transform.position = new Vector2(x, y);
                break;
            //default:
            //    manager.mapArmy[x, y] = new CellArmy();
            //    manager.mapArmy[x, y].type = Manager.Type.none;
            //    break;
                
        }
       
    }

    
	
	void Update () {
		
	}
}
