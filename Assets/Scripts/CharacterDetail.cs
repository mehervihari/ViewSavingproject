using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetail : MonoBehaviour
{
    public bool isDownloaded = false;
    public string downloadLink = "";
    public Texture2D tex;

    public void AddButtonListener()
    {
        this.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(OnClickDownloadButton);
    }

    public void OnClickDownloadButton()
    {
        isDownloaded = true;
    }
}