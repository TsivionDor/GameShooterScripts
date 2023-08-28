public abstract class PassiveItemBase : SkillBase
{
    public string ItemName;
    public string UnlockRequirements;

    public override void Activate(PlayerData playerData)
    {
        // Implementation for passive items
    }


    public bool IsUnlocked()
    {
        int requiredLevelToUnlock = 10; // Example value, set to the level required to unlock this item

        return GameManager.Instance.currentLevel >= requiredLevelToUnlock;
    }
}