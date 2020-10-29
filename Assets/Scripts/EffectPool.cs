using System;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class EffectPool : MonoBehaviour  {
       public List<GameObject> pooledText;
       private Dictionary<string, GameObject> _prefabs;
       private Dictionary<string, List<GameObject>> _effects;
       private int _actorId;
       private const string Path = "Prefabs/Effects/";

       private void Awake()
       {
           _prefabs = new Dictionary<string, GameObject>();
           _effects = new Dictionary<string, List<GameObject>>();
       }

       private void Start()
       {
           Messenger.AddListener<int, string>(MsgConst.ACTOR_EFFECT, ShowEffect);
       }

       public void Init(int id)
       {
           _actorId = id;
       }

       private void ShowEffect(int id, string effectName)
       {
           if (id == _actorId)
           {
               GameObject obj = GetEffect(effectName);
               obj.SetActive(true);
           }
       }

       private GameObject Instantiate(string effectName)
       {
           if (!_prefabs.ContainsKey(effectName))
           {
               _prefabs.Add(effectName, Resources.Load<GameObject>(Path+effectName));
           }
           GameObject obj = Instantiate(_prefabs[effectName], gameObject.transform, false);
           if (!_effects.ContainsKey(effectName))
           {
               _effects.Add(effectName, new List<GameObject>());
           }
           _effects[effectName].Add (obj);
           return obj;
       }
   
       private GameObject GetEffect(string effectName)
       {
           if (_effects.ContainsKey(effectName))
           {
               foreach(var effect in _effects[effectName]){
                   if(!effect.activeInHierarchy){
                       return effect;
                   }
               }
           }
           return Instantiate(effectName);
       }

       private void OnDisable()
       {
           Messenger.RemoveListener<int, string>(MsgConst.ACTOR_EFFECT, ShowEffect);
       }
}