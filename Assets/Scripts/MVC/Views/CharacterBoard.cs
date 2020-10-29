using UnityEngine;
using UnityEngine.UI;
using Message;

namespace MVC.Views
{
    public class CharacterBoard : MonoBehaviour
    {
        public Text Atk;
        public Text ExtraAtk;
        public Text Def;
        public Text ExtraDef;
        public Text Crt;
        public Text ExtraCrt;
        public Text Hp;
        public Image HpBar;
        public Image ApBar;
        private Actor _actor;
        private CharacterInfo _info;

        public void Init(Actor actor)
        {
            _actor = actor;
            _info = _actor.CharacterInfo;
            Refresh(_info.ID);
        }

        void Start()
        {
            Messenger.AddListener<int>(MsgConst.CHARACTER_INFO_UPDATE, Refresh);
            Refresh(_info.ID);
        }

        private void SetExtraString(Text txt, int origin, int current)
        {
            if (origin == current)
            {
                txt.text = "";
            }
            else if (origin < current)
            {
                txt.color = Color.green;
                txt.text = "+" + (current - origin);
            }
            else
            {
                txt.color = Color.red;
                txt.text = "-" + (origin - current);
            }
        }
        
        public void Refresh(int id)
        {
            if (id == _info.ID)
            {
                Atk.text = _info.Attack.ToString();
                Def.text = _info.Denfence.ToString();
                Crt.text = _info.Critical.ToString();
                Hp.text = _info.Hp + "/" + _info.RawHp;
                SetExtraString(ExtraAtk, _info.RawAttack, _info.Attack);
                SetExtraString(ExtraDef, _info.RawDenfence, _info.Denfence);
                SetExtraString(ExtraCrt, _info.RawCritical, _info.Critical);
                HpBar.fillAmount = (float)_info.Hp / _info.RawHp;
            }
        }

        private void Update()
        {
            ApBar.fillAmount = _actor.Time;
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<int>(MsgConst.CHARACTER_INFO_UPDATE, Refresh);
        }
    }
}