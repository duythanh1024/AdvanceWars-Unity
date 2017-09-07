using UnityEngine;
public class Option : MonoBehaviour {
    public RectTransform btnFire, btnCapt, btnWait, btnCancel;
    public enum TypeOption
    {
        wait,
        fire,
        capt,
        captAndFire
    }
    Manager manager;
    void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }
    public void Show(TypeOption t)
    {
        btnWait.gameObject.SetActive(true);
        btnCancel.gameObject.SetActive(true);
        switch (t)
        {
            case TypeOption.wait:
                btnCancel.anchoredPosition = new Vector2(75f, -14f);
                break;
            case TypeOption.fire:
                btnFire.gameObject.SetActive(true);
                btnCancel.anchoredPosition = new Vector2(75f, -64f);
                break;
            case TypeOption.capt:
                btnCapt.gameObject.SetActive(true);
                btnCapt.anchoredPosition = new Vector2(75f, -14f);
                btnCancel.anchoredPosition = new Vector2(75f, -64f);
                break;
            case TypeOption.captAndFire:
                btnCapt.gameObject.SetActive(true);
                btnCapt.anchoredPosition = new Vector2(75f, -64f);
                btnFire.gameObject.SetActive(true);
                btnCancel.anchoredPosition = new Vector2(75f, -14f);        
                break;
        }
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        btnCancel.gameObject.SetActive(false);
        btnWait.gameObject.SetActive(false);
        btnFire.gameObject.SetActive(false);
        btnCapt.gameObject.SetActive(false);
    }
    public void Cancel()
    {
        manager.Cancel();
        gameObject.SetActive(false);
        Hide();
    }
    public void Fire()
    {
        manager.Fire();
        Hide();
    }
    public void Capt()
    {
        manager.Capt();
        Hide();
    }
    public void Wait()
    {
        manager.Wait();
        Hide();
    }
}
