using System;
using Message;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    public class MapUI : MonoBehaviour
    {
        public Text LevelTxt;
        public Image Bg;

        private void Awake()
        {
            Messenger.AddListener<int>(MsgConst.MAP_CHANGED, OnMapChanged);
        }

        private void OnMapChanged(int level)
        {
            LevelTxt.text = "LEVEL " + level;
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener<int>(MsgConst.MAP_CHANGED, OnMapChanged);
        }
    }
}