using UnityEngine;
public struct Position2D{
    public int x;
    public int y;
    public Position2D(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
public class Manager_ : MonoBehaviour {
    public int numDay;
    public Sprite one, two, three, four, five, six, seven, eight, nine, zero;
    public static int savedX, savedY, currentX, currentY, newX, newY;
    public static CellArmy_ savedArmy;
    StateGame saveState;
    public LayerPath_ refLayerPath;
    public LayerArmy_ refLayerArmy;
    public LayerTerrain_ refLayerTerrain;
    public LayerIM refIM;
    public Option refOption;
    [SerializeField]
    Transform win;
    void Awake()
    {
        refIM = GameObject.Find("LayerIM").GetComponent<LayerIM>();
        refLayerTerrain = GameObject.Find("LayerTerrain").GetComponent<LayerTerrain_>();
        refLayerPath = GameObject.Find("LayerPath").GetComponent<LayerPath_>();
        refLayerArmy = GameObject.Find("LayerArmy").GetComponent<LayerArmy_>();
        refOption = GameObject.Find("Option").GetComponent<Option>();
        stateGame = StateGame.WaitSelectArmy;
        numDay = 1;
    }
    public enum OnMove
    {
        Blue,
        Red
    }
    public enum StateGame
    {
        Start,
        WaitSelectArmy,
        ArmySelected,
        WaitArmyMove,
        WaitForOption,
        WaitSelectArmyForFire,
        WaitForFiring,
        SelectedWaitOption,
        WaitForCapt,
        EndDay,
        EndGame,
        DrawIM
    }
    private OnMove onMove = OnMove.Red;
    StateGame stateGame;
    public enum TypeArmy
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
    public void Physic_OnMouseEnter(int x, int y)
    {
        currentX = x;
        currentY = y;
        switch (stateGame)
        {
            case StateGame.Start:
                break;
            case StateGame.WaitSelectArmy:
                refLayerPath.SelectArmy();
                break;
            case StateGame.WaitSelectArmyForFire:
                refLayerPath.SelectFire(refLayerArmy.IsFire(x, y));
                break;               
            case StateGame.ArmySelected:
                refLayerPath.SelectArmy();
                refLayerPath.DrawPath();
                break;
            case StateGame.WaitForOption:
                refLayerPath.SelectArmy();
                break;
            default:
                break;
        }
        
    }
    public void Physic_OnClick(int x, int y)
    {
        switch (stateGame)
        {
            case StateGame.Start:
                break;
            case StateGame.WaitSelectArmy:
                savedX = x;
                savedY = y;
                if (refLayerArmy.Select(onMove))
                {
                    refOption.ShowToogDrawIM(true);
                    refOption.btnEnd.gameObject.SetActive(false);
                    stateGame = StateGame.ArmySelected;
                }
                break;
            case StateGame.ArmySelected:
                refOption.ShowToogDrawIM(false);
                newX = x;
                newY = y;
                refLayerPath.DrawPath();
                if (refLayerArmy.CanMove())
                    stateGame = StateGame.WaitArmyMove;
                break;
            case StateGame.WaitSelectArmyForFire:
                refOption.ShowToogDrawIM(false);
                if (refLayerArmy.IsFire(currentX, currentY))
                {
                    if (refLayerArmy.Fire(x, y, refLayerTerrain.layerTerrain[x, y].def, refLayerTerrain.layerTerrain[newX, newY].def))
                        stateGame = StateGame.WaitForFiring;
                    else
                    {
                        refLayerPath.SelectFire(false);
                        stateGame = StateGame.WaitSelectArmy;
                    }
                }
                break;
            default:
                break;
        }
    }
    public void FeedBack()
    {
        switch (stateGame)
        {
            case StateGame.Start:
                break;
            case StateGame.WaitSelectArmy:
                break;
            case StateGame.ArmySelected:
                refOption.Show(refLayerTerrain.CheckCapt(onMove), refLayerArmy.CheckFire(onMove));
                stateGame = StateGame.WaitForOption;
                break;
            case StateGame.WaitArmyMove://from LayerArmy: Move
                refOption.Show(refLayerTerrain.CheckCapt(onMove), refLayerArmy.CheckFire(onMove));
                stateGame = StateGame.WaitForOption;
                break;
            case StateGame.WaitForOption://
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.WaitForCapt://from LayerArmy: Capt
                refLayerTerrain.Capt(onMove);
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.WaitSelectArmyForFire://from LayerArmy: Fire
                refLayerPath.SelectFire(false);
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.WaitForFiring:
                refLayerPath.SelectFire(false);
                stateGame = StateGame.WaitSelectArmy;
                CheckWin();
                break;
            case StateGame.SelectedWaitOption:
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.EndDay:
                stateGame = StateGame.WaitSelectArmy;
                break;
            default:
                break;
        }
    }
    public void CheckWin()
    {
        int numR = 0;
        int numB = 0;
        int reslult = refLayerArmy.IsWin(ref numR, ref numB);
        if (reslult == -1)
        {
            stateGame = StateGame.EndGame;
            win.gameObject.SetActive(true);
            win.GetComponent<UnityEngine.UI.Text>().text = "Red Win; num blue: " + numB;
        }
        if (reslult == 1)
        {
            stateGame = StateGame.EndGame;
            win.gameObject.SetActive(true);
            win.GetComponent<UnityEngine.UI.Text>().text = "Blue Win; num red " + numR;
        }
    }
    public void Cancel()
    {
        if (stateGame == StateGame.DrawIM)
            return;
        switch (stateGame)
        {
            case StateGame.ArmySelected:
                refLayerPath.ClearPathAndArea();
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.WaitForOption:
                refLayerPath.ClearPathAndArea();
                refLayerArmy.Cancel();
                stateGame = StateGame.WaitSelectArmy;
                break;
        }
        refOption.Hide();
    }
	public void Option_Fire () {
        if (stateGame == StateGame.DrawIM)
            return;
        stateGame = StateGame.WaitSelectArmyForFire;
        refLayerArmy.ConfirmPosition();
        refLayerPath.Fire();
	}
    public void Option_Wait()
    {
        if (stateGame == StateGame.DrawIM)
            return;
        refLayerArmy.ConfirmPosition();
        refLayerPath.Wait();
        refLayerArmy.Wait();
    }
    public void Option_Capt()
    {
        if (stateGame == StateGame.DrawIM)
            return;
        stateGame = StateGame.WaitForCapt;
        refLayerArmy.ConfirmPosition();
        refLayerPath.Capt();
        refLayerArmy.Capt();
    }
    public void IMRed()
    {
        refIM.ResetAllZero();
        refLayerArmy.AddValueIM_Red();
        refIM.ValueToRender();
        saveState = stateGame;
        stateGame = StateGame.DrawIM;
    }
    public void IMBlue()
    {
        refIM.ResetAllZero();
        refLayerArmy.AddValueIM_Blue();
        refIM.ValueToRender();
        saveState = stateGame;
        stateGame = StateGame.DrawIM;
    }
    public void ShowIMFromSelected(bool isShow)
    {
        Cancel();
        refIM.ResetAllZero();
        if (isShow)
        {
            refIM.gameObject.SetActive(true);
            //refLayerArmy.AddValueIM_Red(savedArmy.type);
            refLayerArmy.AddValueIM_Blue(savedArmy.type);
            refLayerTerrain.AddValueIM();
            refIM.ValueToRender();
            if (stateGame != StateGame.DrawIM)
            {
                saveState = stateGame;
                stateGame = StateGame.DrawIM;
            }
        }
        else
        {
            refIM.ResetRed();
            refIM.gameObject.SetActive(false);
            stateGame = saveState;      
        }
    }
    public void ShowIM(bool red, bool blue, bool terrain)
    {
        Cancel();
        refIM.ResetAllZero();
        if (red)//red
        {
            refIM.gameObject.SetActive(true);
            refLayerArmy.AddValueIM_Red();
            refIM.ValueToRender();
            if (stateGame != StateGame.DrawIM)
            {
                saveState = stateGame;
                stateGame = StateGame.DrawIM;
            }
            
        }
        else
        {
            refIM.ResetRed();
        }
        if (blue)//blue
        {
            refIM.gameObject.SetActive(true);
            refLayerArmy.AddValueIM_Blue();
            refIM.ValueToRender();
            if (stateGame != StateGame.DrawIM)
            {
                saveState = stateGame;
                stateGame = StateGame.DrawIM;
            }
        }
        else
        {
            refIM.ResetBlue();
        }
        if (terrain)
        {
            refIM.gameObject.SetActive(true);
            refLayerTerrain.AddValueIM();
            refIM.ValueToRender();
            if (stateGame != StateGame.DrawIM)
            {
                saveState = stateGame;
                stateGame = StateGame.DrawIM;
            }
        }
        else
        {
            refIM.ResetTerrain();
        }
        if (!red && !blue && !terrain)// uncheck all
        {
            refIM.gameObject.SetActive(false);
            stateGame = saveState;      
        }

    }
    public void LabelIM()
    {
        refIM.LableIM();
    }
    public void Option_End()
    {
        if (stateGame == StateGame.DrawIM)
            return;
        if (stateGame != StateGame.EndDay)
        {
            stateGame = StateGame.EndDay;
            TypeArmy t;//add 1 hp every day
            for (int c = 0; c < 10; c++)//check array army, if it on its city-> add
            {
                for (int r = 0; r < 15; r++)
                {
                    if (!refLayerArmy.IsNull(r, c))
                    {
                        t = refLayerArmy.GetType(r, c);
                        if (t == TypeArmy.redInfantry || t == TypeArmy.redMech || t == TypeArmy.redTank)
                        {
                            if (refLayerTerrain.GetType(r, c) == TypeArmy.redCity)
                            {
                                refLayerArmy.AddHp(r, c, 1);
                            }
                        }
                        else
                        {
                            if (t == TypeArmy.blueInfantry || t == TypeArmy.blueMech || t == TypeArmy.blueTank)
                            {
                                if (refLayerTerrain.GetType(r, c) == TypeArmy.blueCity)
                                {
                                    refLayerArmy.AddHp(r, c, 1);
                                }
                            }
                        }
                    }
                }
            }
            bool colorRed;
            if (onMove == OnMove.Blue){
                onMove = OnMove.Red;
                refLayerArmy.b_Moved = true;
                colorRed = true;
            }
            else
            {
                onMove = OnMove.Blue;
                refLayerArmy.r_Moved = true;
                colorRed = false;
            }
            Cancel();
            refLayerArmy.Reset();
            if (refLayerArmy.r_Moved && refLayerArmy.b_Moved)
            {
                refLayerArmy.r_Moved = false;
                refLayerArmy.b_Moved = false;
                numDay++;
            }
           
            refOption.EndDay(onMove, numDay, colorRed);
        }
    }
}
