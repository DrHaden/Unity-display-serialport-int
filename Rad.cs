using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.Net;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO.Ports;
using System.Net.Mail;
using System.IO;
using UnityEditor;
using LitJson;

public class Rad : MonoBehaviour
{
    public Text email1;
    public Text memailtext;
    public Text memailtext2;
    public new Text name;

    string theremail;

    SerialPort sp = new SerialPort("COM5", 9600);

    public GameObject pos1;
    public GameObject pos2;
    public GameObject pos3;
    public GameObject noconnection;
    public Text weight;
    public Text Best1;
    public Text Best2;
    public Text Best3;

    public GameObject graph1;
    public GameObject graph2;
    public GameObject graph3;
    public GameObject graph4;
    public GameObject graph5;
    public GameObject graph6;

    int drop;
    int setone;
    int settwo;
    int setthree;

    int outofone = 16;

    // Use this for initialization
    void Start()
    {
        Debug.Log(Application.dataPath);

        sp.Open();
        sp.ReadTimeout = 1;
        patientname = PlayerPrefs.GetString("name");

        string email2 = PlayerPrefs.GetString("email" + patientname);
        Debug.Log("hi" + email2);
        memailtext.text = email2;
        name.text = patientname;
    }

    // Update is called once per frame
    void Update()
    {
        //Serial Port Set Up.
        if (sp.IsOpen)
        {
            try
            {
                measure(sp.ReadByte());
            }
            catch (System.Exception)
            {

            }
        }

        if (sp.IsOpen)
        {
            Debug.Log("Connected");

            noconnection.SetActive(false);

        }
        else
        {
            noconnection.SetActive(true);

            sp.Open();
            sp.ReadTimeout = 1;


        }
        //graph


        Best1.text = ("Best: " + setone + "g");
        Best2.text = ("Best: " + settwo + "g");
        Best3.text = ("Best: " + setthree + "g");



    }

    void measure(int receive)
    {
        weight.text = receive + "g";
        if (receive >= outofone)
        {
            graph1.SetActive(true);
        }
        else
        {
            graph1.SetActive(false);
        }
        if (receive >= 2 * outofone)
        {
            graph2.SetActive(true);
        }
        else
        {
            graph2.SetActive(false);
        }
        if (receive >= 3 * outofone)
        {
            graph3.SetActive(true);
        }
        else
        {
            graph3.SetActive(false);
        }
        if (receive >= 4 * outofone)
        {
            graph4.SetActive(true);
        }
        else
        {
            graph4.SetActive(false);
        }
        if (receive >= 5 * outofone)
        {
            graph5.SetActive(true);
        }
        else
        {
            graph5.SetActive(false);
        }
        if (receive >= 6 * outofone)
        {
            graph6.SetActive(true);
        }
        else
        {
            graph6.SetActive(false);
        }
        if (receive >= drop)
        {
            if (pos1.activeSelf == true)
            {
                setone = receive;
                Debug.Log("S1 " + setone);
            }
            if (pos2.activeSelf == true)
            {
                settwo = receive;
                Debug.Log("S2 " + settwo);
            }
            if (pos3.activeSelf == true)
            {
                setthree = receive;
                Debug.Log("S3 " + setthree);
            }
            drop = receive;

        }
    }

    public void begin()
    {
        sp.Write("B");
        drop = 0;
    }
    //email
    public void n1 (string patientname)
    {
        this.patientname = patientname;
        lognum = PlayerPrefs.GetInt("lognum" + patientname);
        Debug.Log("Hey: " + patientname);
        string email2 = PlayerPrefs.GetString("email" + patientname);
        Debug.Log(email2);
        memailtext.text = email2;
        memailtext2.text = email2;
        PlayerPrefs.SetString("name", patientname);

    }
    string patientname;
    public void datalog (string email)
    {
        PlayerPrefs.SetString("email" + patientname, email);
        memailtext.text = email;
        memailtext2.text = email;

    }
    

    //send
    int lognum;
    public void send()
    {
        if (lognum < 1)
        {
            lognum = 1;
        }
        else
        {
            lognum++;
        }
        DateTime thedate = DateTime.Now;
        PlayerPrefs.SetString(lognum + patientname + "date", thedate.ToString());
        PlayerPrefs.SetInt(lognum + patientname + "pos1", setone);
        PlayerPrefs.SetInt(lognum + patientname + "pos2", settwo);
        PlayerPrefs.SetInt(lognum + patientname + "pos3", setthree);
        PlayerPrefs.SetInt("lognum" + patientname, lognum);
        Debug.Log("lognum: " + lognum);
    }
}
