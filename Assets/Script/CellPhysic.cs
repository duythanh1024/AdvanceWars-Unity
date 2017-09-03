using UnityEngine;
public class CellPhysic : MonoBehaviour {
    Manager manager;
    DisplayInfo displayInfo;
    [SerializeField]
    int x, y;
    private Transform posSelect;
    private Transform select;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        displayInfo = GameObject.Find("Manager").GetComponent<DisplayInfo>();
        select = GameObject.Find("Select").transform;
        posSelect = transform.GetChild(0).GetComponent<Transform>();
    }
    void OnMouseEnter()
    {
        select.transform.position = posSelect.position;
        //displayInfo.Reset();
        //if (manager.mapTerrain[x,y].type != Manager.Type.none)
        //{
        //    displayInfo.SetInfo(manager.mapTerrain[x, y].type, manager.mapTerrain[x, y].def);		    
        //}
        //if (manager.mapArmy[x,y].type != Manager.Type.none)
        //{
        //    displayInfo.SetInfo(manager.mapArmy[x, y].type, manager.mapArmy[x, y].move, manager.mapArmy[x, y].gas);
        //}
        
    }
    
}
