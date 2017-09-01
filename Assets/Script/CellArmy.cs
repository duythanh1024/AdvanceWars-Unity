using UnityEngine;
public class CellArmy : MonoBehaviour {
    [SerializeField]
    private Manager.Type type;
    private Transform posSelect;
    private Transform select;
    void Awake()
    {
        select = GameObject.Find("Select").transform;
        posSelect = transform.GetChild(0).GetComponent<Transform>();
    }
    void OnMouseDown()
    {
        print(name);
    }
    public void OnMouseEnter()
    {
        select.transform.position = posSelect.position;
    }
    public void ShowInfo()
    {

    }
    
}
