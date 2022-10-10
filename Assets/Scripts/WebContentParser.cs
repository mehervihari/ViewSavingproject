using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebContentParser : MonoBehaviour
{
    // parses the images from htmlcode of the website
    public List<string> ParseImagesFromWebContent(string htmlCode)
    {
        Debug.Log("Parsing web content");

        List<string> links = new List<string>();
        string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?(?=http:?)(?=https:?)([^'"" >]+?)[ '""][^>]*?>";
        MatchCollection matchesImgSrc = Regex.Matches(htmlCode, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        foreach (Match m in matchesImgSrc)
        {
            string link = m.Groups[1].Value;
            links.Add(link);
        }
        return links;
    }

    public void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        WebRequests.Get(url, onError, onSuccess);
    }

    public void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        WebRequests.GetTexture(url, onError, onSuccess);
    }
}
