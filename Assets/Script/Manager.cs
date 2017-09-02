using UnityEngine;
public class Manager : MonoBehaviour {
    CellTerrain[,] mapTerrain;
    CellArmy[,] mapArmy;
    public enum Type
    {
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
	void Start () {
		
	}
	void Update () {
		
	}
}
