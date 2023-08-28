using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MagicWand : WeaponBase
{
    public new int Pierce { get; private set; }

    public MagicWand()
    {
        Type = WeaponType.Magical;
        MaxLevel = 8;
        BaseDamage = 0;
        Amount = 1;
        Cooldown = 1.0f;
        Pierce = 0;
    }

    public override void Attack()
    {
        // Implement the Magic Wand attack logic
        Debug.Log("MagicWand attack initiated!");
        // Implement the code to handle the actual attack mechanics here
    }

    public override void Upgrade()
    {
        if (IsMaxLevel())
        {
            Debug.Log("Max level reached!");
            return;
        }

        switch (Level)
        {
            case 1:
            case 4:
            case 6:
                Amount += 1;
                break;
            case 2:
                Cooldown -= 0.2f;
                break;
            case 3:
            case 8:
                BaseDamage += 10;
                break;
            case 5:
                BaseDamage += 10;
                break;
            case 7:
                Pierce += 1;
                break;
        }

        Level++;
        Debug.Log($"MagicWand upgraded to Level {Level}!");
    }
}

