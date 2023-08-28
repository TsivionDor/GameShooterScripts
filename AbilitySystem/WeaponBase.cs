public abstract class WeaponBase : SkillBase
{
    public enum WeaponType { Normal, Magical }
    public WeaponType Type { get; protected set; }
    public new int MaxLevel { get; protected set; }
    public string weaponName { get; protected set; }
    public string weaponStats { get; protected set; }
    public new int Level { get; protected set; }
    protected float BaseDamage { get; set; }
    protected float Speed { get; set; }
    protected float Cooldown { get; set; }
    protected int Amount { get; set; }
    protected int Pierce { get; set; }
    public float Knockback { get; set; }
    public float Chance { get; set; }

    public override void Activate(PlayerData playerData)
    {
        Attack();
    }

    public abstract void Attack();
    public abstract void Upgrade();
    public bool IsMaxLevel() => Level >= MaxLevel;
}
