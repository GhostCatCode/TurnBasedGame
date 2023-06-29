using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//武器类型 
public enum E_Weapon_Type
{
    None,
    Sword,
    Spear,
    Bow,
    Wand
}

// 物品背包类型
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
    expendable, //可叠加的消耗品: 箭矢, 战利品  特征 : 被动使用
    weapon, //武器,改变角色武器
    Skill //主动使用的道具, 使用后释放对应技能
}

// 房间类型
public enum E_Room_Type
{
    None,
    playerRoom, //玩家出生房间
    ChestRoom,  //宝箱房
    EnemyRoom,  //怪物房
    DestinationRoom, //终点房
    Other
}

// 格子类型
public enum E_Grid_Type
{
    None,
    Space,
    Chest,
    Enemy,
    HorizontalDoor,
    VerticalDoor,
    Destination,    //终点
}

//阵营类型
public enum E_Camp_Type
{
    None,
    Player,
    Neutral,
    Enemy,
}

//标准事件类型
public enum E_Event_Type
{
    OnPrepareTurn,  //回合准备  E_camp_type
    OnTurnStart,    //回合开始  E_camp_type
    OnTurnEnd,      //回合结束  E_camp_type
    OnSelectCharacter, //选择角色   character
    OnChangedCharacter, //角色发生变化    null
    OnSelectSkill,  //选择技能  SkillData
    OnSkillChanged, //技能发生变化    null
    OnDelorySkill, //释放技能   SkillData
    OnSelectItem,   //选择物品  ItemData
    OnItemChanged, //物品发生变化 null
    OnDeloryItem,   //使用物品  ItemData
    OnGameOver,     //游戏结束: 过关或者玩家角色死光  E_Game_Result
    OnCharacterCreated, //玩家被创建后    character
    OnCharacterDestroy, //玩家被删除前    character
    OnGameLoad, //游戏准备保存, GameData保存数据前发出信号,然后保存数据;
    OnGoldChanged,  //玩家金钱数变化   null
    OnCharacterDamage, //角色受到攻击
}

public enum E_Game_Result
{
    GameVictory,
    GameFailure
}

public enum E_Game_Type
{
    Gameing,    //游戏中
    Shoping,    //商店中
}

//网格对象选择类型
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

#region 动画
// 角色动画
public enum E_CharacterAnimation_Parameter
{
    isActiveFinal,
    isSelected,
    Attack,
    Injury  //受伤
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

#region 技能

// 技能类型 :给AI判定技能释放用 
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
