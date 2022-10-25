using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{

    [SerializeField] public TextWriter textWriter;
    private Text messageText;



    private void Awake()
    {
        messageText = transform.Find("EpiTextbox").Find("message").GetComponent<Text>();
    }

    public void writeMessage(string message, float speedPerChara)
    {
        textWriter.Write(messageText, message, speedPerChara);
    }
}
