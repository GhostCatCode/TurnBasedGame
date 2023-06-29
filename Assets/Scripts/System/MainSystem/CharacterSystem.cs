using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterSystem : BaseSystem<CharacterSystem>
{
    [SerializeField] private String_SO enemyNameData;
    [SerializeField] private PlayerCharacterList_SO playerCharacterSO;

    // ��ҽ�ɫ�������
    private int maxCnt;
    public int MaxCnt => maxCnt;

    private List<Character> characterList = new List<Character>();
    private List<Character> playerList = new List<Character>();
    private List<Character> NeutralList = new List<Character>();
    private List<Character> EnemyList = new List<Character>();


    public List<Character> PlayerList => playerList;

    private void Start()
    {
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnCharacterCreated.ToString(), OnCharacterCreated);
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnCharacterDestroy.ToString(), OnCharacterDestroy);
        EventCenter.Instance.AddEventListener(E_Event_Type.OnGameLoad.ToString(), OnGameLoad);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnCharacterCreated.ToString(), OnCharacterCreated);
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnCharacterDestroy.ToString(), OnCharacterDestroy);
    }

    private void OnCharacterCreated(Character character)
    {

        // ����ɫ�Ž���Ӧ��ɫ��
        characterList.Add(character);
        switch (character.GetCampType())
        {
            case E_Camp_Type.None:
                break;
            case E_Camp_Type.Player:
                if(character.IsPlayerController())
                {
                    playerList.Add(character);
                }
                else
                {
                    NeutralList.Add(character);
                }
                break;
            case E_Camp_Type.Neutral:
                NeutralList.Add(character);
                break;
            case E_Camp_Type.Enemy:
                EnemyList.Add(character);
                break;
        }
    }

    private void OnCharacterDestroy(Character character)
    {
        // ����ɫ�Ӷ�Ӧ��ɫ��ɾ��
        characterList.Remove(character);
        switch (character.GetCampType())
        {
            case E_Camp_Type.None:
                break;
            case E_Camp_Type.Player:
                if (character.IsPlayerController())
                {
                    playerList.Remove(character);
                }
                else
                {
                    NeutralList.Remove(character);
                }
                break;
            case E_Camp_Type.Neutral:
                NeutralList.Remove(character);
                break;
            case E_Camp_Type.Enemy:
                EnemyList.Remove(character);
                break;
        }

        // �ж��Ƿ�ʧ��
        if (playerList.Count == 0 && !GameDataMgr.Instance.IsGameOver)
        {
            GameMain.Instance?.GameFailure();
        }
    }

    private void OnGameLoad()
    {
        PlayerCharacterListData playerCharacterListData = new PlayerCharacterListData();
        playerCharacterListData.isNotFirst = true;
        playerCharacterListData.maxCnt = maxCnt;
        playerCharacterListData.list = new List<PlayerCharacterData>();
        for (int i = 0; i < playerList.Count; i++)
        {
            playerCharacterListData.list.Add(playerList[i].Compress());
        }
        GameDataMgr.Instance.LoadPlayerCharacterListData(playerCharacterListData);
    }

    //��ö�Ӧ��Ӫ�����н�ɫ
    public List<Character> GetcharacterListOnCampType(E_Camp_Type campType)
    {
        switch (campType)
        {
            case E_Camp_Type.Player:
                return playerList;
            case E_Camp_Type.Neutral:
                return NeutralList;
            case E_Camp_Type.Enemy:
                return EnemyList;
        }
        return null;
    }

    //������н�ɫ
    public List<Character> GetAllCharacters()
    {
        return characterList;
    }


    // �������һ������
    public void RandomCreateEnemy(GridPosition gridPosition)
    {
        List<string> enemyNameList = enemyNameData.data;
        int index = Random.Range(0, enemyNameList.Count);
        string name = enemyNameList[index];
        CreateCharacter(name, gridPosition);
    }

    // ������Ӧ��ɫ
    public void CreateCharacter(string characterName, GridPosition gridPosition)
    {
        if (GridSystem.Instance.TryGetCharacterOnGridPosition(gridPosition, out Character character)) return;
        ResMgr.Instance.LoadAsync<GameObject>("Character/" + characterName, (obj) =>
        {
            obj.transform.position = GridSystem.Instance.GetWorldPosition(gridPosition);
        });
    }

    // ������ҽ�ɫ��
    public void CreatePlayerCharacter()
    {
        List<GridPosition> playerGridPosList = GridSystem.Instance.GetPlayerGridPosList();
        PlayerCharacterListData playerCharacterListData = GameDataMgr.Instance.PlayerCharacterListData;
        if (playerCharacterListData.list.Count == 0)
        {
            for (int i = 0; i < playerCharacterSO.list.Count; i ++)
            {
                Vector2 position = GridSystem.Instance.GetWorldPosition(playerGridPosList[i]);
                CharacterStatus characterStatus = playerCharacterSO.list[i].status;
                List<int> skillSaveData = playerCharacterSO.list[i].skillSaveData;
                ResMgr.Instance.LoadAsync<GameObject>("Character/Player", (obj) =>
                {
                    obj.transform.position = position;
                    Character character = obj.GetComponent<Character>();
                    character.Setup(characterStatus, skillSaveData);
                });
            }
        }
        else
        {
            for (int i = 0; i < playerCharacterListData.list.Count; i ++)
            {
                Vector2 position = GridSystem.Instance.GetWorldPosition(playerGridPosList[i]);
                CharacterStatus characterStatus = playerCharacterListData.list[i].status;
                List<int> skillSaveData = playerCharacterListData.list[i].skillSaveData;
                ResMgr.Instance.LoadAsync<GameObject>("Character/Player", (obj) =>
                {
                    obj.transform.position = position;
                    Character character = obj.GetComponent<Character>();
                    character.Setup(characterStatus, skillSaveData);
                });
            }
        }
        CameraSystem.Instance.SetCameraPosition(GridSystem.Instance.GetWorldPosition(playerGridPosList[0]));
    }
}
