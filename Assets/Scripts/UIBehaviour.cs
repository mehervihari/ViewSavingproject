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
    public GameObject AnimationScroll;
    public Sprite ButtonPressImage;
    public Sprite ButtonDisableImage;

    private Vector2 startPos;
    private Vector2 direction;
    private Vector2 endPos;
    private float angle;

    private GameObject[] tabButtons;
    private GameObject[] tabScrolls;

    #region "Monobehaviour Methods"
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        elementsLoader = this.GetComponent<ElementsLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        tabButtons = GameObject.FindGameObjectsWithTag("TabButton");
        tabScrolls = GameObject.FindGameObjectsWithTag("TabScroll");
        OnClickCharactersButton();
    }

    // Update is called once in a frame
    void Update()
    {
        HandleSwipe();
    }
    #endregion

    #region "public methods"
    // displays the characters
    public void OnClickCharactersButton()
    {
        DisplayText.text = "Characters";
        ChangeScrolls("ScrollOf" + DisplayText.text);

        if (elementsLoader.characterCount_click == 0)
        {
            elementsLoader.LoadCharacterImages();
        }

        ChangeSprites("ButtonOf" + DisplayText.text);
    }

    // displays the saved characters
    public void OnClickSavedButton()
    {
        DisplayText.text = "Saved";
        ChangeScrolls("ScrollOf" + DisplayText.text);

        elementsLoader.LoadSavedImages();

        ChangeSprites("ButtonOf" + DisplayText.text);
    }

    public void OnClickAnimationsButton()
    {
        DisplayText.text = "Animations";
        ChangeScrolls("ScrollOf" + DisplayText.text);

        ChangeSprites("ButtonOf" + DisplayText.text);
    }
    #endregion

    #region "private methods"
    // Detect and handle swipe movements based on touch positions.
    private void HandleSwipe()
    {
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    endPos = touch.position;
                    direction = startPos - endPos;
                    angle = Vector2.Angle(Vector2.right, direction);

                    if (angle >= 0.5f && angle <= 30f)
                        NextPanel();
                    else if (angle >= 150f && angle <= 179.5f)
                        BeforePanel();
                    break;
            }
        }
    }

    private void NextPanel()
    {
        if (CharacterScroll.activeInHierarchy)
        {
            OnClickAnimationsButton();
        }
        else if (AnimationScroll.activeInHierarchy)
        {
            OnClickSavedButton();
        }
    }

    private void BeforePanel()
    {
        if (SavedScroll.activeInHierarchy)
        {
            OnClickAnimationsButton();
        }
        else if (AnimationScroll.activeInHierarchy)
        {
            OnClickCharactersButton();
        }
    }

    private void ChangeSprites(string activeButtonName)
    {
        foreach (GameObject tabButton in tabButtons)
        {
            if (tabButton.name == activeButtonName)
                tabButton.GetComponent<Button>().image.sprite = ButtonPressImage;
            else tabButton.GetComponent<Button>().image.sprite = ButtonDisableImage;
        }
    }

    private void ChangeScrolls(string activeScrollName)
    {
        foreach (GameObject tabScroll in tabScrolls)
        {
            if (tabScroll.name == activeScrollName)
                tabScroll.SetActive(true);
            else tabScroll.SetActive(false);
        }
    }
    #endregion
}
