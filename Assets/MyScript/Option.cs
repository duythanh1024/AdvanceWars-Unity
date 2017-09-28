using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Option : MonoBehaviour {
    [SerializeField]
    Toggle toggDrawIMFromSelected, toggBlue, toggRed, toggTer;
    [SerializeField]
    GameObject labelIM;
    [SerializeField]
    Image titleDay, numDay0, numDay1;
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
        txtEnd = btnEnd.transform.GetChild(0).GetComponent<Text>();
        manager = GameObject.Find("Manager").GetComponent<Manager_>();
    }
    
    public void ShowToogDrawIM(bool isShow)
    {
        toggDrawIMFromSelected.gameObject.SetActive(isShow);
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
            titleDay.color = Color.red;
        }
        else
        {
            numDay0.color = Color.blue;
            numDay1.color = Color.blue;
            titleDay.color = Color.blue;
        }
        DayToSprite(numDay);
        animDay.Play();
        StartCoroutine(WaitEndDay());
    }
    private IEnumerator WaitEndDay()
    {
        yield return new WaitForSeconds(1f);
        manager.FeedBack();
    }
    public void Show(bool isCapt, bool isFire)
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
        numDay1.sprite = spr1;
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
        numDay0.sprite = spr1;
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
    }
    public void ShowIMRedABleTerr()
    {
        manager.ShowIM(toggRed.isOn, toggBlue.isOn, toggTer.isOn);
    }
    public void ShowImFromSelected()
    {
        manager.ShowIMFromSelected(toggDrawIMFromSelected.isOn);
        if (toggDrawIMFromSelected.isOn)
        {
            toggRed.isOn = false;
            toggBlue.isOn = false;
            toggTer.isOn = false;
        }
    }
    
}
