using UnityEngine;
public class CellArmy : MonoBehaviour {
    Manager manager;
    [SerializeField]
    Sprite one, two, three, four, five, six, seven, eight, nine, zero;
    public int hp, move, gas, dame;
    public Manager.Type type = Manager.Type.none;
    [SerializeField]
    SpriteRenderer hp1, hp2;
    DisplayInfo displayInfo;
    [SerializeField]
    int x, y;
    private Transform posSelect;
    private Transform select;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        displayInfo = GameObject.Find("Manager").GetComponent<DisplayInfo>();
        select = GameObject.Find("Select").transform;
        posSelect = transform.GetChild(0).GetComponent<Transform>();
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
    public void IntToSprite(Sprite spr1, Sprite spr2 = null)
    {
        int a = hp % 10;
        int b = a % 10;
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
        if (spr2!=null)
        {
            switch (a)
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
        }
    }
    public void Set(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
    void OnMouseEnter()
    {
        select.transform.position = posSelect.position;
        displayInfo.Reset();
        displayInfo.SetInfo(type, move, gas, hp, dame);
        displayInfo.SetInfo(manager.mapTerrain[x, y].type, manager.mapTerrain[x, y].def, manager.mapTerrain[x, y].capt);
    }
}
