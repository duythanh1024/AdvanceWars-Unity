using UnityEngine;
public class CellTerrain : MonoBehaviour {
    Manager manager;
    public Manager.Type type = Manager.Type.none;
    DisplayInfo displayInfo;
    public int def, capt;
    public bool check;
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
    public void Set(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    void OnMouseEnter()
    {
        select.transform.position = posSelect.position;
        manager.xSelect = x;
        manager.ySelect = y;
        displayInfo.Reset();
        displayInfo.SetInfo(type, def, capt);
    }
   
  
}
