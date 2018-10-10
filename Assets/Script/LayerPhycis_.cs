using UnityEngine;
public class LayerPhycis_ : MonoBehaviour {
    private Manager_ manager;
    private UserInterface refUserInterface;
    private CellPhysic_[,] layerPhysic;
	private void Awake () {
        refUserInterface = GameObject.Find("CanvasMain").GetComponent<UserInterface>();
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        layerPhysic = new CellPhysic_[15, 10];
	}
    public void OnMouseEnter(int x, int y)
    {
        if (refUserInterface.comVsCom.isOn)
        {
            if (manager.onMove == Manager_.OnMove.Red)
                manager.Physic_OnMouseEnter(x, y);
        }
        else
        {
            manager.Physic_OnMouseEnter(x, y);
        }
    }
    public void ClickOnMap(int x, int y)
    {
        if (refUserInterface.pVsCom.isOn)
        {
            if (manager.onMove == Manager_.OnMove.Red)
                manager.Physic_OnClick(x, y);
        }
        else
        {
            manager.Physic_OnClick(x, y);
        }
    }
    
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        manager.Cancel();
    //    }
    //}
}
