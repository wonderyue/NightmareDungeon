using System;
using System.Collections.Generic;
using MVC.Views;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class UIManager : MonoBehaviour
{
     public SelectView selectView;
     private readonly string[] _sprites = {"Sprites/loots", "Sprites/fire"};
     private Dictionary<string, Sprite> _spriteMap;
     private void Awake()
     {
          _spriteMap = new Dictionary<string, Sprite>();
          foreach (var path in _sprites)
          {
               Sprite[] sprites = Resources.LoadAll<Sprite>(path);
               foreach (var sprite in sprites)
               {
                    _spriteMap.Add(sprite.name, sprite);
               }
          }
     }

     public Sprite GetSprite(string spriteName)
     {
          return _spriteMap[spriteName];
     }

     public void ShowSelectPanel(Option[] options)
     {
          selectView.gameObject.SetActive(true);
          selectView.Init(options);
     } 
}