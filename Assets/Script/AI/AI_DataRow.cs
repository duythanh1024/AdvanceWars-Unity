using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[SerializeField]
public class AI_Row
{
    public float[] influenceMap;//5x5 cell
    public float hp;
    public int posStart;//position: from
    public int posEnd;//position: to 
    public bool moveToHere;//should move to here? -> out: expected
    public Manager_.TypeArmy type;
    public AI_Row(Manager_.TypeArmy _type, float[] _influenceMap, float _hp, int _posEnd, int _posStart, bool moveHere)
    {
        this.hp = _hp;
        this.influenceMap = _influenceMap;
        this.posStart = _posStart;
        this.posEnd = _posEnd;
        this.moveToHere = moveHere; 
        this.type = _type;
    }
}
public class AI_DataRow:MonoBehaviour
{

    private List<AI_Row> rowsRed, rowsBlue;
    public  void CreateNew()
    {
        rowsRed = new List<AI_Row>();
        rowsBlue = new List<AI_Row>();
    }

    public void AddDataRow(bool isRed, Manager_.TypeArmy _type, float[] _influenceMap, float _hp, int _posEnd, int _posStart, bool moveHere)
    {
        AI_Row newRow = new AI_Row(_type, _influenceMap, _hp, _posEnd, _posStart, moveHere);
        //print("add");
        if (isRed)
        {
            rowsRed.Add(newRow);
        }
        else
        {
            rowsBlue.Add(newRow);
        }
        //print("add");
    }
    public void SaveToDiskBinary(bool isRed)
    {
        string path = "";
        List<AI_Row> listSave = new List<AI_Row>();
        if (isRed){
            listSave = rowsRed;
            rowsBlue.Clear();
        }
        else{
            listSave = rowsBlue;
            rowsRed.Clear();
        }
        //phan loai cac army
        for (int i = 0; i < listSave.Count; i++)
        {
            switch (listSave[i].type)
            {
                case Manager_.TypeArmy.redTank:
                case Manager_.TypeArmy.blueTank:
                    path = @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Tank\data.txt";
                    break;
                case Manager_.TypeArmy.redMech:
                case Manager_.TypeArmy.blueMech:
                    path = @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Mech\data.txt";
                    break;
                case Manager_.TypeArmy.redInfantry:
                case Manager_.TypeArmy.blueInfantry:
                    path = @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\data.txt";
                    break;
            }
        }
        
        FileStream fs = File.Create(path);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, this);
        fs.Close();
        //File.WriteAllBytes("", (byte)this);
    }
    public void LoadDataBinary(string path)
    {
        FileStream fs = File.OpenRead(path);
        BinaryFormatter bf = new BinaryFormatter();
        AI_DataRow newData = (AI_DataRow)bf.Deserialize(fs);
        fs.Close();
    }
    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("com vs com");
    }
    public void SaveToDisk(bool isRed)
    {
        //print("save");
        //List<AI_Row> listWin = new List<AI_Row>();
        //List<AI_Row> listSaveTank = new List<AI_Row>();
        //List<AI_Row> listSaveMech = new List<AI_Row>();
        //List<AI_Row> listSaveInfantry = new List<AI_Row>();
        int idFileName = 1 + System.IO.Directory.GetFiles(@"G:\merged_partition_content\Project\Unity\TieuLuan01\DataTraning").Length;
        if (isRed)
        {
            WriteFile(rowsRed, @"G:\merged_partition_content\Project\Unity\TieuLuan01\DataTraning\" + idFileName.ToString() + "dataTraning.txt");
        }
        else
        {
            WriteFile(rowsBlue, @"G:\merged_partition_content\Project\Unity\TieuLuan01\DataTraning\" + idFileName.ToString() + "dataTraning.txt");
        }
        /*//phan loai cac army: tank | mech | infantry
        for (int i = 0; i < listWin.Count; i++)
        {
            switch (listWin[i].type)
            {
                case Manager_.TypeArmy.redTank:
                case Manager_.TypeArmy.blueTank:
                    listSaveTank.Add(listWin[i]);
                    //path = @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Tank\data.txt";
                    break;
                case Manager_.TypeArmy.redMech:
                case Manager_.TypeArmy.blueMech:
                    listSaveMech.Add(listWin[i]);
                    //path = @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Mech\data.txt";
                    break;
                case Manager_.TypeArmy.redInfantry:
                case Manager_.TypeArmy.blueInfantry:
                    listSaveInfantry.Add(listWin[i]);
                    //path = @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\data.txt";
                    break;
            }
        }*/
        //WriteFile(listWin, @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\dataTraning.txt");
        //WriteFile(listSaveMech, @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Mech\data.txt");
        //WriteFile(listSaveTank, @"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Tank\data.txt");
        rowsBlue.Clear();
        rowsRed.Clear();
        StartCoroutine(ReloadScene());
        
    }
    private void WriteFile(List<AI_Row> list, string path)
    {
        //print("a== " + list.Count);
        StringBuilder builder = new StringBuilder();
        int t = list.Count;
        //if (t > 1000)
        //{
        //    t = 1000;
        //}
        for (int j = 0; j < t; j++)
        {
            for (int i = 0; i < 25; i++)
            {
                builder.Append(list[j].influenceMap[i]);
                builder.Append(",");
            }
            builder.Append(list[j].hp);
            builder.Append(",");
            builder.Append(list[j].posStart);
            builder.Append(",");
            builder.Append(list[j].posEnd);
            builder.Append(",");
            switch (list[j].type)
            {
                case Manager_.TypeArmy.redTank:
                case Manager_.TypeArmy.blueTank:
                    builder.Append(0);
                    break;
                case Manager_.TypeArmy.redInfantry:
                case Manager_.TypeArmy.blueInfantry:
                    builder.Append(1);
                    break;
                case Manager_.TypeArmy.redMech:
                case Manager_.TypeArmy.blueMech:
                    builder.Append(2);
                    break;
            }
            builder.Append(",");
            if (list[j].moveToHere)
                builder.Append(1);                
            else
                builder.Append(0);
            builder.AppendLine();
            //print(builder.ToString());
        }
        File.WriteAllText(path, builder.ToString());

    }
    void Update()
    {
        //if (Input.GetKeyDown("t"))
        //{

        //    SaveToDisk(true);
            
        //}
    }
}
