using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    private ElementsLoader elementsLoader;
    public TextMeshProUGUI DisplayText;
    public GameObject CharacterScroll;
    public GameObject SavedScroll;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        elementsLoader = this.GetComponent<ElementsLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        elementsLoader.LoadCharacterImages();
    }

    // displays the characters
    public void OnClickCharactersButton()
    {
        SavedScroll.SetActive(false);
        CharacterScroll.SetActive(true);
        DisplayText.text = "Charac";

        if (elementsLoader.characterCount_click == 0)
        {
            elementsLoader.LoadCharacterImages();
        }
    }

    // displays the saved characters
    public void OnClickSavedButton()
    {
        CharacterScroll.SetActive(false);
        SavedScroll.SetActive(true);
        DisplayText.text = "Saved";

        if (elementsLoader.savedCount_click == 0)
        {
            elementsLoader.LoadSavedImages();
        }
    }
}
