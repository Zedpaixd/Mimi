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
        epilogue.Add("After a long journey, Mimi found herself out of the {Level1 theme} that she lives in, straight into {Level2 theme}.");
        epilogue.Add("A weird feeling was emmerging, as if she was drawn towards something.");
        epilogue.Add("Did she get herself into something she shouldn't have? Is this whole thing a message from a more powerful being? What should Mimi do?");
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
            epilogue.Add("Through her travels, Mimi now found herself outside of the {Level2 theme} yet she was still not tired, yet thrilled to continue with her journey.");
            epilogue.Add("Though one thing was still on her mind. Why are others trying to stop her? Is she doing something she should not? It's the only possible explanation...");
            epilogue.Add("What should Mimi do...? Should she continue...? Should she stop and trace her way back home...?");
            writer.addStory(epilogue);
            writer.startWriting();
            endStoryStarted = true;
        }
    }
}
