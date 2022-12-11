using System.Collections;
using System.Linq;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiLevelSelector : MonoBehaviour
{
    [SerializeField] List<Button> levelButtons;
    // Start is called before the first frame update
    private void OnEnable()
    {
        JsonSaveDAO save = new JsonSaveDAO(Application.persistentDataPath);

        levelButtons.ForEach(button => button.interactable = false);

        int lastLevelIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/" + save.getCurrentLevelFromJson());
        for (int i = 0; i < lastLevelIndex; i++)
        {
            if (i < levelButtons.Count)
                levelButtons[i].interactable = true;
        }

    }

    // Update is called once per frame
}
