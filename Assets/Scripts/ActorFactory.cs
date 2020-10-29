using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactory
{
    private const string Path = "Prefabs/Actors/";
    private static readonly Dictionary<String, GameObject> _map = new Dictionary<string, GameObject>();
    public static GameObject CreateActor(String prefabName, Transform parent)
    {
        if (!_map.ContainsKey(prefabName))
        {
            _map[prefabName] = Resources.Load<GameObject>(Path + prefabName);
        }
        return GameObject.Instantiate(_map[prefabName], parent);;
    }
}