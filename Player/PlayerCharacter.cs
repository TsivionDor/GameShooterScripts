using System;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour 
{
    public static PlayerCharacter Instance; 
    public event Action OnDataChanged; 

    public string ID;
    public string Name;
    public SkillBase StartingWeapon;
    public float BaseDamage;
    public Sprite CharacterSprite;
    public int MaxHealth;
    public int Armor;
    public float Might;
    public float Growth;
    public int MoveSpeed;
    public float Recovery;
    public float AttackSpeed;
    public float Duration;
    public float Area;
    public float Magnet;
    public float Luck;
    public float Cooldown;
    public string Description { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public PlayerCharacter(string id, string name, SkillBase startingWeapon, string description, int maxHealthBonus, float baseDamage, Sprite characterSprite, int armorBonus, float mightBonus, float growthBonus, int moveSpeed, float recovery, float attackSpeed, float duration, float area, float magnet, float luck, float cooldown)
    {
        ID = id;
        Name = name;
        StartingWeapon = startingWeapon;
        Description = description; 
        MaxHealth = maxHealthBonus;
        BaseDamage = baseDamage;
        CharacterSprite = characterSprite;
        Armor = armorBonus;
        Might = mightBonus;
        Growth = growthBonus;
        MoveSpeed = moveSpeed;
        Recovery = recovery;
        AttackSpeed = attackSpeed;
        Duration = duration;
        Area = area;
        Magnet = magnet;
        Luck = luck;
        Cooldown = cooldown;
    }

  
    public void UpdateData(string newName, int newMaxHealth)
    {
        this.Name = newName;
        this.MaxHealth = newMaxHealth;

        OnDataChanged?.Invoke(); // Invoke event
    }
}