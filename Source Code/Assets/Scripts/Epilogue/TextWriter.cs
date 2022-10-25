using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class TextWriter : MonoBehaviour
{

    public Text uiText;
    private string textToBeWritten;
    private int charIndex;
    private float timePerChar;
    private float timer;

    public void resetText()
    {
        charIndex = 0;
        uiText = null;
    }

    public void Write(Text uiText, string textToBeWritten, float timePerChar)
    {
        this.uiText = uiText;
        this.textToBeWritten = textToBeWritten;
        this.timePerChar = timePerChar;
    }

    public void Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                timer += timePerChar;
                charIndex++;
                string text = textToBeWritten.Substring(0, charIndex);

                uiText.text = text;

                if (charIndex >= textToBeWritten.Length)
                {
                    uiText = null;
                    return;
                }
            }
        }
    }

    public Text getUiText()
    {
        return uiText;
    }


    
}
