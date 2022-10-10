using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementsLoader : MonoBehaviour
{
    private WebContentParser webParser;

    public GameObject CharacterRenderingPrefab;
    public GameObject CharacterPanelParent;
    public GameObject SavedRenderingPrefab;
    public GameObject SavedPanelParent;

    public string CharactersHtmlCode;
    public int characterCount_click = 0;
    public int savedCount_click = 0;

    public List<GameObject> characterImageList = new List<GameObject>();

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        webParser = this.GetComponent<WebContentParser>();
    }

    // loads the images parsed from website
    public void LoadCharacterImages()
    {
        List<string> imgLinks = webParser.ParseImagesFromWebContent(CharactersHtmlCode);
        Debug.Log("loading the images parsed from website");

        foreach (string imgLink in imgLinks)
        {
            webParser.GetTexture(imgLink, (string error) =>
            {
                Debug.Log("Error occured to parse image: " + error);
            }, (Texture2D tex) =>
            {
                LoadCharacterImages(tex, imgLink);
            });
        }
        characterCount_click++;
    }

    private void LoadCharacterImages(Texture2D texture2D, string link)
    {
        GameObject characterRenderingObject = Instantiate(CharacterRenderingPrefab, CharacterPanelParent.transform);
        characterRenderingObject.SetActive(true);
        characterRenderingObject.GetComponent<RawImage>().texture = texture2D;
        characterRenderingObject.GetComponent<CharacterDetail>().tex = texture2D;
        characterRenderingObject.GetComponent<CharacterDetail>().downloadLink = link;
        characterRenderingObject.GetComponent<CharacterDetail>().AddButtonListener();
        characterImageList.Add(characterRenderingObject);
    }

    // loads the save/ downloaded images
    public void LoadSavedImages()
    {
        Debug.Log("loading the saved/ downloaded images");
        for (int i = 0; i < characterImageList.Count; i++)
        {
            if(characterImageList[i].GetComponent<CharacterDetail>().isDownloaded)
            {
                GameObject savedRenderingObject = Instantiate(SavedRenderingPrefab, SavedPanelParent.transform);
                savedRenderingObject.SetActive(true);
                savedRenderingObject.GetComponent<RawImage>().texture = characterImageList[i].GetComponent<CharacterDetail>().tex;
            }
        }
        savedCount_click++;
    }
}
