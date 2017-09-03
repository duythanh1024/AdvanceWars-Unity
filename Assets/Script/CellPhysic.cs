using UnityEngine;
public class CellPhysic : MonoBehaviour {
    [SerializeField]
    int x, y;
    private Transform posSelect;
    private Transform select;
    void Awake()
    {
        select = GameObject.Find("Select").transform;
        posSelect = transform.GetChild(0).GetComponent<Transform>();
    }
    public void OnMouseEnter()
    {
        select.transform.position = posSelect.position;
    }
}
