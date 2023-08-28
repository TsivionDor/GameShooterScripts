using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class CharacterSelection : MonoBehaviour
{
    public Image selectedCharacterImage;
    public TMP_Text selectedCharacterName;
    public TMP_Text selectedCharacterDescription;

    public List<PlayerCharacter> characters;
    public Player playerController;
    public GameObject characterSelectionUI;
    public Transform characterSelectionPanel;
    public GameObject characterButtonPrefab;
    public PlayerData playerData;

    public GameObject antonioGameObject;
    public GameObject imeldaGameObject;

    List<GameObject> characterButtons = new List<GameObject>();

    private void Awake()
    {
        // Initialize characters in Awake method
        InitializeCharacters();
        Debug.Log("Characters initialized.");
    }

    private void InitializeCharacters()
    {
        WeaponBase whip = new Whip();
        Sprite antonioSprite = antonioGameObject.GetComponent<SpriteRenderer>().sprite;
        WeaponBase magicWand = new MagicWand();
        Sprite imeldaSprite = imeldaGameObject.GetComponent<SpriteRenderer>().sprite;

        characters = new List<PlayerCharacter>
        {
            new PlayerCharacter("ANTONIO", "Antonio Belpaese", whip, "Gains 10% more damage every ten levels (max +50%)", 130, 10f, antonioSprite, 1, 1, 1,10,0.5f,1,1,1,1,1,1),
            new PlayerCharacter("Imelda", "Imelda Belpaese", magicWand, "Gains 10% more experience every 5 levels (max +30%)", 120, 8f, imeldaSprite, 0, 0, 0,10,0.5f,1,1,1,1,1,1)
        };
    }

    private void Start()
    {
        Debug.Log("[CharacterSelection] Start method called");
        SetupCharacterButtons();
        PopulateCharacterSelectionUI();
    }


    private void SetupCharacterButtons()
    {
        foreach (GameObject characterButton in characterButtons)
        {
            CharacterButton buttonScript = characterButton.GetComponent<CharacterButton>();
            buttonScript.OnCharacterSelected.AddListener(SelectCharacter);
        }
    }

    public void PopulateCharacterSelectionUI()
    {
        foreach (GameObject btn in characterButtons)
        {
            btn.SetActive(false);  // Deactivate instead of destroying
        }

        for (int i = 0; i < characters.Count; i++)
        {
            GameObject characterButton;
            if (i < characterButtons.Count)
            {
                characterButton = characterButtons[i];
                characterButton.SetActive(true);  // Reactivate existing button
            }
            else
            {
                characterButton = Instantiate(characterButtonPrefab, characterSelectionPanel);
                characterButtons.Add(characterButton);  // Add to list
            }
            CharacterButton buttonScript = characterButton.GetComponent<CharacterButton>();
            buttonScript.SetupButton(characters[i]);
            buttonScript.OnCharacterSelected.AddListener(SelectCharacter);
        }
    }

    public void SelectCharacter(string characterID)
    {
        Debug.Log("Attempting to select character with ID: " + characterID);
        Debug.Log("Existing characters: " + string.Join(", ", characters.Select(c => c.ID).ToArray()));

        if (characters == null)
        {
            Debug.LogError("Characters list is null");
            return;
        }

        PlayerCharacter selectedCharacter = characters.Find(character => character.ID == characterID);
        Debug.Log("Selected character is: " + (selectedCharacter == null ? "null" : selectedCharacter.Name));
        if (selectedCharacter == null)
        {
            Debug.LogError("Selected character is null. Double-check IDs and list population.");
            return;
        }
        Debug.Log("[CharacterSelection] Successfully selected character: " + selectedCharacter.Name);
        Debug.Log("Selected character is: " + (selectedCharacter == null ? "null" : selectedCharacter.Name));

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null");
            return;
        }
        GameManager.Instance.SetSelectedCharacter(selectedCharacter);

        if (selectedCharacterImage == null)
        {
            Debug.LogError("selectedCharacterImage is null");
            return;
        }
        selectedCharacterImage.sprite = selectedCharacter.CharacterSprite;

        if (selectedCharacterName == null)
        {
            Debug.LogError("selectedCharacterName is null");
            return;
        }
        selectedCharacterName.text = selectedCharacter.Name;

        if (selectedCharacterDescription == null)
        {
            Debug.LogError("selectedCharacterDescription is null");
            return;
        }
        selectedCharacterDescription.text = selectedCharacter.Description;
    }
    public void StartGame()
    {
        Debug.Log("[CharacterSelection] Character selection UI deactivated. Starting game...");
        characterSelectionUI.SetActive(false);
        Debug.Log("Character selection UI deactivated. Starting game...");

        UnityEngine.SceneManagement.SceneManager.LoadScene("SpaceShooterScene1");
    }
}