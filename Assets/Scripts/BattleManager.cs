using System;
using System.Collections.Generic;
using Message;
using MVC.Views;
using UnityEngine;
using Random = System.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private List<Actor> _players;
    [SerializeField]
    private List<Actor> _enemies;
    private Random _rand;
    public Transform PlayerSlot;
    public Transform EnemySlot;
    public CharacterBoard PlayerBoard;
    public CharacterBoard EnemyBoard;
    public bool IsRunning { get; private set; }

    private void Start()
    {
        _rand = new Random((int)DateTime.Now.Ticks);
        Messenger.AddListener<int>(MsgConst.ACTOR_DIE, OnActorDie);
    }

    public void OnBattleStart(String playerName, String enemyName, CharacterInfo playerInfo)
    {
        IsRunning = true;
        GameObject playerObj = ActorFactory.CreateActor(playerName, PlayerSlot);
        Actor player = playerObj.GetComponent<Actor>();
        player.Init(playerInfo);
        _players.Add(player);
        PlayerBoard.Init(player);
        PlayerBoard.gameObject.SetActive(true);
        GameObject enemyObj = ActorFactory.CreateActor(enemyName, EnemySlot);
        Actor enemy = enemyObj.GetComponent<Actor>();
        enemy.Init(null);
        _enemies.Add(enemy);
        EnemyBoard.Init(enemy);
        EnemyBoard.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (!IsRunning)
        {
            return;
        }
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

    public Actor GetActorByID(int id)
    {
        Actor baseActor = _players.Find(a => a.CharacterInfo.ID == id);
        if (baseActor == null)
            baseActor = _enemies.Find(a => a.CharacterInfo.ID == id);
        return baseActor;
    }
    
    private List<Actor> GetTarget(Actor caster, Skill skill)
    {
        return caster.IsPlayer ? _enemies : _players;
    }

    public void SkillCast(Actor caster, Skill skill)
    {
        foreach (var target in GetTarget(caster, skill))
        {
            var skillResult = skill.OnCast(caster.CharacterInfo, target.CharacterInfo, _rand);
            Messenger.Broadcast(MsgConst.BATTLE_LOG, BattleLogHelper.GenerateBattleLog(caster, target, skill, skillResult));
            Messenger.Broadcast(MsgConst.ACTOR_DAMAGE, target.CharacterInfo.ID, skillResult);
            Messenger.Broadcast(MsgConst.ACTOR_EFFECT, target.CharacterInfo.ID, "hit_effect_01");
            SkillHit(target, skillResult);
            if (!IsRunning)
                break;
        }
    }
    
    public void SkillHit(Actor target, SkillResult skillResult)
    {
        target.OnSkillHit(skillResult);
    }

    private void OnActorDie(int id)
    {
        bool playerLose = true; 
        foreach (var player in _players)
        {
            if (player.IsAlive)
            {
                playerLose = false;
            }
        }
        if (playerLose)
        {
            OnBattleEnd(false);
        }
        bool enemyLose = true; 
        foreach (var enemy in _enemies)
        {
            if (enemy.IsAlive)
            {
                enemyLose = false;
            }
        }
        if (enemyLose)
        {
            OnBattleEnd(true);
        }
    }

    private void Clear()
    {
        foreach (var player in _players)
        {
            player.Clear();
        }
        _players.Clear();
        foreach (var enemy in _enemies)
        {
            enemy.Clear();
        }
        _enemies.Clear();
    }

    private void OnBattleEnd(bool win)
    {
        IsRunning = false;
        Clear();
        Messenger.Broadcast(MsgConst.BACK_TO_MAP);
    }
    
    private void OnDestroy()
    {
        Messenger.RemoveListener<int>(MsgConst.ACTOR_DIE, OnActorDie);
    }
}