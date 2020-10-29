using System;
using System.Collections;
using Message;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BattleManager _battleManager;
    private UIManager _uiManager;
    public GameObject BattleUI;
    public GameObject BattleField;
    public GameObject Map;
    public CharacterInfo playerInfo;
    public int Level;
    private void Awake()
    {
        _battleManager = GetComponent<BattleManager>();
        _uiManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        Messenger.AddListener<Option>(MsgConst.OPTION_SELECTED, OnOptionsSelected);
        Messenger.AddListener(MsgConst.BACK_TO_MAP, OnEnterMap);
        NewGame();
        OnEnterMap();
        // NewGame();
        // OnEnterBattleField();
        // OnBattleStart();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<Option>(MsgConst.OPTION_SELECTED, OnOptionsSelected);
        Messenger.RemoveListener(MsgConst.BACK_TO_MAP, OnEnterMap);
    }

    public void NewGame()
    {
        playerInfo = null;
        Level = 0;
    }

    private Option[] GenerateNewLevel()
    {
        Option[] options = new Option[3];
        for (int i = 0; i < options.Length; i++)
        {
            options[i] = new Option();
            options[i].Name = "name_" + i;
            options[i].Desc = "description_" + i;
            options[i].Img = "loots_" + i;
        }
        options[0].Action = Constants.Action.Battle;
        options[1].Action = Constants.Action.Buff;
        options[2].Action = Constants.Action.Camp;
        return options;
    }

    private void OnOptionsSelected(Option option)
    {
        switch (option.Action)
        {
            case Constants.Action.Battle:
                OnEnterBattleField();
                OnBattleStart();
                break;
            case Constants.Action.Buff:
                WaitAndExecute(1, OnEnterMap);
                break;
            case Constants.Action.Camp:
                WaitAndExecute(1, OnEnterMap);
                break;
            case Constants.Action.Shop:
                WaitAndExecute(1, OnEnterMap);
                break;
            case Constants.Action.Event:
                WaitAndExecute(1, OnEnterMap);
                break;
        }
    }

    public void WaitAndExecute(float timer, Action callBack = null)
    {
        StartCoroutine(WaitAndExecute_CR(timer, callBack));
    }

    IEnumerator WaitAndExecute_CR(float timer, Action callBack = null)
    {
        yield return new WaitForSeconds(timer);
        callBack?.Invoke();
    }

    public void OnBattleStart()
    {
        _battleManager.OnBattleStart("Rouge", "Snake", playerInfo);
    }

    public void OnEnterMap()
    {
        Level++;
        BattleField.SetActive(false);
        BattleUI.SetActive(false);
        Map.SetActive(true);
        _uiManager.ShowSelectPanel(GenerateNewLevel());
        Messenger.Broadcast(MsgConst.MAP_CHANGED, Level);
    }

    public void OnEnterBattleField()
    {
        BattleField.SetActive(true);
        BattleUI.SetActive(true);
        Map.SetActive(false);
    }
}