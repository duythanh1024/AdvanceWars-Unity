using System;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
public class ReadMap : MonoBehaviour {
    [SerializeField]
    Text notifi;
    Manager manager;
    public int[] dataSaveMap_LayerArmy;
    public int[] dataSaveMap_LayerTerrain;
    public GameObject
        redTank,
        redInfantry,
        redMech,
        redHeadquater,

        blueTank,
        blueInfantry,
        blueMech,
        blueHeadquater,

        river,
        streetHor,
        streetVer,
        streetTopRight,
        streetTopLeft,
        streetBottomRight,
        streetBottomLeft,
        moutain,
        city,
        wood,
        grass,
        bridge;
    void Awake()
    {
        manager = GetComponent<Manager>();
    }
   
	IEnumerator Start () {
        dataSaveMap_LayerArmy = new int[150];
        dataSaveMap_LayerTerrain = new int[150];
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
            InStance((CreateMap.Type)(Int32.Parse(txtCell[i])), i%15, i/15);
            //yield return null;            
        }
        int k = 0;
        for (int i = 150; i < 300; i++)
        {
            InStance((CreateMap.Type)(Int32.Parse(txtCell[i])), k % 15, k / 15);
            k++;
            //if (txtCell[i] != "-1")
            //    yield return null;
        }
        //line = reader.ReadLine();
        //print(line);
        
        yield break;
	}
    void InStance(CreateMap.Type type, int x, int y)
    {
        switch (type)
        {
            case CreateMap.Type.redTank:
                manager.mapArmy[x, y] = Instantiate(redTank).GetComponent<CellArmy>();
                manager.mapArmy[x, y].transform.position = new Vector3(x, y, -1f);
                manager.mapArmy[x, y].Set(x, y);
                break;
            case CreateMap.Type.redInfantry:
                manager.mapArmy[x, y] = Instantiate(redInfantry).GetComponent<CellArmy>();
                manager.mapArmy[x, y].transform.position = new Vector3(x, y, -1f);
                manager.mapArmy[x, y].Set(x, y);
                break;
            case CreateMap.Type.redMech:
                manager.mapArmy[x, y] = Instantiate(redMech).GetComponent<CellArmy>();
                manager.mapArmy[x, y].transform.position = new Vector3(x, y, -1f);
                manager.mapArmy[x, y].Set(x, y);
                break;
            case CreateMap.Type.redHeadquater:
                manager.mapTerrain[x, y] = Instantiate(redHeadquater).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y, -1f);

                break;
            case CreateMap.Type.blueTank:
                manager.mapArmy[x, y] = Instantiate(blueTank).GetComponent<CellArmy>();
                manager.mapArmy[x, y].transform.position = new Vector3(x, y, -1f);
                manager.mapArmy[x, y].Set(x, y);
                break;
            case CreateMap.Type.blueInfantry:
                manager.mapArmy[x, y] = Instantiate(blueInfantry).GetComponent<CellArmy>();
                manager.mapArmy[x, y].transform.position = new Vector3(x, y, -1f);
                manager.mapArmy[x, y].Set(x, y);
                break;
            case CreateMap.Type.blueMech:
                manager.mapArmy[x, y] = Instantiate(blueMech).GetComponent<CellArmy>();
                manager.mapArmy[x, y].transform.position = new Vector3(x, y, -1f);
                manager.mapArmy[x, y].Set(x, y);
                break;
            case CreateMap.Type.blueHeadquater:
                manager.mapTerrain[x, y] = Instantiate(blueHeadquater).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x,y);
                break;
            case CreateMap.Type.river:
                manager.mapTerrain[x, y] = Instantiate(river).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.streetHor:
                manager.mapTerrain[x, y] = Instantiate(streetHor).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.streetVer:
                manager.mapTerrain[x, y] = Instantiate(streetVer).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.streetTopRight:
                manager.mapTerrain[x, y] = Instantiate(streetTopRight).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.streetTopLeft:
                manager.mapTerrain[x, y] = Instantiate(streetTopLeft).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.streetBottomRight:
                manager.mapTerrain[x, y] = Instantiate(streetBottomRight).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.streetBottomLeft:
                manager.mapTerrain[x, y] = Instantiate(streetBottomLeft).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.moutain:
                manager.mapTerrain[x, y] = Instantiate(moutain).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.city:
                manager.mapTerrain[x, y] = Instantiate(city).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.wood:
                manager.mapTerrain[x, y] = Instantiate(wood).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.grass:
                manager.mapTerrain[x, y] = Instantiate(grass).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
            case CreateMap.Type.bridge:
                manager.mapTerrain[x, y] = Instantiate(bridge).GetComponent<CellTerrain>();
                manager.mapTerrain[x, y].transform.position = new Vector3(x, y);
                manager.mapTerrain[x, y].Set(x, y);
                break;
        }
    }
	void Update () {
		
	}
}
