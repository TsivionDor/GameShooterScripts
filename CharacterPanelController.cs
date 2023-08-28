using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterPanelController : MonoBehaviour
{
  

    
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
    private void Start()
    {
        // Subscribe to the event from the PlayerCharacter Singleton
        PlayerCharacter.Instance.OnDataChanged += UpdateUI;

        // Initialize UI
        UpdateUI();
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        PlayerCharacter.Instance.OnDataChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        characterName.text ="Name: " + PlayerCharacter.Instance.Name;
        characterDescription.text = "Description:" + PlayerCharacter.Instance.Description;
        baseDamageText.text = "Base Damage:  " + PlayerCharacter.Instance.BaseDamage.ToString();
        armorText.text = "Armor:  " + PlayerCharacter.Instance.Armor.ToString();
        mightText.text = "Might:  " +PlayerCharacter.Instance.Might.ToString();
        growthText.text = "Growth:  " + PlayerCharacter.Instance.Growth.ToString();
        maxHealthText.text = "Max Health:  " + PlayerCharacter.Instance.MaxHealth.ToString();
        attackSpeedText.text = "Speed:  " + PlayerCharacter.Instance.AttackSpeed.ToString();
        cooldownText.text = "Cooldown:  " + PlayerCharacter.Instance.Cooldown.ToString();
        durationText.text = "Duration:  " + PlayerCharacter.Instance.Duration.ToString();
        magnetText.text = "Magnet:  " + PlayerCharacter.Instance.Magnet.ToString();
        luckText.text = PlayerCharacter.Instance.Luck.ToString();       
    }
}
