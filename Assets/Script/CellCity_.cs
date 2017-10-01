using UnityEngine.UI;
using UnityEngine;
public class CellCity_ : MonoBehaviour {
    SpriteRenderer c1, c2;
    [SerializeField]
    private int capt;
    private Manager_ manager;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        c1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        c2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
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
        c2.sprite = spr1;
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
            c1.sprite = spr1;
    }
    public Manager_.TypeArmy Capt(Manager_.OnMove oM)
    {
        if (capt == 20){
            capt = 10;
            IntToSprite();
            return Manager_.TypeArmy.none;
        }
        else
        {
            capt = 20;
            if (oM == Manager_.OnMove.Blue)
                return Manager_.TypeArmy.blueCity;
        }
        return Manager_.TypeArmy.redCity;

    }
    
}
