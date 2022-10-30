using UnityEngine;
using UnityEngine.UI;


public class Writing : MonoBehaviour
{
    private Text uiText;
    private string textToWrite;
    private int charaIndex;
    private float timePerChar, 
                  timer;

    public void writeMessage(Text _uiText, string _textToWrite, float _timePerChar)
    {
        uiText = _uiText;
        textToWrite = _textToWrite;
        timePerChar = _timePerChar;
        charaIndex = 0;
    }

    private void Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer += timePerChar;
                try
                {
                    charaIndex++;
                    uiText.text = textToWrite.Substring(0, charaIndex);
                }
                catch
                {
                    return;
                }
                
            }
        }
    }

    public bool getStatus()
    {
        return uiText == null;
    }    

    public void resetText()
    {
        uiText = null;
    }
}