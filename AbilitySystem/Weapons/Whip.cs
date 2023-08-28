using System;
using UnityEngine;

public class Whip : WeaponBase
{
    public float Area { get; private set; }

    public Whip()
    {
        Type = WeaponType.Normal;
        MaxLevel = 8;
        BaseDamage = 10;
        Area = 1;
        Speed = 1;
        Amount = 1;
        Cooldown = 1.35f;
        Knockback = 1;
        Level = 1;
    }


    public override void Attack()
    {
        // Implement the Whip attack logic
        Debug.Log("Whip attack initiated!");
        // Implement the code to handle the actual attack mechanics here
    }
    public override void Upgrade()
    {
        if (Level >= MaxLevel)
        {
            Debug.Log("Max level reached!");
            return;
        }

        switch (Level)
        {
            case 1:
                Amount += 1;
                break;
            case 2:
            case 5:
            case 7:
                BaseDamage += 5;
                break;
            case 3:
            case 6:
                Area += Area * 0.10f;
                BaseDamage += 5;
                break;
            case 4:
                BaseDamage += 5;
                break;
        }

        Level++;
        Debug.Log($"Whip upgraded to Level {Level}!");
    }
}
