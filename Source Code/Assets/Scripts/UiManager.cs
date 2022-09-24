using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text uiText;

    public void UpdateCollectible(int collectibleCount){
        uiText.text="Collectible : "+collectibleCount;
    }


}
