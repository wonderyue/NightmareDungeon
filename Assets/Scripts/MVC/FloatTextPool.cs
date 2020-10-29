using System.Collections.Generic;
using Message;
using UnityEngine;
using UnityEngine.UI;

namespace MVC
{
    public class FloatTextPool : MonoBehaviour  {
        public List<GameObject> pooledText;
        public GameObject prefab;
        public int initCount;
        public int normalFontSize = 80;
        public int criticalFontSize = 160;
        private int _actorId;
        
        public void Init(int id)
        {
            _actorId = id;
        }

        private void Start()
        {
            pooledText = new List<GameObject> ();
            for (int i = 0; i < initCount; i++)
            {
                Instantiate();
            }
            Messenger.AddListener<int, SkillResult>(MsgConst.ACTOR_DAMAGE, ShowDamageText);
        }

        private void ShowDamageText(int id, SkillResult skillResult)
        {
            if (id == _actorId)
            {
                GameObject obj = GetPooledText();
                obj.SetActive(true);
                Text txt = obj.GetComponent<Text>();
                if (skillResult.IsMiss)
                {
                    txt.text = "Miss";
                    txt.fontSize = normalFontSize;
                }
                else
                {
                    txt.text = "-" + skillResult.Damage;
                    txt.fontSize = skillResult.IsCritical ? criticalFontSize : normalFontSize;
                }
            }
        }

        private GameObject Instantiate()
        {
            GameObject obj = Instantiate(prefab, gameObject.transform, false);
            obj.SetActive (false);
            pooledText.Add (obj);
            return obj;
        }
    
        private GameObject GetPooledText()
        {
            foreach(var txt in pooledText){
                if(!txt.activeInHierarchy){
                    return txt;
                }
            }
            return Instantiate();
        }

        private void OnDisable()
        {
            Messenger.RemoveListener<int, SkillResult>(MsgConst.ACTOR_DAMAGE, ShowDamageText);
        }
    }
}