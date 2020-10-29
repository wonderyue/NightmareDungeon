using System;
using Message;
using UnityEngine;

public class ActorBehaviour : MonoBehaviour
{
    private Animator _animator;
    private int _id;
    private static readonly int Die = Animator.StringToHash("die");
    private static readonly int Attack = Animator.StringToHash("attack");

    public void Init(int id)
    {
        _id = id;
    }
    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Messenger.AddListener<int, string>(MsgConst.ACTOR_ANIM_TRIGGER, OnTrigger);
        Messenger.AddListener<int, string, bool>(MsgConst.ACTOR_ANIM_BOOL, OnBool);
        Messenger.AddListener<int>(MsgConst.ACTOR_DIE, OnActorDie);
        Messenger.AddListener<int>(MsgConst.ACTOR_ATTACK, OnActorAttack);
    }

    private void OnActorAttack(int id)
    {
        if (id == _id)
        {
            _animator.SetBool(Attack, true);
        }
    }

    private void OnActorDie(int id)
    {
        if (id == _id)
        {
            _animator.SetBool(Die, true);
        }
    }

    private void OnTrigger(int id, string trigger)
    {
        if (id == _id)
        {
            _animator.SetTrigger(trigger);
        }
    }
    
    private void OnBool(int id, string key, bool value)
    {
        if (id == _id)
        {
            _animator.SetBool(key, value);
        }
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<int, string>(MsgConst.ACTOR_ANIM_TRIGGER, OnTrigger);
        Messenger.RemoveListener<int, string, bool>(MsgConst.ACTOR_ANIM_BOOL, OnBool);
        Messenger.RemoveListener<int>(MsgConst.ACTOR_DIE, OnActorDie);
        Messenger.RemoveListener<int>(MsgConst.ACTOR_ATTACK, OnActorAttack);
    }
}