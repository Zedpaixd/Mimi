using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storytelling : MonoBehaviour
{

    private List<string> epilogue;
    public UI_Assistant writer;
    public UiManager uiManager;
    public PlayerState playerState;
    private bool endStoryStarted;

    void Start()
    {
        epilogue = new List<string>();
        epilogue.Add("In a long distant world, a rabbit was born.Mimi was an unlucky kind; since birth, she was unable to differentiate colours. Not thinking much of it, she thought it was a completely normal thing.");
        epilogue.Add("Years of criticism and confusion later, the surroundings made Mimi realise her one big lack; her sight of colors. It was not an easy thing to accept; Mimi simply refused to believe that others could see a wider range of colours than her.");
        epilogue.Add("Sorrowful months have passed by, until, one night, upon a deep look into the forest from her balcony, she saw something unseen before; it was a different, more fragrant shining shade of grey. It was almost as if... it was not grey?");
        epilogue.Add("Without a second thought, Mimi began her chase in fulfilling her inquiries about the witnessed anomaly.");
        writer.addStory(epilogue);
        writer.startWriting();
    }

    private void Update()
    {
        if (endStoryStarted && writer.getStoryLength() == 0 && writer.canMimiMove())
        {
            endStoryStarted = false;
            uiManager.SetEndScreenVisible(playerState.getcollectibleCount());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == Globals.PLAYER_TAG)
        {
            epilogue.Clear();
            epilogue.Add("As Mimi kept on approaching the bizarre anomaly previously seen, she has noticed weird things that started happening to her. Was it colours that she started seeing? And why were people trying to stop her?");
            epilogue.Add("Mimi wondered for a brief second but with no avail; the only thought that overwhelmed her mind was  of the odd experience she is living, giving her energy and motivation to continue.");
            writer.addStory(epilogue);
            writer.startWriting();
            endStoryStarted = true;
        }
    }
}
