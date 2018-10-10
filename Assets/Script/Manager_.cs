using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool endGame;
    public int numDay;
    public Sprite one, two, three, four, five, six, seven, eight, nine, zero;
    public Sprite sprRedCity, sprBlueCity, sprNoneCity;
    public static int selectedX, selectedY, cursorX, cursorY, newX, newY;
    public static CellArmy_ savedArmy;
    StateGame saveState;
    public LayerPath_ refLayerPath;
    public LayerArmy_ refLayerArmy;
    public LayerTerrain_ refLayerTerrain;
    public LayerIM refIM;
    public Option refOption;
    //UI
    UserInterface refUserInterface;

    [SerializeField]
    Transform win;
    //AI
    AI_DecisionMaking refAI_DecisionBlue, refAI_DecisionRed;
    AI_DataRow refDataRows;
    AI_Training refTraning;
    //prefabs
    [SerializeField]
    private GameObject layerArmy, layerTerrain, ai;
    private void Awake()
    {
        LoadGame();
    }
    private void LoadGame()
    {
        GameObject t = null;
        refIM.gameObject.SetActive(true);   
        win.gameObject.SetActive(false);

        t = Instantiate(ai);
        t.name = "AI";


        t = Instantiate(layerArmy);
        t.name = "LayerArmy";
        t.GetComponent<LayerArmy_>().MyAwake();

        t = Instantiate(layerTerrain);
        t.name = "LayerTerrain";
        t.GetComponent<LayerTerrain_>().MyAwake();


        MyAwake();
        refTraning.MyAwake();
        refLayerPath.MyAwake();
        refAI_DecisionBlue.MyAwake();
        refAI_DecisionRed.MyAwake();
        refUserInterface.MyAwake();
        refIM.MyAwake();
        GetComponent<CreateMap_>().StartLoadPrefabs();
    }
    private void DestroyGame()
    {
        Destroy(refLayerArmy.gameObject);
        //Destroy(refLayerPath.gameObject);
        Destroy(refLayerTerrain.gameObject);
        Destroy(GameObject.Find("AI").gameObject);
    }
    private void MyAwake()
    {
        refUserInterface = GameObject.Find("CanvasMain").GetComponent<UserInterface>();
        refLayerTerrain = GameObject.Find("LayerTerrain").GetComponent<LayerTerrain_>();
        refLayerPath = GameObject.Find("LayerPath").GetComponent<LayerPath_>();
        refLayerArmy = GameObject.Find("LayerArmy").GetComponent<LayerArmy_>();
        refOption = GameObject.Find("Option").GetComponent<Option>();
        refDataRows = GameObject.Find("AI_Traning").GetComponent<AI_DataRow>();
        refTraning = GameObject.Find("AI_Traning").GetComponent<AI_Training>();
        refAI_DecisionBlue = GameObject.Find("AI_Blue").GetComponent<AI_DecisionMaking>();
        refAI_DecisionRed = GameObject.Find("AI_Red").GetComponent<AI_DecisionMaking>();

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
    public OnMove onMove = OnMove.Red;
    [SerializeField]
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
        noneCity,
        wood,
        grass,
        bridge,
    }
    public void Physic_OnMouseEnter(int x, int y)//mouse enter on cell of the map
    {
        //print("py");
        cursorX = x;
        cursorY = y;
        switch (stateGame)
        {
            case StateGame.Start:
                break;
            case StateGame.WaitSelectArmy:
                refLayerPath.SetIconSelectArmy(Manager_.cursorX, Manager_.cursorY);
                break;
            case StateGame.WaitSelectArmyForFire:
                refLayerPath.SelectFire(refLayerArmy.IsFire(x, y));
                break;               
            case StateGame.ArmySelected:
                refLayerPath.SetIconSelectArmy(Manager_.cursorX, Manager_.cursorY);
                refLayerPath.DrawPath();
                break;
            case StateGame.WaitForOption:
                refLayerPath.SetIconSelectArmy(Manager_.cursorX, Manager_.cursorY);
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
                selectedX = x;
                selectedY = y;
                if (refLayerArmy.Select(onMove))//select and draw area can move
                {
                    //refOption.ShowToogDrawIM(true);
                    refLayerPath.DrawArea();
                    refOption.btnEnd.gameObject.SetActive(false);
                    stateGame = StateGame.ArmySelected;
                }
                break;
            case StateGame.ArmySelected:
                //refOption.ShowToogDrawIM(false);
                newX = x;
                newY = y;
                Physic_OnMouseEnter(x, y);
                //refLayerPath.DrawPath();
                if (x == selectedX && y == selectedY)
                {
                    
                }
                if (refLayerPath.IsCheckHere(x, y) && (refLayerArmy.IsNull(Manager_.cursorX, Manager_.cursorY) || (x == selectedX && y == selectedY)))
                {
                    //if (refLayerArmy.CanMove(refUserInterface.isPlayAnim.isOn))
                    stateGame = StateGame.WaitArmyMove;
                    refLayerArmy.AnimMove();
                }
                
                break;
            case StateGame.WaitSelectArmyForFire:
                //refOption.ShowToogDrawIM(false);
                if (refLayerArmy.IsFire(cursorX, cursorY))
                {
                    if (refLayerArmy.Fire(x, y, refLayerTerrain.layerTerrain[x, y].def, refLayerTerrain.layerTerrain[newX, newY].def))//is die
                        stateGame = StateGame.WaitForFiring;
                    else
                    {
                        refLayerPath.SelectFire(false);
                        stateGame = StateGame.WaitSelectArmy;
                        //if (refUserInterface.comVsCom.isOn)
                        //{
                            refAI_DecisionBlue.FinishAmim(); 
                            refAI_DecisionRed.FinishAmim();
                        //}
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
                //if (refUserInterface.comVsCom.isOn)
                //{
                    refAI_DecisionBlue.FinishAmim();
                    refAI_DecisionRed.FinishAmim();
                //}
                break;
            case StateGame.ArmySelected:
                //refOption.Show(refLayerTerrain.CheckCapt(onMove), refLayerArmy.CheckFire(onMove));
                stateGame = StateGame.WaitForOption;
                break;
            case StateGame.WaitArmyMove://from LayerArmy: Move
                Option.TypeOption t = Option.TypeOption.wait;
                bool isCap = refLayerTerrain.CheckCapt(onMove);
                bool isFire = refLayerArmy.CheckFire(onMove);
                if (isCap && isFire)
                    t = Option.TypeOption.captAndFire;
                else
                    if (isCap)
                        t = Option.TypeOption.capt;
                    else
                        if (isFire)
                            t = Option.TypeOption.fire;
                refOption.Show(t);
                //print(t);
                //if (refUserInterface.comVsCom.isOn)
                //{
                    refAI_DecisionBlue.SetChoiceOption(t);
                    refAI_DecisionRed.SetChoiceOption(t);
                    refAI_DecisionBlue.FinishAmim();
                    refAI_DecisionRed.FinishAmim();
                //}
                //if (refUserInterface.pVsCom.isOn)
                //{
                //    refAI_DecisionBlue.SetChoiceOption(t);
                //}
                
                stateGame = StateGame.WaitForOption;
                break;
            case StateGame.WaitForOption://
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.WaitForCapt://from LayerArmy: Capt
                //print("caped");
                refLayerTerrain.Capt(onMove);
                //if (refUserInterface.comVsCom.isOn)
                //{
                    refAI_DecisionBlue.FinishAmim();
                    refAI_DecisionRed.FinishAmim();
                //}
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.WaitSelectArmyForFire://from LayerArmy: Fire
                refLayerPath.SelectFire(false);
                //if (refUserInterface.comVsCom.isOn)
                //{
                    refAI_DecisionBlue.FinishAmim();
                    refAI_DecisionRed.FinishAmim();
                //}
                stateGame = StateGame.WaitSelectArmy;
                break;
            case StateGame.WaitForFiring:
                refLayerPath.SelectFire(false);
                //if (refUserInterface.comVsCom.isOn)
                //{
                    refAI_DecisionBlue.FinishAmim();
                    refAI_DecisionRed.FinishAmim();
                //}
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
    public void BlueWin()
    {
        int numR = 0;
        int numB = 0;
        int reslult = refLayerArmy.IsWin(ref numR, ref numB);
        stateGame = StateGame.EndGame;
        win.gameObject.SetActive(true);
        refAI_DecisionBlue.StopAllCoroutines();
        refAI_DecisionRed.StopAllCoroutines();
        win.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Blue Win; num blue " + numB;
        if (refUserInterface.recordeStep.isOn)
            refDataRows.SaveToDisk(true);
    }
    public void RedWin()
    {
        int numR = 0;
        int numB = 0;
        int reslult = refLayerArmy.IsWin(ref numR, ref numB);
        stateGame = StateGame.EndGame;
        win.gameObject.SetActive(true);
        refAI_DecisionBlue.StopAllCoroutines();
        refAI_DecisionRed.StopAllCoroutines();
        win.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Red Win; num red: " + numR;
        if (refUserInterface.recordeStep.isOn)
            refDataRows.SaveToDisk(false);
    }
    public void CheckWin()
    {
        int numR = 0;
        int numB = 0;
        int result = refLayerArmy.IsWin(ref numR, ref numB);
        if (result == -1)
        {
            refAI_DecisionBlue.StopAllCoroutines();
            refAI_DecisionRed.StopAllCoroutines();
            stateGame = StateGame.EndGame;
            win.gameObject.SetActive(true);
            win.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Red Win; num red: " + numR;
            if (refUserInterface.recordeStep.isOn)
                refDataRows.SaveToDisk(true);
            return;
        }
        if (result == 1)
        {
            refAI_DecisionBlue.StopAllCoroutines();
            refAI_DecisionRed.StopAllCoroutines();
            stateGame = StateGame.EndGame;
            win.gameObject.SetActive(true);
            win.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = "Blue Win; num blue " + numB;
            if (refUserInterface.recordeStep.isOn)
                refDataRows.SaveToDisk(false);
        }
    }

    //UI
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
    
   
    public void LabelIM()
    {
        refIM.LableIM();
    }
    public void SetInluenceMapForWriteFile2()
    {
        //Cancel();
        refIM.ResetAllZero();//reset
        refLayerArmy.AddValueToInluenceMap2(refLayerArmy.GetType(selectedX, selectedY));
        refLayerTerrain.AddValueIM();
        refIM.CombineRedBlueTerr();
        
    }
    public void SetInluenceMapForWriteFile()
    {
        Cancel();
        refIM.ResetAllZero();//reset
        refLayerArmy.AddValueToInluenceMap(refLayerArmy.GetType(selectedX, selectedY));
        refLayerTerrain.AddValueIM();
        refIM.CombineRedBlueTerr();
        //refIM.Normalize();
        //refIM.Nagetive(onMove);
    }
    //call from UI togg
    public void InfluenceMap(bool isShow)
    {
        Cancel();
        refIM.ResetAllZero();//reset
        if (isShow)
        {
            refIM.gameObject.SetActive(true);
            refLayerArmy.AddValueIM_Blue();
            refLayerArmy.AddValueIM_Red();
            //refLayerArmy.Nagative();
            refLayerTerrain.AddValueIM();
            //refLayerArmy.AddValueToInluenceMap(refLayerArmy.GetType(newX, newY));
            refIM.Normalize();
            refIM.ShowInluenceMap();
            refIM.ValueToRender();
            //refIM.Nagetive(onMove);
            saveState = stateGame;
            stateGame = StateGame.DrawIM;
        }
        else
        {
            refIM.gameObject.SetActive(false);
            stateGame = saveState;  
        }
    }
    public void Option_End()
    {
        if (stateGame == StateGame.DrawIM)
            return;
        if (stateGame != StateGame.EndDay)
        {
            //stateGame = StateGame.EndDay;
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
            
            //UI
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
            //AI
            if (refUserInterface.comVsCom.isOn)
            {
                if (onMove == OnMove.Blue)
                {
                    refAI_DecisionBlue.StartAutoPlay();// print("~~");
                }
                else
                {
                    refAI_DecisionRed.StartAutoPlay();
                }
            }
            if (refUserInterface.pVsCom.isOn)
            {
                if (onMove == OnMove.Blue)
                    refAI_DecisionBlue.StartAutoPlay();// print("~~");                    
            }
        }
    }
    //void Update()
    //{
    //    if (Input.GetKeyDown("1"))
    //    {
    //        DestroyGame();
    //    }
    //    if (Input.GetKeyDown("2"))
    //    {
    //        LoadGame();
    //    }
    //    //if (Input.GetKeyDown("t"))
    //    //{
    //    //    refTraning.SetInputFromFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\data.txt");
    //    //    refTraning.StartTraining();
    //    //    refTraning.SaveWeightToFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\weight.dat");
    //    //}
    //    //print(System.DateTime.Today.Ticks);
    //    //print(System.IO.Directory.GetFiles(@"G:\merged_partition_content\Project\Unity\TieuLuan01\DataTraning").Length);
    //}
}
