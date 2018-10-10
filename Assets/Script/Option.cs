using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Option : MonoBehaviour {
    [SerializeField]
    GameObject disUI;
    private UserInterface refUserInterface;
    [SerializeField]
    Toggle influenceMap;
    [SerializeField]
    GameObject labelIM;
    [SerializeField]
    Image titleDay, numDay0, numDay1, numDay2;
    [SerializeField]
    Animation animDay;
    [SerializeField]
    Sprite redMove, blueMove;
    [SerializeField]
    Image onMove;
    public GameObject btnEnd;
    UnityEngine.UI.Text txtEnd;
    public RectTransform btnFire, btnCapt, btnWait, btnCancel;
    private TypeOption typeOption;
    public enum TypeOption
    {
        wait,
        fire,
        capt,
        captAndFire
    }
    Manager_ manager;
    void Awake()
    {
        refUserInterface = GameObject.Find("CanvasMain").GetComponent<UserInterface>();
        txtEnd = btnEnd.transform.GetChild(0).GetComponent<Text>();
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
    }
    
    
    public void LableIM()
    {
        manager.LabelIM();
    }
    public void EndDay(Manager_.OnMove oM, int numDay, bool colorRed)
    {
        if (oM == Manager_.OnMove.Blue){
            onMove.sprite = blueMove;
            txtEnd.color = Color.blue;
        }
        else
        {
            onMove.sprite = redMove;
            txtEnd.color = Color.red;
        }
        if (colorRed)
        {
            numDay0.color = Color.red;
            numDay1.color = Color.red;
            numDay2.color = Color.red;
            titleDay.color = Color.red;
        }
        else
        {
            numDay0.color = Color.blue;
            numDay1.color = Color.blue;
            numDay2.color = Color.blue;
            titleDay.color = Color.blue;
        }
        DayToSprite(numDay);
        int speedAnim = refUserInterface.speedUp == 1 ? 1 : (refUserInterface.speedUp == 5 ? 5 : 10);

        animDay["Day"].speed = speedAnim;
        animDay.Play();
        //StartCoroutine(WaitEndDay());
    }
    private IEnumerator WaitEndDay()
    {
        yield return new WaitForSeconds(.5f);
        //manager.FeedBack();
    }
    public void Show(TypeOption t)
    {
        typeOption = t;
        Set();
    }
    public void _Show(bool isCapt, bool isFire)
    {
        btnEnd.gameObject.SetActive(false);
        typeOption = TypeOption.wait;
        if (isCapt && isFire)
            typeOption = TypeOption.captAndFire;
        else
            if (isCapt)
                typeOption = TypeOption.capt;
            else
                if(isFire)
                    typeOption = TypeOption.fire;
        Set();
    }
    private void Set()
    {
        btnWait.gameObject.SetActive(true);
        btnCancel.gameObject.SetActive(true);
        switch (typeOption)
        {
            case TypeOption.wait:
                btnCancel.anchoredPosition = new Vector2(-81.4f, -154.4f);
                break;
            case TypeOption.fire:
                btnFire.gameObject.SetActive(true);
                btnCancel.anchoredPosition = new Vector2(-81.4f, -204.4f);
                break;
            case TypeOption.capt:
                btnCapt.gameObject.SetActive(true);
                btnCapt.anchoredPosition = new Vector2(-81.4f, -154.4f);
                btnCancel.anchoredPosition = new Vector2(-81.4f, -204.4f);
                break;
            case TypeOption.captAndFire:
                btnCapt.gameObject.SetActive(true);
                btnCapt.anchoredPosition = new Vector2(-81.4f, -204.4f);
                btnFire.gameObject.SetActive(true);
                btnCancel.anchoredPosition = new Vector2(-81.4f, -255.4f);        
                break;
        }
    }
    public void Hide()
    {
        btnEnd.gameObject.SetActive(true);
        btnCancel.gameObject.SetActive(false);
        btnWait.gameObject.SetActive(false);
        btnFire.gameObject.SetActive(false);
        btnCapt.gameObject.SetActive(false);
    }
    public void DayToSprite(int day)
    {
        int b = (day / 10) % 10;
        int a = day % 10;
        int c = (day / 100) % 10;
        Sprite sprTemp = null;
        switch (c)
        {
            case 0:
                sprTemp = manager.zero;
                break;
            case 1:
                sprTemp = manager.one;
                break;
            case 2:
                sprTemp = manager.two;
                break;
            case 3:
                sprTemp = manager.three;
                break;
            case 4:
                sprTemp = manager.four;
                break;
            case 5:
                sprTemp = manager.five;
                break;
            case 6:
                sprTemp = manager.six;
                break;
            case 7:
                sprTemp = manager.seven;
                break;
            case 8:
                sprTemp = manager.eight;
                break;
            case 9:
                sprTemp = manager.nine;
                break;
        }
        numDay2.sprite = sprTemp;
        switch (a)
        {
            case 0:
                sprTemp = manager.zero;
                break;
            case 1:
                sprTemp = manager.one;
                break;
            case 2:
                sprTemp = manager.two;
                break;
            case 3:
                sprTemp = manager.three;
                break;
            case 4:
                sprTemp = manager.four;
                break;
            case 5:
                sprTemp = manager.five;
                break;
            case 6:
                sprTemp = manager.six;
                break;
            case 7:
                sprTemp = manager.seven;
                break;
            case 8:
                sprTemp = manager.eight;
                break;
            case 9:
                sprTemp = manager.nine;
                break;
        }
        numDay1.sprite = sprTemp;
        switch (b)
        {
            case 0:
                sprTemp = manager.zero;
                break;
            case 1:
                sprTemp = manager.one;
                break;
            case 2:
                sprTemp = manager.two;
                break;
            case 3:
                sprTemp = manager.three;
                break;
            case 4:
                sprTemp = manager.four;
                break;
            case 5:
                sprTemp = manager.five;
                break;
            case 6:
                sprTemp = manager.six;
                break;
            case 7:
                sprTemp = manager.seven;
                break;
            case 8:
                sprTemp = manager.eight;
                break;
            case 9:
                sprTemp = manager.nine;
                break;
        }
        numDay0.sprite = sprTemp;
    }
    //UI
    public void Cancel()
    {
        manager.Cancel();
    }
    public void Fire()
    {
        manager.Option_Fire();
        Hide();
    }
    public void Capt()
    {
        manager.Option_Capt();
        Hide();
    }
    public void Wait()
    {
        manager.Option_Wait();
        Hide();
    }
    public void End()
    {
        manager.Option_End();                
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "ComVsCom")
        {
            disUI.gameObject.SetActive(true);
            return;
        }
        if (manager.onMove == Manager_.OnMove.Blue)
        {
            disUI.gameObject.SetActive(true);
        }
        if (manager.onMove == Manager_.OnMove.Red)
        {
            disUI.gameObject.SetActive(false);
        }


    }
    public void Menu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    public void InfluenceMap()
    {
        if (influenceMap.isOn)
        {
            manager.InfluenceMap(true);
            labelIM.SetActive(true);
        }
        else
        {
            manager.InfluenceMap(false);
            labelIM.SetActive(false);
        }
    }
}
