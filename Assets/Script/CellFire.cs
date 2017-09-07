using UnityEngine;
public class CellFire : MonoBehaviour {
    Manager manager;
    private Transform posSelect;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        posSelect = transform.GetChild(0).GetComponent<Transform>();
    }
    void OnMouseEnter()
    {
        manager.SetPosSelectIcon(posSelect.position.x, posSelect.position.y);        
    }
	void Start () {
		
	}
	void Update () {
		
	}
}
