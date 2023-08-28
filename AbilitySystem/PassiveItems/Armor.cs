
public class Armor : IPassiveAbility
{
    private int level = 0;
    private const int BASE_ARMOR_INCREASE = 1;
    private const float RETALIATORY_DAMAGE_INCREASE = 0.10f;

    public void Apply(Player player)
    {
      //  player.Character.ArmorBonus += level * BASE_ARMOR_INCREASE;
        // Assuming mightMultiplier is a field representing Might
       // player.damageMultiplier += player.damageMultiplier * level * RETALIATORY_DAMAGE_INCREASE;
    }

    public void LevelUp()
    {
        if (level < 5) level++;
    }
}