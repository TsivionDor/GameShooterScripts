using UnityEngine;
using System;

public abstract class SkillBase : ScriptableObject
{
    public string SkillName { get; protected set; }
    public string Description { get; protected set; }
    public int Level { get; protected set; } = 1;
    public int MaxLevel { get; protected set; } // Added MaxLevel property
    public bool IsActive { get; protected set; }
    public bool IsPassive { get; protected set; }
    public Sprite Icon { get; protected set; }

    public float BaseCooldown { get; protected set; }
    public float BaseDuration { get; protected set; }
    public float BaseRange { get; protected set; }

    // Other properties specific to certain types of skills might be moved to derived classes

    public event Action OnLevelUpEvent;

    protected Player player;

    public virtual void ResetSkill()
    {
        Level = 1;
    }

    public virtual void Equip(Player p)
    {
        player = p;
    }

    // Added an Unequip method for symmetry with Equip
    public virtual void Unequip(Player p)
    {
        // Optional logic for when the skill is unequipped
    }

    public override bool Equals(object obj)
    {
        if (obj is SkillBase otherSkill)
        {
            return SkillName.Equals(otherSkill.SkillName);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return SkillName.GetHashCode();
    }

    public virtual void Activate(PlayerData playerData) { /* Implementation */ }

    public void LevelUp(PlayerData playerData)
    {
        if (Level >= MaxLevel)
        {
            Debug.LogWarning($"[LevelUp] {SkillName} has reached the maximum level {MaxLevel} and cannot be leveled up further!");
            return;
        }

        Debug.Log($"[LevelUp] Before leveling up {SkillName} - Current Level {Level}!");
        Level++;
        Debug.Log($"[LevelUp] After leveling up {SkillName} - New Level {Level}!");
        OnLevelUp();
        Activate(playerData);
    }

    protected virtual void OnLevelUp()
    {
        OnLevelUpEvent?.Invoke();
    }

    // You might add a cooldown mechanism here or in the derived classes depending on your design
}