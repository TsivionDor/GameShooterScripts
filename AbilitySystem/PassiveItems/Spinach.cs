public class Spinach : IPassiveAbility
{
    private int level = 0;
    private const float BASE_MIGHT_INCREASE = 0.10f;

    public void Apply(Player player)
    {
      //  player.damageMultiplier += player.damageMultiplier * level * BASE_MIGHT_INCREASE;
    }

    public void LevelUp()
    {
        if (level < 5) level++;
    }
}
