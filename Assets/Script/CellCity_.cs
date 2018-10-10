using UnityEngine.UI;
using UnityEngine;
public class CellCity_ : MonoBehaviour {
    SpriteRenderer sprNumCap1, sprNumCap2, spriMain;
    [SerializeField]
    private int capt;
    private Manager_ manager;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        sprNumCap1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprNumCap2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
        spriMain = transform.GetChild(3).GetComponent<SpriteRenderer>();
    }
    public void IntToSprite()
    {
        int b = (capt / 10) % 10;
        int a = capt % 10;
        Sprite spr1 = null;
        switch (a)
        {
            case 0:
                spr1 = manager.zero;
                break;
            case 1:
                spr1 = manager.one;
                break;
            case 2:
                spr1 = manager.two;
                break;
            case 3:
                spr1 = manager.three;
                break;
            case 4:
                spr1 = manager.four;
                break;
            case 5:
                spr1 = manager.five;
                break;
            case 6:
                spr1 = manager.six;
                break;
            case 7:
                spr1 = manager.seven;
                break;
            case 8:
                spr1 = manager.eight;
                break;
            case 9:
                spr1 = manager.nine;
                break;
        }
        sprNumCap2.sprite = spr1;
            switch (b)
            {
                case 0:
                    spr1 = manager.zero;
                    break;
                case 1:
                    spr1 = manager.one;
                    break;
                case 2:
                    spr1 = manager.two;
                    break;
                case 3:
                    spr1 = manager.three;
                    break;
                case 4:
                    spr1 = manager.four;
                    break;
                case 5:
                    spr1 = manager.five;
                    break;
                case 6:
                    spr1 = manager.six;
                    break;
                case 7:
                    spr1 = manager.seven;
                    break;
                case 8:
                    spr1 = manager.eight;
                    break;
                case 9:
                    spr1 = manager.nine;
                    break;
            }
            sprNumCap1.sprite = spr1;
    }
    public void CaptHQ(Manager_.OnMove oM)
    {
        if (capt == 20)
        {//20
            capt = 10;
            IntToSprite();
        }
        else//10
        {
            if (oM == Manager_.OnMove.Blue)
            {
                manager.BlueWin();
            }
            else
            {
                manager.RedWin();
            }
        }
    }
    public void CaptCity(Manager_.OnMove oM)
    {
        if (capt == 20){//20
            capt = 10;
            IntToSprite();
            spriMain.sprite = manager.sprNoneCity;
            GetComponent<CellTerrain_>().type = Manager_.TypeArmy.noneCity;
        }
        else//10
        {
            capt = 20;
            IntToSprite();
            if (oM == Manager_.OnMove.Blue)
            {
                spriMain.sprite = manager.sprBlueCity;
                GetComponent<CellTerrain_>().type = Manager_.TypeArmy.blueCity;

            }
            else
            {
                spriMain.sprite = manager.sprRedCity;
                GetComponent<CellTerrain_>().type = Manager_.TypeArmy.redCity;
            }
        }
    }
}
