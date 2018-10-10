using UnityEngine;
public class CellPhysic_ : MonoBehaviour {
    private UserInterface refUserInterface;
    [SerializeField]
    public int x, y;
    LayerPhycis_ parent;
	void Awake () {
        refUserInterface = GameObject.Find("CanvasMain").GetComponent<UserInterface>();
        parent = transform.parent.GetComponent<LayerPhycis_>();
	}
    void OnMouseEnter()
    {
        if (!refUserInterface.comVsCom.isOn)
        {
            parent.OnMouseEnter(x, y);            
        }
    }
    void OnMouseDown()
    {
        if (!refUserInterface.comVsCom.isOn)
        {
            parent.ClickOnMap(x, y);
        }
    }
}
