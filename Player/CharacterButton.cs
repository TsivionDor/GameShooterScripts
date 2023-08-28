using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.TextCore.Text;

public class CharacterButton : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Elements")]
    public UnityEvent<string> OnCharacterSelected;
    public Image characterImage;
    public TMP_Text characterName;
    public TMP_Text characterDescription;
    public GameObject extendedInfo;
    public TMP_Text baseDamageText;
    public TMP_Text armorText;
    public TMP_Text mightText;
    public TMP_Text growthText;
    public TMP_Text maxHealthText;
    public TMP_Text attackSpeedText;
    public TMP_Text cooldownText;
    public TMP_Text durationText;
    public TMP_Text magnetText;
    public TMP_Text luckText;

    [Header("RectTransform Settings")]
    private Vector2 smallSize = new Vector2(350, 200);
    private Vector2 extendedSize = new Vector2(350, 400);
    private RectTransform rectTransform;

    private string characterID;
    private bool isExtendedInfoVisible = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = smallSize;
        extendedInfo.SetActive(false);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void SetSmallSize(Vector2 newSize)
    {
        smallSize = newSize;
        if (!isExtendedInfoVisible) 
        {
            rectTransform.sizeDelta = smallSize;
        }
    }

    public void SetExtendedSize(Vector2 newSize)
    {
        extendedSize = newSize;
        if (isExtendedInfoVisible)
        {
            rectTransform.sizeDelta = extendedSize;
        }
    }
    public void SetupButton(PlayerCharacter characterData)
    {
       
        characterImage.sprite = characterData.CharacterSprite;
        characterName.text = characterData.Name;
        characterDescription.text = "Description:" + characterData.Description;
        baseDamageText.text = "Base Damage:  " + characterData.BaseDamage.ToString();
        armorText.text = "Armor:  " + characterData.Armor.ToString();
        mightText.text = "Might:  " + characterData.Might.ToString();
        growthText.text = "Growth:  " + characterData.Growth.ToString();
        maxHealthText.text = "Max Health:  " + characterData.MaxHealth.ToString();
        attackSpeedText.text = "Speed:  " + characterData.AttackSpeed.ToString();
        cooldownText.text = "Cooldown:  " + characterData.Cooldown.ToString();
        durationText.text = "Duration:  " + characterData.Duration.ToString();
        magnetText.text = "Magnet:  " + characterData.Magnet.ToString();
        luckText.text = characterData.Luck.ToString();

        characterID = characterData.ID;
    }
    public void ToggleExtendedInfo(bool shouldOpen)
    {
        isExtendedInfoVisible = shouldOpen;
        extendedInfo.SetActive(shouldOpen);
        rectTransform.sizeDelta = shouldOpen ? extendedSize : smallSize;
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void SetName(string name)
    {
        Debug.Log("[CharacterButton] Setting name to " + name);
        characterName.text = name;
    }

    public void SetDescription(string description)
    {
        Debug.Log("[CharacterButton] Setting description to " + description);
        characterDescription.text = description;
    }

    public void SetSprite(Sprite sprite)
    {
        Debug.Log("Setting Sprite");
        characterImage.sprite = sprite;
    }

    public void SetCharacterID(string id)
    {
        Debug.Log("Setting Character ID to " + id);
        characterID = id;
    }

    public string GetCharacterID()
    {
        return characterID;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnCharacterSelected == null)
        {
            Debug.LogError("OnCharacterSelected is null");
        }
        isExtendedInfoVisible = !isExtendedInfoVisible;
        CharacterSelectionManager.Instance.ToggleButton(this);
        ToggleExtendedInfo(isExtendedInfoVisible);

        OnCharacterSelected?.Invoke(characterID);
    }
}