using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    public class ItemView : MonoBehaviour
    {
        public Image ItemImg;
        private UIManager _uiManager;
        private UIManager GetUIManager()
        {
            if (_uiManager == null)
                _uiManager = GameObject.Find("Root").GetComponent<UIManager>();
            return _uiManager;
        }

        public void Init(string img)
        {
            ItemImg.sprite = GetUIManager().GetSprite(img);
        }
    }
}