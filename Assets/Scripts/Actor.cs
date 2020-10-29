using System;
using System.Collections.Generic;
using Message;
using MVC;
using Unity.Mathematics;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public CharacterConfig CharacterConfig;
    public CharacterInfo CharacterInfo;
    [field: SerializeField] public bool IsPlayer { get; private set; }
    [field: SerializeField][field: Range(0.0f, 1.0f)] public float Time { get; private set; }
    protected BattleManager _battleManager;
    public bool IsAlive => CharacterInfo.Hp > 0;

    protected void Awake()
    {
        _battleManager = GameObject.Find("Root").GetComponent<BattleManager>();
    }

    public void Init(CharacterInfo info)
    {
        CharacterInfo = info ?? new CharacterInfo(CharacterConfig);
        GetComponent<ActorBehaviour>().Init(CharacterInfo.ID);
        GetComponent<EffectPool>().Init(CharacterInfo.ID);
        InitPool();
    }

    private void InitPool()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        var bounds = sprite.bounds;
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(bounds.center.x,bounds.max.y,0));
        GameObject prefab = Resources.Load<GameObject>("Prefabs/UI/FloatTextPool");
        GameObject floatText = Instantiate(prefab, GameObject.Find("Canvas").transform);
        RectTransform rect = floatText.GetComponent<RectTransform>();
        floatText.GetComponent<FloatTextPool>().Init(CharacterInfo.ID);
        rect.anchoredPosition = Vector2.zero;
        rect.position = pos;
    }

    public void UpdateTimeBar(float deltaTime)
    {
        Time += deltaTime * CharacterInfo.Speed / 100;
        if (Time > 1)
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
        List<Buff> toRemove = new List<Buff>();
        foreach (var buff in CharacterInfo.Buffs)
        {
            buff.RefreshCd();
            if (buff.LeftCd == 0)
            {
                buff.OnRoundBegin(CharacterInfo);
            }
            if (!buff.IsActive)
            {
                toRemove.Add(buff);
            }
        }
        foreach (var buff in toRemove)
        {
            CharacterInfo.Buffs.Remove(buff);
        }
        foreach (var skill in CharacterInfo.ActiveSkills)
        {
            skill.RefreshCd();
        }
    }

    private Skill curSkill;
    private void AutoCast()
    {
        for (var index = CharacterInfo.ActiveSkills.Count - 1; index >= 0; index--)
        {
            var skill = CharacterInfo.ActiveSkills[index];
            if (skill.LeftCd == 0)
            {
                if (skill.OnCastBegin(CharacterInfo))
                {
                    _battleManager.SkillCast(this, skill);
                }
                else
                {
                    curSkill = skill;
                }
                break;
            }
        }
    }

    public void OnSkillCast()
    {
        if (IsAlive)
        {
            _battleManager.SkillCast(this, curSkill);
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
        AddHp(-skillResult.Damage);
    }
    
    private void AddHp(int value)
    {
        CharacterInfo.Hp = math.clamp(CharacterInfo.Hp + value, 0, CharacterInfo.RawHp);
        if (CharacterInfo.Hp == 0)
        {
            Messenger.Broadcast(MsgConst.ACTOR_DIE, CharacterInfo.ID);
        }
        Messenger.Broadcast(MsgConst.CHARACTER_INFO_UPDATE, CharacterInfo.ID);
    }

    public void Clear()
    {
        Destroy(gameObject);
    }
}