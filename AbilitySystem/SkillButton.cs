using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Image skillIcon; 
    public TMP_Text skillNameText; 
    public TMP_Text skillDescriptionText; 
    public Image[] starImages; 

    private SkillBase _associatedSkill;

    public void Setup(SkillBase skill)
    {
        Debug.Log($"Setting up {skill.SkillName} with Level {skill.Level}");
        Debug.Log($"Stars array length: {starImages.Length}");
        if (skill == null)
        {
            Debug.LogError("Given skill to Setup is null!");
            return;
        }

        _associatedSkill = skill;
        skillIcon.sprite = skill.Icon;
        skillNameText.text = skill.SkillName;
        skillDescriptionText.text = skill.Description;
        skill.OnLevelUpEvent += OnSkillLevelUp;

        UpdateStarImages(skill.Level);
    }

    private void UpdateStarImages(int level)
    {
        int levelToDisplay = Mathf.Min(level, starImages.Length);
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].enabled = i < levelToDisplay; 
            Debug.Log($"Star image at index {i}: {(starImages[i].enabled ? "Enabled" : "Disabled")}");
        }
    }

    private void OnSkillLevelUp()
    {
        UpdateStarImages(_associatedSkill.Level);
    }

    public void OnButtonPressed()
    {
        if (_associatedSkill == null)
        {
            Debug.LogError("No skill is associated with this button!");
            return;
        }

        Debug.Log($"[OnButtonPressed] Button pressed with associated skill: {_associatedSkill.SkillName}");
        UIManager.Instance.OnSkillChosen(_associatedSkill);
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed to avoid memory leaks
        if (_associatedSkill != null)
        {
            _associatedSkill.OnLevelUpEvent -= OnSkillLevelUp;
        }
    }
}