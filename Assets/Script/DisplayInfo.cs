using UnityEngine;
using UnityEngine.UI;
public class DisplayInfo : MonoBehaviour {
    [SerializeField]
    GameObject panelTerrain, panelarmy;
    public Sprite iconHp, iconRiver, iconRoach, iconDame, iconGrass, iconDef, iconMt, iconWood, iconCity, iconRedCity, iconBlueCity, iconBridge, iconMove, iconGas, iconRedTank, iconBlueTank, iconRedInfantry, iconBlueInfantry, iconRedMech, iconBlueMech, one, two, three, four, five, six, seven, eight, nine, zero,
           nameRiver, nameRoach, nameMt, nameWood, nameGrass, nameCity, nameBridge, nameTak, nameInfantry, nameMech, nameDame, iconCapt, iconBlueQH, iconRedHQ, iconHQ;
    [SerializeField]
    Image arName, arHp, arMove, arValueHp1, arValueHp2, arValueMove, arValueGas1, arValueGas2, arDisplay, arGas, arDame, arvalueDame1, arValueDame2;
    [SerializeField]
    Image terName, terSpr, terDef, terValueDef, terCapt, terValueCapt1, terValueCapt2;
	void Start () {
		
	}
	void Update () {
		
	}
    public void SetInfo(Manager.Type type, int move, int gas, int hp, int dame)
    {
        panelarmy.gameObject.SetActive(true);
        switch (type)
        {
            case Manager.Type.redTank:
                arName.sprite = nameTak;
                arDisplay.sprite = iconRedTank;
                arDame.sprite = iconDame;
                arGas.sprite = iconGas;
                arHp.sprite = iconHp;
                arMove.sprite = iconMove;
                IntToSprite(dame, arvalueDame1, arValueDame2);
                IntToSprite(gas, arValueGas1, arValueGas2);
                IntToSprite(move, arValueMove);
                IntToSprite(hp, arValueHp1, arValueHp2);
                break;
            case Manager.Type.blueTank:
                arName.sprite = nameTak;
                arDisplay.sprite = iconBlueTank;
                arDame.sprite = iconDame;
                arGas.sprite = iconGas;
                arHp.sprite = iconHp;
                arMove.sprite = iconMove;
                IntToSprite(dame, arvalueDame1, arValueDame2);
                IntToSprite(gas, arValueGas1, arValueGas2);
                IntToSprite(move, arValueMove);
                IntToSprite(hp, arValueHp1, arValueHp2);
                break;
            case Manager.Type.redInfantry:
                arName.sprite = nameInfantry;
                arDisplay.sprite = iconRedInfantry;
                arMove.sprite = iconMove;
                arGas.sprite = iconGas;
                arHp.sprite = iconHp;
                arDame.sprite = iconDame;
                IntToSprite(gas, arValueGas1, arValueGas2);
                IntToSprite(move, arValueMove);
                IntToSprite(hp, arValueHp1, arValueHp2);
                IntToSprite(dame, arvalueDame1, arValueDame2);
                break;
            case Manager.Type.blueInfantry:
                arName.sprite = nameInfantry;
                arDisplay.sprite = iconBlueInfantry;
                arMove.sprite = iconMove;
                arGas.sprite = iconGas;
                arHp.sprite = iconHp;
                arDame.sprite = iconDame;
                IntToSprite(gas, arValueGas1, arValueGas2);
                IntToSprite(move, arValueMove);
                IntToSprite(hp, arValueHp1, arValueHp2);
                IntToSprite(dame, arvalueDame1, arValueDame2);
                break;
            case Manager.Type.redMech:
                arName.sprite = nameMech;
                arDisplay.sprite = iconRedMech;
                arMove.sprite = iconMove;
                arGas.sprite = iconGas;
                arHp.sprite = iconHp;
                arDame.sprite = iconDame;
                IntToSprite(gas, arValueGas1, arValueGas2);
                IntToSprite(move, arValueMove);
                IntToSprite(hp, arValueHp1, arValueHp2);
                IntToSprite(dame, arvalueDame1, arValueDame2);
                break;
            case Manager.Type.blueMech:
                arName.sprite = nameMech;
                arDisplay.sprite = iconBlueMech;
                arMove.sprite = iconMove;
                arGas.sprite = iconGas;
                arHp.sprite = iconHp;
                arDame.sprite = iconDame;
                IntToSprite(gas, arValueGas1, arValueGas2);
                IntToSprite(move, arValueMove);
                IntToSprite(hp, arValueHp1, arValueHp2);
                IntToSprite(dame, arvalueDame1, arValueDame2);
                break;
        }
    }
    public void SetInfo(Manager.Type type, int def, int capt)
    {
        panelTerrain.gameObject.SetActive(true);
        terCapt.gameObject.SetActive(true);
        terValueCapt1.gameObject.SetActive(true);
        terValueCapt2.gameObject.SetActive(true);
            switch (type)
	        {
                case Manager.Type.redCity:
                    terName.sprite = nameCity;
                    terSpr.sprite = iconRedCity;
                    IntToSprite(def, terValueDef);
                    IntToSprite(capt, terValueCapt1, terValueCapt2);
                    break;
                case Manager.Type.redHeadquater:
                    terName.sprite = iconHQ;
                    terSpr.sprite = iconRedHQ;
                    IntToSprite(def, terValueDef);
                    IntToSprite(capt, terValueCapt1, terValueCapt2);
                    break;
                case Manager.Type.blueCity:
                    terName.sprite = nameCity;
                    terSpr.sprite = iconBlueCity;
                    IntToSprite(def, terValueDef);
                    IntToSprite(capt, terValueCapt1, terValueCapt2);
                    break;
                case Manager.Type.blueHeadquater:
                    terName.sprite = iconHQ;
                    terSpr.sprite = iconBlueQH;
                    IntToSprite(def, terValueDef);
                    IntToSprite(capt, terValueCapt1, terValueCapt2);
                     break;
                case Manager.Type.river:
                     terName.sprite = nameRiver;
                    terSpr.sprite = iconRiver;
                    terCapt.gameObject.SetActive(false);
                    terValueCapt2.gameObject.SetActive(false);
                    terValueCapt1.gameObject.SetActive(false);
                    IntToSprite(def, terValueDef);
                    break;
                case Manager.Type.road:
                    terName.sprite = nameRoach;
                    terSpr.sprite = iconRoach;
                    terCapt.gameObject.SetActive(false);
                    terValueCapt2.gameObject.SetActive(false);
                    terValueCapt1.gameObject.SetActive(false);
                    IntToSprite(def, terValueDef);
                    break;
                case Manager.Type.moutain:
                    terName.sprite = nameMt;
                    terSpr.sprite = iconMt;
                    terCapt.gameObject.SetActive(false);
                    terValueCapt2.gameObject.SetActive(false);
                    terValueCapt1.gameObject.SetActive(false);
                    IntToSprite(def, terValueDef);
                    break;
                case Manager.Type.city:
                    terName.sprite = nameCity;
                    terSpr.sprite = iconCity;
                    terCapt.sprite = iconCapt;
                    IntToSprite(0, terValueCapt1, terValueCapt2);
                    IntToSprite(0, terValueDef, terValueCapt2);
                    break;
                case Manager.Type.wood:
                    terName.sprite = nameWood;
                    terSpr.sprite = iconWood;
                    terCapt.gameObject.SetActive(false);
                    terValueCapt2.gameObject.SetActive(false);
                    terValueCapt1.gameObject.SetActive(false);
                    IntToSprite(def, terValueDef);
                    break;
                case Manager.Type.grass:
                    terName.sprite = nameGrass;
                    terSpr.sprite = iconGrass;
                    terCapt.gameObject.SetActive(false);
                    terValueCapt2.gameObject.SetActive(false);
                    terValueCapt1.gameObject.SetActive(false);
                    IntToSprite(def, terValueDef);
                    break;
                case Manager.Type.bridge:
                    terName.sprite = nameBridge;
                    terSpr.sprite = iconBridge;
                    terCapt.gameObject.SetActive(false);
                    terValueCapt2.gameObject.SetActive(false);
                    terValueCapt1.gameObject.SetActive(false);
                    IntToSprite(def, terValueDef);
                    break;
	    }
    }
    public void Reset()
    {
        panelarmy.gameObject.SetActive(false);
        panelTerrain.gameObject.SetActive(false);
    }
    public void IntToSprite(int value, Image img1, Image img2 = null)
    {
        int b = (value / 10) % 10;
        int a = value % 10;
        Sprite spr1 = null;
        switch (a)
        {
            case 0:
                spr1 = zero;
                break;
            case 1:
                spr1 = one;
                break;
            case 2:
                spr1 = two;
                break;
            case 3:
                spr1 = three;
                break;
            case 4:
                spr1 = four;
                break;
            case 5:
                spr1 = five;
                break;
            case 6:
                spr1 = six;
                break;
            case 7:
                spr1 = seven;
                break;
            case 8:
                spr1 = eight;
                break;
            case 9:
                spr1 = nine;
                break;
        }
        img1.sprite = spr1;
        if (img2 != null)
        {
            Sprite spr2 = null;
            switch (b)
            {
                case 0:
                    spr2 = zero;
                    break;
                case 1:
                    spr2 = one;
                    break;
                case 2:
                    spr2 = two;
                    break;
                case 3:
                    spr2 = three;
                    break;
                case 4:
                    spr2 = four;
                    break;
                case 5:
                    spr2 = five;
                    break;
                case 6:
                    spr2 = six;
                    break;
                case 7:
                    spr2 = seven;
                    break;
                case 8:
                    spr2 = eight;
                    break;
                case 9:
                    spr2 = nine;
                    break;
            }
            img2.sprite = spr2;
        }
    }
}
