using UnityEngine;
public class CellTerrain : MonoBehaviour {
    Manager manager;
    public Manager.Type type = Manager.Type.none;
    DisplayInfo displayInfo;
    public int def, capt;
    public bool check, checkPath;
    [SerializeField]
    int x, y;
    private Transform posSelect;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        displayInfo = GameObject.Find("Manager").GetComponent<DisplayInfo>();
        posSelect = transform.GetChild(0).GetComponent<Transform>();
    }
    public void Set(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    void OnMouseEnter()
    {
        manager.SetPosSelectIcon(posSelect.position.x, posSelect.position.y);
        displayInfo.Reset();
        displayInfo.SetInfo(type, def, capt);
        manager.DrawPath(x, y);
    }
    void OnMouseDown()
    {
        manager.MoveArmyTo(x, y, type);
    }
  
}
