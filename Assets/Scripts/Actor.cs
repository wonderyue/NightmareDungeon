using System;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [field: SerializeField] public bool IsPlayer { get; private set; }
    [field: SerializeField] public float Time { get; private set; }
    public CharacterInfo CharacterInfo;
    protected List<Buff> _buffs;
    protected List<Skill> _activeSkills;
    protected BattleManager _battleManager;

    public Actor(bool isPlayer, List<Buff> buffs, List<Skill> activeSkills)
    {
        IsPlayer = isPlayer;
        _buffs = buffs;
        _activeSkills = activeSkills;
    }

    private void Start()
    {
        _battleManager = GameObject.Find("Main Camera").GetComponent<BattleManager>();
        _activeSkills = new List<Skill>();
        _activeSkills.Add(new AttackSkill("SimpleAttack"));
    }

    public void UpdateTimeBar(float deltaTime)
    {
        Time += deltaTime * CharacterInfo.Speed;
        if (Time > Constants.MAX_TIME_BAR_VALUE)
        {
            OnActionTurn();
            Time = 0;
        }
    }

    private void OnActionTurn()
    {
        RefreshCd();
        AutoCast();
    }

    private void RefreshCd()
    {
        foreach (var skill in _activeSkills)
        {
            Debug.Log(skill.Cd);
            skill.RefreshCd();
        }
    }

    private void AutoCast()
    {
        for (var index = _activeSkills.Count - 1; index >= 0; index--)
        {
            var skill = _activeSkills[index];
            if (skill.LeftCd == 0)
            {
                _battleManager.SkillCast(this, skill);
                break;
            }
        }
    }

    public void OnSkillHit(SkillResult skillResult)
    {
        if (skillResult.IsMiss)
        {
            Debug.Log("Miss");
            return;
        }
        if (skillResult.IsCritical)
        {
            Debug.Log("Critical");
        }
        CharacterInfo.AddHp(-skillResult.Damage);
    }
}