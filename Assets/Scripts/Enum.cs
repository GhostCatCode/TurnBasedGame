using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������� 
public enum E_Weapon_Type
{
    None,
    Sword,
    Spear,
    Bow,
    Wand
}

// ��Ʒ��������
public enum E_ItemBag_Type
{
    None,
    Player,
    Store,
    Box,
    Other,
}

public enum E_Character_Type
{
    None,
    Player,
    Enemy1,
    Enemy2,
}

public enum E_Item_Type
{
    expendable, //�ɵ��ӵ�����Ʒ: ��ʸ, ս��Ʒ  ���� : ����ʹ��
    weapon, //����,�ı��ɫ����
    Skill //����ʹ�õĵ���, ʹ�ú��ͷŶ�Ӧ����
}

// ��������
public enum E_Room_Type
{
    None,
    playerRoom, //��ҳ�������
    ChestRoom,  //���䷿
    EnemyRoom,  //���﷿
    DestinationRoom, //�յ㷿
    Other
}

// ��������
public enum E_Grid_Type
{
    None,
    Space,
    Chest,
    Enemy,
    HorizontalDoor,
    VerticalDoor,
    Destination,    //�յ�
}

//��Ӫ����
public enum E_Camp_Type
{
    None,
    Player,
    Neutral,
    Enemy,
}

//��׼�¼�����
public enum E_Event_Type
{
    OnPrepareTurn,  //�غ�׼��  E_camp_type
    OnTurnStart,    //�غϿ�ʼ  E_camp_type
    OnTurnEnd,      //�غϽ���  E_camp_type
    OnSelectCharacter, //ѡ���ɫ   character
    OnChangedCharacter, //��ɫ�����仯    null
    OnSelectSkill,  //ѡ����  SkillData
    OnSkillChanged, //���ܷ����仯    null
    OnDelorySkill, //�ͷż���   SkillData
    OnSelectItem,   //ѡ����Ʒ  ItemData
    OnItemChanged, //��Ʒ�����仯 null
    OnDeloryItem,   //ʹ����Ʒ  ItemData
    OnGameOver,     //��Ϸ����: ���ػ�����ҽ�ɫ����  E_Game_Result
    OnCharacterCreated, //��ұ�������    character
    OnCharacterDestroy, //��ұ�ɾ��ǰ    character
    OnGameLoad, //��Ϸ׼������, GameData��������ǰ�����ź�,Ȼ�󱣴�����;
    OnGoldChanged,  //��ҽ�Ǯ���仯   null
    OnCharacterDamage, //��ɫ�ܵ�����
}

public enum E_Game_Result
{
    GameVictory,
    GameFailure
}

public enum E_Game_Type
{
    Gameing,    //��Ϸ��
    Shoping,    //�̵���
}

//�������ѡ������
public enum E_GridSelector_Type
{
    All,
    Self,
    BaseMove,
    VisibleEnemy,
    VisibleFriendly,
    VisibleDamageable,
    VisibleInteractable,
    CanStandable,
    VisibleCanStandable,
}

#region ����
// ��ɫ����
public enum E_CharacterAnimation_Parameter
{
    isActiveFinal,
    isSelected,
    Attack,
    Injury  //����
}

public enum E_CharacterAnimation_Type
{
    Select,
    DeSelect,
    Active,
    ActiveFinal,
    Attack,
    Injury,
    Poison,
    Attack2,
}
#endregion

#region ����

// �������� :��AI�ж������ͷ��� 
public enum E_Skill_Type
{
    Move,
    Attack,
    Other,
    special
}


public enum E_Impact_type
{
    BaseMove,
    BaseInteract,
    BaseBuff,
    BaseAttack,
    BaseRecoverHp,
    SwordAttack,
    bowAttack,
    Charge,
    BaseRecoverSan,
    BasePrefab,
    BaseSound
}
#endregion


public enum E_buff_Type
{
    None,
    AddAtk,
    RecoverHp,
    RecoverSan,
    Fightback,
    Burn,
    AttackRecovery,
    AddDef,
    Poison,
    Counterattack,
    Incorporeal
}
