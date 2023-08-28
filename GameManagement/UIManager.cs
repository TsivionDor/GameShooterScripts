
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Components")]
    public SkillButton[] skillButtons;
    public TMP_Text playerLevelText;
    public ProgressBar progressBar;
    public TMP_Text movementSpeedText;
    public TMP_Text magnetRangeText;
    public TMP_Text minDamageText;
    public TMP_Text maxDamageText;
    public GameObject skillChoicePanel;
    [SerializeField] private TMP_Text timerText;

    [Header("Managers")]
    public SkillManager skillManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {

        GameManager.Instance.OnExperienceChanged += UpdateProgressBar;
        GameManager.Instance.OnLevelUpEvent += UpdateLevelText;
        GameManager.Instance.OnTimeUpdated += UpdateTimerUI;
        InitializeUI();
        InitializeSkillChoicePanel();
    }

    public void InitializeUI()
    {
        UpdateProgressBar(GameManager.Instance.currentExperience);
        UpdateLevelText(GameManager.Instance.currentLevel);
    }

    private void InitializeSkillChoicePanel()
    {
        // RefreshSkillDisplay(skillManager.GetThreeRandomSkills());
    }

    public void RefreshSkillDisplay(List<SkillBase> skillsToDisplay)
    {
        for (int i = 0; i < skillsToDisplay.Count; i++)
        {
            skillButtons[i].Setup(skillsToDisplay[i]);
        }
    }


    private void UpdateTimerUI(float elapsedTime)
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void UpdateProgressBar(int newExperience)
    {
        float progressPercentage = (float)newExperience / GameManager.Instance.experienceToLevelUp;
        progressBar.UpdateProgressBar(progressPercentage);
    }

    public void UpdateLevelText(int newLevel)
    {
        playerLevelText.text = newLevel.ToString();
    }
    public void HideSkillChoicePanel()
    {

        skillChoicePanel.SetActive(false);
    }

    public void OnSkillChosen(SkillBase chosenSkill)
    {
        Debug.Log($"[OnSkillChosen] Received skill: {chosenSkill?.SkillName ?? "NULL"}");

        if (chosenSkill == null)
        {
            Debug.LogError("[OnSkillChosen] Chosen skill is null");
            return;
        }

        Debug.Log($"[OnSkillChosen] Skill chosen: {chosenSkill.SkillName}");

        progressBar.ResetProgressBar();
        Time.timeScale = 1f;
        HideSkillChoicePanel();

        // Decrement the count of pending level ups
        GameManager.Instance.pendingLevelUps--;

        if (GameManager.Instance.pendingLevelUps > 0)
        {
            ShowSkillChoicePanel();
        }
        else
        {
            Time.timeScale = 1f;
            HideSkillChoicePanel();
        }


    }
    public void ShowSkillChoicePanel()
    {

        if (GameManager.Instance.IsGameJustStarted)
            return;

        Time.timeScale = 0f;
        if (GameManager.Instance.pendingLevelUps > 0)
        {
            List<SkillBase> skillsToDisplay = SkillManager.Instance.GetThreeRandomSkills();
            RefreshSkillDisplay(skillsToDisplay);

            Debug.Log($"[ShowSkillChoicePanel] Number of skills to display: {skillsToDisplay.Count}");
            for (int i = 0; i < skillsToDisplay.Count; i++)
            {
                Debug.Log($"[ShowSkillChoicePanel] Skill {i + 1}: {skillsToDisplay[i].SkillName}");
            }



            skillChoicePanel.SetActive(true);
        }
    }

}