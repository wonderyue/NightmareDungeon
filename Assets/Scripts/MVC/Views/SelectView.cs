using System;
using Message;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    public class SelectView : MonoBehaviour
    {
        public GameObject itemPrefab;
        public Transform container;
        public ToggleGroup toggleGroup;
        public Text nameTxt;
        public Text desTxt;
        private ItemView[] _itemViews;
        private Option[] _options;
        private Option _selectedOption;
        private void Awake()
        {
            _itemViews = new ItemView[3];
            for (int i = 0; i < _itemViews.Length; i++)
            {
                GameObject obj = Instantiate(itemPrefab, container);
                _itemViews[i] = obj.GetComponent<ItemView>();
                Toggle toggle = obj.GetComponent<Toggle>();
                toggle.group = toggleGroup;
                var index = i;
                toggle.onValueChanged.AddListener((isOn) => { OnValueChanged(index, isOn); });
            }
        }

        public void Init(Option[] options)
        {
            _options = (Option[]) options.Clone();
            for (var index = 0; index < _itemViews.Length; index++)
            {
                if (index < _options.Length)
                {
                    var option = options[index];
                    _itemViews[index].Init(option.Img);
                    _itemViews[index].gameObject.SetActive(true);
                }
                else
                {
                    _itemViews[index].gameObject.SetActive(false);
                }
            }
            _itemViews[0].GetComponent<Toggle>().isOn = true;
        }

        public void OnValueChanged(int index, bool isOn)
        {
            if (isOn)
            {
                nameTxt.text = _options[index].Name;
                desTxt.text = _options[index].Desc;
                _selectedOption = _options[index];
            }
        }

        public void OnConfirm()
        {
            Messenger.Broadcast(MsgConst.OPTION_SELECTED, _selectedOption);
            gameObject.SetActive(false);
        }
    }
}