using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    public List<SkillBase> AllSkills = new List<SkillBase>();  // Changed to SkillBase

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public List<SkillBase> GetThreeRandomSkills()
    {
        var randomSkills = AllSkills.OrderBy(x => Random.value).Distinct().Take(3).ToList();
        foreach (var skill in randomSkills)
        {
            Debug.Log($"Fetched Skill: {skill.SkillName}");
        }

        return randomSkills;
    }

    public void ResetAllSkills()
    {
        foreach (var skill in AllSkills)
        {
            skill.ResetSkill(); // Calling the ResetSkill method on each skill
            Debug.Log($"Skill {skill.SkillName} reset to level {skill.Level}.");
        }
    }
}