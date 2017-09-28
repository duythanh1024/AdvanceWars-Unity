using UnityEngine;
public class LayerTerrain_ : MonoBehaviour {
    public CellTerrain_[,] layerTerrain;
    private Manager_ manager;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
    }
    public void AddValueIM()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                manager.refIM.SetTerraint(r, c, layerTerrain[r, c].def);
                //manager.refIM.SetRed(r, c, layerTerrain[r, c].def);
                //manager.refIM.SetBlue(r, c, layerTerrain[r, c].def);
            }
        }
    }
    public bool CheckCapt(Manager_.OnMove oM)
    {
        if (Manager_.savedArmy.type == Manager_.TypeArmy.blueTank || Manager_.savedArmy.type == Manager_.TypeArmy.redTank)
        {
            return false;
        }
        Manager_.TypeArmy t = layerTerrain[Manager_.newX, Manager_.newY].type;
        if (oM == Manager_.OnMove.Red)
        {
            if (t == Manager_.TypeArmy.city || t == Manager_.TypeArmy.blueCity || t == Manager_.TypeArmy.blueHeadquater)
            {
                return true;
            }
        }
        else//blue
        {
            if (t == Manager_.TypeArmy.city || t == Manager_.TypeArmy.redCity || t == Manager_.TypeArmy.redHeadquater)
            {
                return true;
            }
        }
        return false;
    }
    public Manager_.TypeArmy GetType(int x, int y)
    {
        return layerTerrain[x, y].type;
    }
	public void Capt (Manager_.OnMove oM) {
        Manager_.TypeArmy t = layerTerrain[Manager_.newX, Manager_.newY].GetComponent<CellCity_>().Capt(oM);
        if (t == Manager_.TypeArmy.redCity)
        {
            Destroy(layerTerrain[Manager_.newX, Manager_.newY].gameObject);
            layerTerrain[Manager_.newX, Manager_.newY] = Instantiate(Resources.Load("RedCity") as GameObject, transform).GetComponent<CellTerrain_>();
            layerTerrain[Manager_.newX, Manager_.newY].transform.position = new Vector3(Manager_.newX, Manager_.newY);
        }
        else
        {
            if (t == Manager_.TypeArmy.blueCity)
            {
                Destroy(layerTerrain[Manager_.newX, Manager_.newY].gameObject);
                layerTerrain[Manager_.newX, Manager_.newY] = Instantiate(Resources.Load("BlueCity") as GameObject, transform).GetComponent<CellTerrain_>();
                layerTerrain[Manager_.newX, Manager_.newY].transform.position = new Vector3(Manager_.newX, Manager_.newY);
            }
        }
	}
}
