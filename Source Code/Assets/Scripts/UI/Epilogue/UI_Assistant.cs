using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{
    [SerializeField] private Writing textWriter;
    private Text messageText;

    private List<string> epilogue;
    [SerializeField] private PlayerMovement player;

    private void Awake()
    {
        messageText = transform.Find("Panel").Find("Message").GetComponent<Text>();
        epilogue = new List<string>();
        epilogue.Add("In a long distant world, a rabbit was born.Mimi was an unlucky kind; since birth, she was unable to differentiate colours. Not thinking much of it, she thought it was a completely normal thing.");
        epilogue.Add("Years of criticism and confusion later, the surroundings made Mimi realise her one big lack; her sight of colors.It was not an easy thing to accept; Mimi simply refused to believe that others could see a wider range of colours than her.");
        epilogue.Add("Sorrowful months have passed by, until, one night, upon a deep look into the forest from her balcony, she saw something unseen before; it was a different, more fragrant shining shade of grey. It was almost as if... it was not grey?");
        epilogue.Add("Without a second thought, Mimi began her chase in fulfilling her inquiries about the witnessed anomaly.");
    }

    // Update is called once per frame
    void Start()
    {
        player.canMove = false;
        textWriter.writeMessage(messageText, epilogue[0], 0.05f);
        epilogue.RemoveAt(0);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {

            if (epilogue.Count <= 0)
            {
                player.canMove = true;
                gameObject.SetActive(false);
                return;
            }

            if (!textWriter.getStatus())
            {
                textWriter.resetText();
            }
            textWriter.writeMessage(messageText, epilogue[0], Globals.WRITING_SPEED);
            epilogue.RemoveAt(0);
        }

        
    }
}
