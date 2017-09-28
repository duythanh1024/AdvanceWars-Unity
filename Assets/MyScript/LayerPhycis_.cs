using UnityEngine;
public class LayerPhycis_ : MonoBehaviour {
    private Manager_ manager;
    private CellPhysic_[,] layerPhysic;
	private void OnEnable () {
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        layerPhysic = new CellPhysic_[15, 10];
	}
    public void OnMouseEnter(int x, int y)
    {
        manager.Physic_OnMouseEnter(x, y);
    }
    public void ClickOnMap(int x, int y)
    {
        manager.Physic_OnClick(x, y);  
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            manager.Cancel();
        }
    }
}
