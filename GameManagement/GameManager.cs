using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Stats")]
    public int currentExperience = 0;
    public int experienceToLevelUp = 100;
    public int currentLevel = 1;
    public int pendingLevelUps = 0;
    public PlayerCharacter SelectedCharacter { get; private set; }
    public bool IsDamageMultiplierActive { get; private set; }
    public bool IsGameJustStarted { get; private set; } = true;
    private float elapsedTime = 0f;
    private bool isGameRunning = true;

    public delegate void ExperienceChanged(int newExperience);
    public event ExperienceChanged OnExperienceChanged;

    public delegate void LevelUpAction(int newLevel);
    public event LevelUpAction OnLevelUpEvent;

    public delegate void TimeUpdated(float elapsedTime);
    public event TimeUpdated OnTimeUpdated;

    public delegate void SkillEquippedAction(SkillBase equippedSkill);
    public event SkillEquippedAction OnSkillEquippedEvent;

    public List<PlayerCharacter> CharacterList = new List<PlayerCharacter>();

    public List<PlayerCharacter> characters;
    public GameObject antonioGameObject;
    public GameObject imeldaGameObject;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    
        Debug.Log("GameManager instance initialized.");

        InitializeCharacters(); 
    }
    public void SetSelectedCharacter(PlayerCharacter character)
    {
        Debug.Log("[GameManager] Setting selected character to " + character.Name);
        if (character == null)
        {
            Debug.LogError("Passed-in character is null");
            return;
        }
        Debug.Log($"Selected character set to {character.Name}"); // Added debug log
        SelectedCharacter = character;
    }
    void Start()
    {
        if (SkillManager.Instance != null)
        {
         
           // SkillManager.Instance.ResetSkills();
        }
        else
        {
            Debug.LogWarning("SkillManager instance is null. Delaying skill reset.");
        }

        Debug.Log("GameManager Start method called.");

     
    }
    private void Update()
    {
        if (isGameRunning)
        {
            elapsedTime += Time.deltaTime;
            OnTimeUpdated?.Invoke(elapsedTime);
        }
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
    public void PauseGameTimer()
    {
        isGameRunning = false;
    }

    public void ResumeGameTimer()
    {
        isGameRunning = true;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void AddExperience(int exp)
    {
        currentExperience += exp;
        OnExperienceChanged?.Invoke(currentExperience);
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        if (currentExperience >= experienceToLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Debug.Log("LevelUp() called"); // Add this line

        if (IsGameJustStarted)
        {
            IsGameJustStarted = false;
        }
        while (currentExperience >= experienceToLevelUp)
        {
            currentExperience -= experienceToLevelUp;
            currentLevel++;
            experienceToLevelUp *= 2;
            pendingLevelUps++;
            OnLevelUpEvent?.Invoke(currentLevel);

        }
        UIManager.Instance.ShowSkillChoicePanel();
    }
}