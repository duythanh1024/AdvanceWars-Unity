using UnityEngine;
public class CellTerrain : MonoBehaviour {
    public Manager.Type type = Manager.Type.none;
    DisplayInfo displayInfo;
    public int def, capt;
    [SerializeField]
    int x, y;
    private Transform posSelect;
    private Transform select;
    void Awake()
    {
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
        displayInfo.Reset();
        displayInfo.SetInfo(type, def, capt);
    }
  
}
