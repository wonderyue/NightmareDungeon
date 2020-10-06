using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private List<Actor> _players;
    [SerializeField]
    private List<Actor> _enemies;
    private Random _rand;

    private void Start()
    {
        _rand = new Random((int)DateTime.Now.Ticks);
    }

    public void Update()
    {
        foreach (Actor actor in _players)
        {
            actor.UpdateTimeBar(Time.deltaTime);
        }
        foreach (Actor actor in _enemies)
        {
            actor.UpdateTimeBar(Time.deltaTime);
        }
    }

    public void CreateActor()
    {
        
    }
    
    private List<Actor> GetTarget(Actor caster, Skill skill)
    {
        if (caster.IsPlayer)
        {
            return _enemies;
        }
        else
        {
            return _players;
        }
    }

    public void SkillCast(Actor caster, Skill skill)
    {
        foreach (var target in GetTarget(caster, skill))
        {
            var skillResult = skill.OnCast(caster.CharacterInfo, target.CharacterInfo, _rand);
            SkillHit(target, skillResult);
        }
    }
    
    public void SkillHit(Actor target, SkillResult skillResult)
    {
        target.OnSkillHit(skillResult);
    }
}