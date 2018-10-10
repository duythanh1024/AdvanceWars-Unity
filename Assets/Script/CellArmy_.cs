using UnityEngine;
public class CellArmy_ : MonoBehaviour {
    public int hp;
    public int move;
    public int dame;
    public bool checkFire;
    public bool isMoved;
    public Manager_.TypeArmy type;
    private Manager_ manager;
    SpriteRenderer spr, c1, c2;
    Animation anim;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
        if (type == Manager_.TypeArmy.redInfantry || type == Manager_.TypeArmy.redMech || type == Manager_.TypeArmy.blueInfantry || type == Manager_.TypeArmy.blueMech)
        {
            spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            anim = transform.GetChild(0).GetComponent<Animation>();
            c1 = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
            c2 = transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        else
        {
            spr = GetComponent<SpriteRenderer>();
            c1 = transform.GetChild(0).GetComponent<SpriteRenderer>();
            c2 = transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        UpdateHp(10, true);
        

    }
    /// <summary>
    /// return false if die (hp = 0)
    /// </summary>
    /// <param name="newHp"></param>
    /// <returns></returns>
    public bool UpdateHp(int newHp, bool isAdd = false)
    {
        if (isAdd)
            hp = newHp;
        else
            hp = Mathf.Min(newHp, hp);
        if (hp > 10)    
            hp = 10;
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            return false;
        }
        IntToSprite();
        return true;
    }
    public int GetDame(Manager_.TypeArmy tag)
    {
        int dame = 0;
         switch (type)
        {
            case Manager_.TypeArmy.redTank:
            case Manager_.TypeArmy.blueTank:
                dame = 5;
                break;
            case Manager_.TypeArmy.redInfantry:
            case Manager_.TypeArmy.blueInfantry:
                switch (tag)
                {
                    case Manager_.TypeArmy.redTank:
                    case Manager_.TypeArmy.blueTank:
                        dame = 3;
                        break;
                    case Manager_.TypeArmy.redInfantry:
                    case Manager_.TypeArmy.blueInfantry:
                    case Manager_.TypeArmy.redMech:
                    case Manager_.TypeArmy.blueMech:
                        dame = 4;
                        break;
                }
                break;
            case Manager_.TypeArmy.redMech:
            case Manager_.TypeArmy.blueMech:
                switch (tag)
                {
                    case Manager_.TypeArmy.redTank:
                    case Manager_.TypeArmy.blueTank:
                        dame = 5;
                        break;
                    case Manager_.TypeArmy.redInfantry:
                    case Manager_.TypeArmy.blueInfantry:
                        dame = 5;
                        break;
                    case Manager_.TypeArmy.redMech:
                    case Manager_.TypeArmy.blueMech:
                        dame = 4;
                        break;
                }
                break;
        }   
        return dame*hp/10;
    }
  
    public void Capt(float sp)
    {
        anim["Capt"].speed = sp;
        anim.Play();
    }
    public void Moved()
    {
        isMoved = true;
        spr.color = new Color(.25f, .25f, .25f);
    }
    public void Reset()
    {
        isMoved = false;
        spr.color = new Color(1f, 1f, 1f);
    }
    public void IntToSprite()
    {
        int b = (hp / 10) % 10;
        int a = hp % 10;
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
}
