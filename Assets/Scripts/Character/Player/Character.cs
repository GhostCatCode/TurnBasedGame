using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D;

[RequireComponent(typeof(CharacterSkillManager))]
[RequireComponent(typeof(CharacterBuffManager))]
public class Character : MonoBehaviour, IDamageable
{

    //当前位置
    protected  GridPosition gridPosition;

    protected Animator animator;
    protected SpriteRenderer characterSprite;

    [SerializeField] protected bool isActionable;
    [SerializeField] protected bool isFaceRight;
    [SerializeField] protected Character targetCharacter;

    [SerializeField] private CharacterStatus status;
    private CharacterSkillManager skillManager;
    private CharacterBuffManager buffManager;


    public CharacterStatus Status => status;
    public CharacterSkillManager SkillManager => skillManager;
    public CharacterBuffManager BuffManager => buffManager;

    protected bool isTest;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        skillManager = GetComponent<CharacterSkillManager>();
        buffManager = GetComponent<CharacterBuffManager>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnTurnStart.ToString(), OnTurnStart);
        EventCenter.Instance.AddEventListener<E_Camp_Type>(E_Event_Type.OnTurnEnd.ToString(), OnTurnEnd);
        EventCenter.Instance.AddEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.AddEventListener<E_Game_Result>(E_Event_Type.OnGameOver.ToString(), OnGameOver);
    }

    protected virtual void Start()
    {
        gridPosition = GridSystem.Instance.GetGridPosition(transform.position);
        GridSystem.Instance.SetCharacterOnGridPosition(gridPosition, this);
        EventCenter.Instance.EventTrigger<Character>(E_Event_Type.OnCharacterCreated.ToString(), this);
        InitStatus();
    }

    protected virtual void OnDisable()
    {
        EventCenter.Instance.RemoveEventListener<E_Camp_Type>(E_Event_Type.OnTurnStart.ToString(), OnTurnStart);
        EventCenter.Instance.RemoveEventListener<E_Camp_Type>(E_Event_Type.OnTurnEnd.ToString(), OnTurnEnd);
        EventCenter.Instance.RemoveEventListener<Character>(E_Event_Type.OnSelectCharacter.ToString(), OnSelectCharacter);
        EventCenter.Instance.AddEventListener<E_Game_Result>(E_Event_Type.OnGameOver.ToString(), OnGameOver);
        EventCenter.Instance.EventTrigger<Character>(E_Event_Type.OnCharacterDestroy.ToString(), this);
    }


    // 初始化
    private void InitStatus()
    {
        Status.ac = Status.maxAc;
        Status.san = Status.maxSan;
        Status.TempAtk = 0;
        Status.TempAtkMul = 0;
    }

    // 改变动画
    public void ChangeAnimation(E_CharacterAnimation_Type type)
    {
        switch (type)
        {
            case E_CharacterAnimation_Type.Select:
                animator.SetBool(E_CharacterAnimation_Parameter.isSelected.ToString(), true);
                break;
            case E_CharacterAnimation_Type.DeSelect:
                animator.SetBool(E_CharacterAnimation_Parameter.isSelected.ToString(), false);
                break;
            case E_CharacterAnimation_Type.Active:
                animator.SetBool(E_CharacterAnimation_Parameter.isActiveFinal.ToString(), false);
                break;
            case E_CharacterAnimation_Type.ActiveFinal:
                animator.SetBool(E_CharacterAnimation_Parameter.isActiveFinal.ToString(), true);
                break;
            case E_CharacterAnimation_Type.Attack:
                animator.SetTrigger(type.ToString());
                break;
            case E_CharacterAnimation_Type.Injury:
                animator.SetTrigger(type.ToString());
                break;
            case E_CharacterAnimation_Type.Poison:
                animator.SetTrigger(type.ToString());
                break;
            case E_CharacterAnimation_Type.Attack2:
                animator.SetTrigger(type.ToString());
                break;
        }
    }

    // 更新位置
    public void UpdateGridPosition()
    {
        GridPosition newGridPosition = GridSystem.Instance.GetGridPosition(transform.position);
        if (gridPosition != newGridPosition)
        {
            GridSystem.Instance.MoveCharacterOnGridPosition(gridPosition, newGridPosition, this);
            gridPosition = newGridPosition;
        }
    }

    //被伤害
    public void Damage(Character maker, int value)
    {
        ChangeAnimation(E_CharacterAnimation_Type.Injury);
        int damage = value - Status.def;

        OnCharacterDamageArgs args = new OnCharacterDamageArgs(maker, this, damage);
        EventCenter.Instance.EventTrigger<OnCharacterDamageArgs>(E_Event_Type.OnCharacterDamage.ToString(), args);

        Status.hp -= args.value;
        WorldUISystem.Instance?.CreateTxt(transform.position, "-" + damage, Color.red);
        EventCenter.Instance.EventTrigger(E_Event_Type.OnChangedCharacter.ToString());

        // 尝试将攻击者视作目标
        if(maker != null && maker.GetCampType() != GetCampType())
        {
            targetCharacter = maker;
        }

        if (Status.hp <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        BuffManager.RemoveAllBuff();
        GridSystem.Instance.ClearCharacterOnGridPosition(gridPosition);
        EventCenter.Instance.EventTrigger<Character>(E_Event_Type.OnCharacterDestroy.ToString(), this);
        Destroy(gameObject);
    }

    //回复生命值
    public void RecoverHp(Character maker, int value)
    {
        WorldUISystem.Instance?.CreateTxt(transform.position, "+" + value, Color.green);
        Status.hp = Mathf.Min(Status.maxHp, Status.hp + value);

        EventCenter.Instance.EventTrigger(E_Event_Type.OnChangedCharacter.ToString());
    }

    public void RecoverSan(Character maker, int value)
    {
        WorldUISystem.Instance?.CreateTxt(transform.position, "+" + value, Color.blue);
        Status.san = Mathf.Min(Status.maxSan, Status.san + value);

        EventCenter.Instance.EventTrigger(E_Event_Type.OnChangedCharacter.ToString());
    }

    //改变面向
    public void UpdateFace(GridPosition targetGridPos)
    {
        if (targetGridPos.x > gridPosition.x)
        {
            isFaceRight = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (targetGridPos.x < gridPosition.x)
        {
            isFaceRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void UpdateFace(Vector2 targetPos)
    {
        float posX = transform.position.x;
        if (targetPos.x > posX)
        {
            isFaceRight = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (targetPos.x < posX)
        {
            isFaceRight = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    //装备武器

    public void EquipWeapons(WeaponData weapon)
    {
        WeaponData oldWeapon = ItemSystem.Instance.GetWeaponData(status.weaponId);
        if (oldWeapon == null)
        {
            oldWeapon = new WeaponData();
        }
        status.atk += weapon.AddAtk - oldWeapon.AddAtk;
        status.def += weapon.AddDef - oldWeapon.AddDef;
        status.maxHp += weapon.AddMaxHP - oldWeapon.AddMaxHP;
        status.maxSan += weapon.AddMaxSan - oldWeapon.AddMaxSan;
        status.hp = Mathf.Min(status.hp, status.maxHp);
        status.san = Mathf.Min(status.san, status.maxSan);

        status.weaponId = weapon.weaponId;
        status.weaponType = weapon.weaponType;
        status.imgName = weapon.imgName;
        SpriteAtlas sa = ResMgr.Instance.Load<SpriteAtlas>("Sprite/SpriteAtlas");
        characterSprite.sprite = sa.GetSprite(status.imgName);

        EventCenter.Instance.EventTrigger(E_Event_Type.OnChangedCharacter.ToString());
    }

    #region 公开属性
    //角色属性
    public E_Camp_Type GetCampType() => Status.campType;
    public Character GetTargetCharacter() => targetCharacter;
    public bool IsPlayerController() => Status.isPlayerController;

    // 技能系统
    public void DelorySkill(SkillData data, GridPosition targetPos, UnityAction unityAction) => SkillManager.DelorySkill(data, targetPos, unityAction);



    // 是否可以行动
    public bool IsActionable => isActionable;
    public void SetIsActionable(bool isActionable)
    {
        if (isActionable)
        {
            this.isActionable = true;
            ChangeAnimation(E_CharacterAnimation_Type.Active);
        }
        else
        {
            this.isActionable = false;
            ChangeAnimation(E_CharacterAnimation_Type.ActiveFinal);
        }
    }

    // 当前位置
    public GridPosition GetGridPosition() => gridPosition;
    public void SetGridPosition(GridPosition targetGridPos)
    {
        Vector2 worldPos = GridSystem.Instance.GetWorldPosition(targetGridPos);
        transform.position = worldPos;
        gridPosition = targetGridPos;
    }
    #endregion

    // AI自动行动
    public void AutoAction(UnityAction onComplete)
    {
        FindTargetCharacter();
        RandomDelorySkill(onComplete);
    }

    // 寻找目标 把最近的敌人当作目标
    public void FindTargetCharacter()
    {
        IGridSelector gridSelector = GridSelectorFactory.GetGridSelector(E_GridSelector_Type.VisibleEnemy);
        List<GridPosition> list = gridSelector.GetValidGridPositions(gridPosition, Status.viewDistance, GetCampType());
        int distance = 1000;
        if (targetCharacter != null)
        {
            distance = GridPosition.Distance(targetCharacter.gridPosition, gridPosition);
        }
        for (int i = 0; i < list.Count; i++)
        {
            int newDistance = GridPosition.Distance(list[i], gridPosition);
            if (distance > newDistance)
            {
                distance = newDistance;
                targetCharacter = GridSystem.Instance.GetCharacterOnGridPosition(list[i]);
            }
        }
    }

    // AI用随机释放技能
    public void RandomDelorySkill(UnityAction unityAction) => SkillManager.RandomDelorySkill(unityAction);

    // 初始化
    public void Setup(CharacterStatus status, List<int> skillSaveData)
    {
        this.status = status.Clone();
        SpriteAtlas sa = ResMgr.Instance.Load<SpriteAtlas>("Sprite/SpriteAtlas");
        characterSprite.sprite = sa.GetSprite(status.imgName);
        SkillManager.Setup(skillSaveData);
    }

    public PlayerCharacterData Compress()
    {
        PlayerCharacterData playerCharacterData = new PlayerCharacterData();
        playerCharacterData.status = Status.Clone() ;
        playerCharacterData.skillSaveData = new List<int>(SkillManager.SkillSaveData);
        return playerCharacterData;
    }

    // 事件
    private void OnTurnStart(E_Camp_Type currentTurn)
    {
        if (currentTurn == E_Camp_Type.Enemy)
        {
            SetIsActionable(true);
            Status.ac = Mathf.Min(Status.ac + 2, Status.maxAc);
        }
    }

    private void OnTurnEnd(E_Camp_Type currentTurn)
    {
    }

    private void OnSelectCharacter(Character character)
    {
        if (character == this)
        {
            ChangeAnimation(E_CharacterAnimation_Type.Select);
        }
        else
        {
            ChangeAnimation(E_CharacterAnimation_Type.DeSelect);
        }
    }


    private void OnGameOver(E_Game_Result arg)
    {
        buffManager.RemoveAllBuff();
    }

}
