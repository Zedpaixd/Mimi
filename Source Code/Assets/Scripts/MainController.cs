using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject epi;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] UI_Assistant UI_Assistant;
    private List<string> epilogueStory;

    void Start()
    {
        playerMovement.canMove = false;

        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
        foreach (GameObject go in gos)
        {
            if (go.layer == 10)
            {
                epi = go;
            }
            
        }

        epilogueStory = new List<string>();
        epilogueStory.Add("In a long distant world, a rabbit was born. Mimi was an unlucky kind; since birth, she was unable to differentiate colours. Not thinking too much of it, she thought it was a completely normal thing.");
        epilogueStory.Add("Years of criticism and confusion later, the surroundings made Mimi realise her one big lack; her sight of colors. It was not an easy thing to accept; Mimi simply refused to believe that others could see a wider range of colours than her.");
        epilogueStory.Add("Sorrowful months have passed by, until, one night, upon a deep look into the forest from her balcony, she saw something unseen before; it was a different, more fragrant  shining shade of grey. It was almost as if... it was not grey?");
        epilogueStory.Add("Without a second thought, Mimi began her chase in fulfilling her inquiries about the witnessed anomaly.");


        //UI_Assistant = GetComponent<UI_Assistant>();
        UI_Assistant.writeMessage(epilogueStory[0], Globals.WRITING_SPEED);
        epilogueStory.RemoveAt(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && epi)
        {
            UI_Assistant.textWriter.resetText();

            if (epilogueStory.Count > 0)
            {
                UI_Assistant.writeMessage(epilogueStory[0], Globals.WRITING_SPEED);
                epilogueStory.RemoveAt(0);
            }
            else
            {
                epi.SetActive(false);
                playerMovement.canMove = true;
            }
            
        }
    }
}
