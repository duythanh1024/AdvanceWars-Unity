using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
[System.Serializable]
public class SaveWeights
{
    public List<float[,]> listWeight;
    public SaveWeights(List<float[,]> listWeight)
    {
        this.listWeight = listWeight;
    }
}
public class AI_Training : MonoBehaviour {
    private LayerIM refLayerIM;
    Manager_ refManager;

    NeuralNetwork net;
    List<float[]> input;
    List<float> expected;
    int numRowsData;
    public string path;

    //test traning for Infantry
    AI_DataRow refDataRows;
    
    //public void Set(float[] _input, float[] _output)
    //{
    //    input = _input;
    //    expected = _output;
    //}
    public void MyAwake()
    {
        refDataRows = GameObject.Find("AI").GetComponent<AI_DataRow>();
        refManager = GameObject.Find("Manager").GetComponent<Manager_>();
        refLayerIM = GameObject.Find("LayerIM").GetComponent<LayerIM>();
    }
    public float[] SetInputRealTime()
    {
        float[] input = new float[152];
        for (int i = 0; i < 150; i++)
        {
            input[i] = refLayerIM.value[i % 15, i / 15];
        }
        input[150] = Manager_.savedArmy.hp;
        input[151] = 15 * Manager_.selectedX + Manager_.selectedY;
        //for (int i = 0; i < 151; i++)
        //{
        //    print(input[i]);
        //}
        return input;
    }
    public void SetInputFromFile(string path)
    {
        this.path = path;
        input = new  List<float[]>();
        expected = new List<float>();
        string[] inputTxt = File.ReadAllLines(path);
        numRowsData = inputTxt.Length;// print(numRowsData);
        for (int eachRow = 0; eachRow < numRowsData; eachRow++)
        {
            string[] inputSplit = inputTxt[eachRow].Split(','); //print(inputSplit.Length);

            float[] t = new float[29];
            for (int i = 0; i < 29; i++)//influence map + hp +star pos = 29 input ; 1 output
            {
                t[i] = float.Parse(inputSplit[i]);
            }
            input.Add(t);
            expected.Add(int.Parse(inputSplit[29]));
        }
        //for (int i = 0; i < 29; i++)
        //{
        //    print(input[0][i]);
        //}
        print("~");
    }
    public void StartTraining()
    {
        net = new NeuralNetwork(new int[] { 29, 10, 10, 10, 1 });
                //net.FeedForward(input[0]);
                //net.BackProp(new float[] { expected[0] });
        for (int i = 0; i < 50000; i++)
        {
            for (int eachRow = 0; eachRow < numRowsData; eachRow++)
            {
                net.FeedForward(input[0]);
                net.BackProp(new float[] { expected[0] });
            }
        }
        print(Out(net.FeedForward(input[6])[0]) + " test"); 
    }
    public void TestOut(string pathInputFile)
    {
        SetInputFromFile(pathInputFile);
        net = new NeuralNetwork(new int[] { 152, 20, 20, 20, 150 });
        for (int i = 0; i < 150; i++)
        {
            print(Out(net.FeedForward(input[0])[i]) + " " + i);
            //print(net.FeedForward(input)[i]);
        }
    }
    //save weights to file
    public void SaveWeightToFile(string path)
    {
        FileStream fs = File.Open(path, FileMode.OpenOrCreate);
        //if (File.Exists(path)) 
        //    fs = File.OpenWrite(path);
        //else
        //    fs = File.Create(path);
        SaveWeights sw = new SaveWeights(net.GetWeights());
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sw);
        fs.Close();
    }
    public List<float[,]> LoadWeightFromFile(string path)
    {
        FileStream fs = File.OpenRead(path);
        BinaryFormatter bf = new BinaryFormatter();
        SaveWeights sw = (SaveWeights)bf.Deserialize(fs);
        fs.Close();
        return sw.listWeight;
    }
	void _Start () {
        // 0 0 0    => 0
        // 0 0 1    => 1
        // 0 1 0    => 1
        // 0 1 1    => 0
        // 1 0 0    => 1
        // 1 0 1    => 0
        // 1 1 0    => 0
        // 1 1 1    => 1

        NeuralNetwork net = new NeuralNetwork(new int[] { 4, 088, 60, 2 }); //intiilize network

        //Itterate 5000 times and train each possible output
        //5000*8 = 40000 traning operations
        for (int i = 0; i < 5000; i++)
        {
            net.FeedForward(new float[] { .3f, .6f, 0, 1 });
            net.BackProp(new float[] { 1 , 1 });

            net.FeedForward(new float[] { .3f, .6f, 0 ,1});
            net.BackProp(new float[] { 1, 0});

            //net.FeedForward(new float[] { .5f, 1, 0 });
            //net.BackProp(new float[] { 1, 1 });

            //net.FeedForward(new float[] { 0, .1f, 1 });
            //net.BackProp(new float[] { 0, 1 });

            //net.FeedForward(new float[] { 1, .8f, 0 });
            //net.BackProp(new float[] { 1, 0 });

            //net.FeedForward(new float[] { 1, .2f, 1 });
            //net.BackProp(new float[] { 0, 1 });

            //net.FeedForward(new float[] { 1, .2f, 0 });
            //net.BackProp(new float[] { 0,0 });

            //net.FeedForward(new float[] { 1, .3f, 1 });
            //net.BackProp(new float[] { 1, 0 });
        }


        //output to see if the network has learnt
        //WHICH IT HAS!!!!!
        //UnityEngine.Debug.Log( net.FeedForward(new float[] { 0, 0, 0 })[0]);
        //UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 0, 1 })[0]);
        //UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 1, 0 })[0]);
        //UnityEngine.Debug.Log(net.FeedForward(new float[] { 0, 1, 1 })[0]);
        //UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 0, 0 })[0]);
        //UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 0, 1 })[0]);
        //UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 1, 0 })[0]);
        //UnityEngine.Debug.Log(net.FeedForward(new float[] { 1, 1, 1 })[0]);


        UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { .3f, .6f, 0 })[1]));
        //UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { .6f, 0, 1 })[1]));
        //UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { .5f, 1, 0 })[1]));
        //UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { 0, .1f, 1 })[1]));
        //UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { 1, .8f, 0 })[1]));
        //UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { 1, .2f, 1 })[1]));
        //UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { 1, .2f, 0 })[1]));
        //UnityEngine.Debug.Log(Out(net.FeedForward(new float[] { 1, .3f, 1 })[1]));



    }
    public int[] GetOutNeuralNetRealTime()
    {
        int[] pos2d = new int[2];//x, y
        //pos2d[0] = -1;
        //pos2d[1] = -1;
        net = new NeuralNetwork(new int[] { 150, 10, 10, 10, 150 });
        //SetInputFromFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\data.txt");//influence map
        float[] inputRealTime = SetInputRealTime();
        net.SetWeight(LoadWeightFromFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\weight.dat"));
        float[] feedForward = net.FeedForward(inputRealTime);
        for (int i = 0; i < 150; i++)
        {
            if (Out(feedForward[i]) == 1)//1dim to 2dim
            {
                print("i = " + i);
                pos2d[1] = i / 15;//y
                pos2d[0] =  i % 15;//x
            }
            //print(Out(net.FeedForward(input[0])[i]) + " " + i);
            //print(net.FeedForward(input)[i]);
        }
        print("x = " + pos2d[0]);
        print("y = " + pos2d[1]);
        return pos2d;
    }
    public int Out(double d)
    {
        //print(d);
        return (d<=.5f) ?  0 : 1;
    }
    // Update is called once per frame
    void Update () {
		//test
        if (Input.GetKeyDown("p"))
        {
            net = new NeuralNetwork(new int[] { 29, 10, 10, 10, 1 });
            SetInputFromFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\dataTraning.txt");            
            net.SetWeight(LoadWeightFromFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\weight.dat"));
            for (int i = 0; i < 150; i++)
            {
                print(Out(net.FeedForward(input[0])[i]) + " " + i);
                //print(net.FeedForward(input)[i]);
            }
        }
        if (Input.GetKeyDown("t"))
        {
            SetInputFromFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\dataTraning.txt");
            StartTraining();
            SaveWeightToFile(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\weight.dat");
        }

        //tets data traning for Infantry
        if (Input.GetKeyDown("j"))//save data of Infantry to file
        {
            refDataRows.SaveToDisk(true);
        }
        if (Input.GetKeyDown("k"))//test k-fold
        {
            TestOut(@"G:\merged_partition_content\Project\Unity\Advanced War - Copy\DataTraning\Infantry\dataTest.txt");
        }
	}
}
