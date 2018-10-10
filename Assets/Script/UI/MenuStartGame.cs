using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuStartGame : MonoBehaviour {
    float t1, t2;
    public Text notifi;
    public GameObject gonotifi;
    void Awake()
    {
        Screen.SetResolution(1091, 595, false);
    }
    void Start()
    {
        if (ConnectPython.notSuccess)
        {
            notifi.text = "Failed to connect to server (python)! \n Please run with path:" + Application.dataPath + "/Python/ServerPy.py";
            gonotifi.gameObject.SetActive(true);
        }
    }
    public void ComVsCom()
    {
        SceneManager.LoadScene("ComVsCom");
    }
    public void PlayerVsCom()
    {
        SceneManager.LoadScene("PlayerVsCom");

    }
    public void OpenFile()
    {
        if (t1<Time.time)
        {
            System.Diagnostics.Process.Start(Application.streamingAssetsPath + @"\BaoCao.docx");
            t1 = Time.time + 1;
        }
    }
    public void Source()
    {
        if (t2 < Time.time)
        {
            Application.OpenURL("https://stackoverflow.com/questions/1283584/how-do-i-launch-files-in-c-sharp");
            t2 = Time.time + 1;
        }
    }
	
}
