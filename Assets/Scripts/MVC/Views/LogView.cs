using Message;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.Views
{
    public class LogView : MonoBehaviour
    {
        public Text log;
        
        void Start()
        {
            Messenger.AddListener<string>(MsgConst.BATTLE_LOG, AppendLog);
        }

        private void AppendLog(string content)
        {
            log.text += content;
        }
        
        void OnDestroy()
        {
            Messenger.RemoveListener<string>(MsgConst.BATTLE_LOG, AppendLog);
        }
    }
}