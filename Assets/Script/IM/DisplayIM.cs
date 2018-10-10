using UnityEngine;
public class DisplayIM : MonoBehaviour {
    Manager_ manager;
    SpriteRenderer value1, value2;
    SpriteRenderer mainColor;
    int valueR, valueB, terrain;
    int valueCell;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        mainColor = GetComponent<SpriteRenderer>();
        value1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
        value2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
        valueB = 0;
        valueR = 0;
        //Show();
    }
    public void Reset()
    {
        valueB = 0;
        valueR = 0;
        mainColor.color = new Color(0f, 0f, 0f);
    }
    public void SetDisplay(int valueCell, float colorR, float colorTerrain, float colorB)
    {
        mainColor.color = new Color(colorR, colorTerrain, colorB);
        IntToSprite(valueCell);
    }
    //public void SetValue(int _valueR, int _valueB, int _terrain)
    //{
    //    valueR = _valueR; print(valueR);
    //    valueB = _valueB;
    //    terrain = _terrain;
    //    //int s = valueR + valueB + terrain;
    //    //print("all " + s);
    //    valueCell = (int)(10*Mathf.InverseLerp(0, 13, (valueR + valueB + terrain))); //print("value cell " + valueCell);
    //    float colorValueR = Mathf.InverseLerp(0, 10, valueR); 
    //    float colorValueB = Mathf.InverseLerp(0, 10, valueB);
    //    float colorTerrain = Mathf.InverseLerp(0, 10, terrain);
    //    mainColor.color = new Color(colorValueR, colorTerrain, colorValueB);
    //    //if (_valueR == 0)
    //    //    IntToSprite(false);
    //    //else
    //    //    IntToSprite();
    //    IntToSprite();

    //}
    //public void Show()
    //{
    //    float colorValueR = Mathf.InverseLerp(0, 10, valueR);
    //    float colorValueB = Mathf.InverseLerp(0, 10, valueB);
    //    mainColor.color = new Color(colorValueR, 0f, colorValueB);
    //    IntToSprite();
    //}
    public void ShowLabel()
    {
        if (value1.enabled)
        {
            value1.enabled = false;
            value2.enabled = false;
        }
        else
        {
            value1.enabled = true;
            value2.enabled = true;
        }

    }
    public void IntToSprite(/*bool isRed = true*/ int value)
    {
        //int b = 0;
        //int a = 0;
        //if (isRed)
        //{
        //    b = (valueR / 10) % 10;
        //    a = valueR % 10;
        //}
        //else
        //{
        //    b = (valueB / 10) % 10;
        //    a = valueB % 10;
        //}
        if (value > 99)
        {
            value = 99;
        }
        int a = (value / 10) % 10;
        int b = value % 10;
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
        value2.sprite = spr1;
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
        value1.sprite = spr1;
    }
}
