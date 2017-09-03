using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Text;
public class CreateMap : MonoBehaviour {
    [SerializeField]
    InputField fileName;
    [SerializeField]
    public Text notifi;
    public enum Typelayer
    {
        terrain,
        army
    }
    public enum Type
    {
        redTank,
        redInfantry,
        redMech,
        redHeadquater,

        blueTank,
        blueInfantry,
        blueMech,
        blueHeadquater,

        river,
        streetHor,
        streetVer,
        streetTopRight,
        streetTopLeft,
        streetBottomRight,
        streetBottomLeft,
        moutain,
        city,
        wood,
        grass,
        bridge,
        none
    }
   public Type currentType;
   StringBuilder builer;
    #region Var
    
    public Sprite
        redTank,
        redInfantry,
        redMech,
        redHeadquater,

        blueTank,
        blueInfantry,
        blueMech,
        blueHeadquater,

        river,
        streetHor,
        streetVer,
        streetTopRight,
        streetTopLeft,
        streetBottomRight,
        streetBottomLeft,
        moutain,
        city,
        wood,
        grass,
        bridge;
    #endregion
    //[HideInInspector]
    public int[] dataSaveMap_LayerArmy;
    public int[] dataSaveMap_LayerTerrain;
    void Start()
    {
        builer = new StringBuilder();
        dataSaveMap_LayerArmy = new int[150];
        dataSaveMap_LayerTerrain = new int[150];
        for (int i = 0; i < 150; i++)
        {
            dataSaveMap_LayerTerrain[i] = -1;
            dataSaveMap_LayerArmy[i] = -1;
        }
    }
    public void SaveMap()
    {
        string _fileName = fileName.text.ToString();
        if (_fileName == "")
        {
            notifi.color = Color.red;
            notifi.text = "no name?";
            return;
        }        
        string symbol = "!@#$%^&*()+~";
        bool ok = true;
        for (int i = 0; i < _fileName.Length; i++)
        {
            for (int j = 0; j < symbol.Length; j++)
            {
                if (symbol[j] == _fileName[i])
                {
                    ok = false;
                    notifi.color = Color.red;
                    notifi.text = "Not contain special character!";
                    break;
                }
            }
            if (!ok)
                return;
        }
        if (ok)
        {
            for (int i = 0; i < 150; i++)
            {
                builer.Append(dataSaveMap_LayerTerrain[i]);
                builer.Append(",");
            }
            for (int i = 0; i < 150; i++)
            {
                builer.Append(dataSaveMap_LayerArmy[i]);
                builer.Append(",");
            }
            builer.Remove(builer.Length-1, 1);
            print(builer.ToString());
            System.IO.File.WriteAllText(Application.dataPath + @"\" + fileName.text, builer.ToString());
            notifi.color = Color.green;
            notifi.text = "Saved in " + Application.dataPath;
            builer.Length = 0;
        }
    }
    public void OnChange()
    {
        if (notifi.text != "")
        {
            notifi.text = "";
        }
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
