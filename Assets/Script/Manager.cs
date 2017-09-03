using UnityEngine;
public class Manager : MonoBehaviour {
    [SerializeField]
    Transform layerTerrainAndPhysic;
    public  CellTerrain[,] mapTerrain;
    public CellArmy[,] mapArmy;
    public enum Type
    {
        none,

        redTank,
        redInfantry,
        redMech,
        redCity,
        redHeadquater,

        blueTank,
        blueInfantry,
        blueMech,
        blueCity,
        blueHeadquater,

        river,
        road,
        moutain,
        city,
        wood,
        grass,
        bridge,
    }
	void Awake () {
        Screen.SetResolution(1090, 590, false);
        mapTerrain = new CellTerrain[15, 10];
        mapArmy = new CellArmy[15, 10];
        int l = layerTerrainAndPhysic.childCount;
	}
	void Update () {
		
	}
}
