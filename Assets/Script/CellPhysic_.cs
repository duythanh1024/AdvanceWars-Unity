using UnityEngine;
public class CellPhysic_ : MonoBehaviour {
    [SerializeField]
    public int x, y;
    LayerPhycis_ parent;
	void OnEnable () {
        parent = transform.parent.GetComponent<LayerPhycis_>();
	}
    void OnMouseEnter()
    {
        parent.OnMouseEnter(x, y);
    }
    void OnMouseDown()
    {
        parent.ClickOnMap(x, y);

    }
}
