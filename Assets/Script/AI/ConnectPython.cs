using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectPython : MonoBehaviour {
    public GameObject notificatonError;
    public Text pathPythonFile; 
    public Text t;
    private string receiveFromPython, sentToPython;
    bool result;
    //
    static UdpClient client;
    static IPEndPoint ip;
    byte[] r;
    byte[] s;

    byte[] data;
    IPEndPoint ipep;
    UdpClient newsock;
    float timer;
    bool waitPython;
    float timeOut;

    static bool firsrLoad;
    public static bool notSuccess;
    void Start()
    {
        notSuccess = false;
        if (!firsrLoad)
        {
            client = new UdpClient(5455);
            ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5454);
            firsrLoad = true;
        }
        client.Client.ReceiveTimeout = 3000;
        sentToPython = "0.2,.05,5.9";
        
        //

    }
    public void CallMLP(string data)
    {
         
        sentToPython = data;
        Send(false);
    }
   
    void Update()
    {
        if (waitPython)
        {
            Recive();
            
        }
    }

    IEnumerator FailedConnect()
    {
        notificatonError.gameObject.SetActive(true);
        pathPythonFile.text = "Failed to connect to server (python)!\nPath:" + Application.dataPath + "/Python";
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene("Menu");
    }
    private void Send(bool feedback)
    {
        newsock = new UdpClient();
        newsock.Connect(IPAddress.Parse("127.0.0.1"), 5454);
        Byte[] senddata;
        if (!feedback)
        {
            senddata = Encoding.ASCII.GetBytes(sentToPython);
            waitPython = true;
            timeOut = Time.time + 3;
        }
        else
            senddata = Encoding.ASCII.GetBytes("fb");
        newsock.Send(senddata, senddata.Length);

    }
    private void Recive()
    {
        if (timer < Time.time)
        {
            //if (timeOut < Time.time)
            //{
            //    waitPython = false;
            //    StartCoroutine(FailedConnect());
            //}
            try
            {
                r = client.Receive(ref ip);
            }
            catch (Exception)
            {
                //client.Close();
                //StartCoroutine(FailedConnect());
                notSuccess = true;
                SceneManager.LoadScene("Menu");
                throw;
            }
            if (r != null)
            {
                receiveFromPython = ASCIIEncoding.ASCII.GetString(r);
                if (receiveFromPython == "OK")
                    result = true;
                else
                    result = false;
                //print(receiveFromPython);
                AI_DecisionMaking.Dec(result);
                waitPython = false;
                Send(true);
            }
            else
            {
                //print("null");
                return;
            }
            timer = Time.time + .1f;
        }


    }
}
