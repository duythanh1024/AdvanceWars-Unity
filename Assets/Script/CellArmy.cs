using UnityEngine;
using UnityEngine.UI;
public class CellArmy : MonoBehaviour {
    Manager manager;
    [SerializeField]
    Sprite one, two, three, four, five, six, seven, eight, nine, zero;
    SpriteRenderer hp1, hp2;
    public int hp, move, gas, dame;
    public bool checkFire;
    public Manager.Type type;// = Manager.Type.none;
    [SerializeField]
    DisplayInfo displayInfo;
    public int x, y;
    private Transform posSelect;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        displayInfo = GameObject.Find("Manager").GetComponent<DisplayInfo>();
        posSelect = transform.GetChild(0).GetComponent<Transform>();
        hp1 = transform.GetChild(1).GetComponent<SpriteRenderer>();
        hp2 = transform.GetChild(2).GetComponent<SpriteRenderer>();
        one = displayInfo.one;
        two = displayInfo.two;
        three = displayInfo.three;
        four = displayInfo.four;
        five = displayInfo.five;
        six = displayInfo.six;
        seven = displayInfo.seven;
        eight = displayInfo.eight;
        nine = displayInfo.nine;
        zero = displayInfo.zero;
    }
    public void UpdatePro(int _hp, int _gas)
    {
        hp = _hp;
        gas = _gas;
        IntToSprite(hp, hp1, hp2);
    }
    public void IntToSprite(int value, SpriteRenderer img1, SpriteRenderer img2 = null)
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
    public void Set(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    void OnMouseEnter()
    {
        displayInfo.Reset();
        displayInfo.SetInfo(type, move, gas, hp, dame);
        displayInfo.SetInfo(manager.mapTerrain[x, y].type, manager.mapTerrain[x, y].def, manager.mapTerrain[x, y].capt);
        manager.DrawPath(x, y);
        manager.SetPosSelectIcon(posSelect.position.x, posSelect.position.y, checkFire);
    }
    void OnMouseDown()
    {
        manager.MoveArmyTo(x, y, type);        
        manager.OnClickArmy(this);
        manager.FireWith(this);
    }
    
    
}
