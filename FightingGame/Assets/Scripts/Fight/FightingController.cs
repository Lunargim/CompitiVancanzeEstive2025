using System;
using System.Collections;
using UnityEngine;
using static SoundManager;

public class FightingController : MonoBehaviour
{
    [Header("Fighting")]
    [SerializeField] private float _attackCoolDown = 0.5f;
    private int _normalAttackDamage = 1;
    private int _heavyAttackDamage = 2;
    public string[] attackAnimations = {"Punch","Heavy Kick","Block"};
    private float _lastTimeAttack;
    public static int attackTypePlayer;

    private Animator _animator;
    private bool _isBlocking;
    private int _punchesBlocked = 0;
    private bool _isStunned = false;

    public static event Action OnPlayerTakeDamage;
    public static event Action<int> OnPlayerLosingHealth;
    public static event Action OnBlockBroken;

    public PlayerStaminaController playerStaminaController;

    [SerializeField] public Collider blockingCollider;
    [SerializeField] public Collider punchCollider;
    [SerializeField] public Collider kickCollider;
    [SerializeField] public Collider bodyCollider;


    public void Start()
    {
        _isBlocking = false;
        _animator = GetComponent<Animator>();
        blockingCollider.enabled = false;
        punchCollider.enabled = false;
        kickCollider.enabled = false;
        bodyCollider.enabled = true;

    }
    public void Update()
    {
       _lastTimeAttack += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(PerformAttack(0));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(PerformAttack(1));    
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(PerformBlock(2));
        }
    }
    public IEnumerator PerformAttack(int attackIndex)
    {
        if (!_isStunned)
        {
            attackTypePlayer = attackIndex;

            if (_lastTimeAttack > _attackCoolDown)
            {
                _animator.Play(attackAnimations[attackIndex]);

                if (attackIndex == 0)
                {
                    SoundManager.PlaySound(SoundType.PUNCH, 1f);
                }
                if (attackIndex == 1)
                {
                    SoundManager.PlaySound(SoundType.KICK, 1f);
                }

                int damage = 0;

                switch (attackIndex)
                {
                    case 0:
                        damage = _normalAttackDamage;
                        punchCollider.enabled = true;
                        break;
                    case 1:
                        damage = _heavyAttackDamage;
                        kickCollider.enabled = true;
                        break;
                }

                _lastTimeAttack = 0;
            }
            OnPlayerTakeDamage?.Invoke();
            yield return new WaitForSeconds(0.5f);
        }  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Punch" || other.tag == "Kick")
        {
            if (_isBlocking && OpponentAI.damageTypeEnemy == 0)
            {
                _punchesBlocked++;
                SoundManager.PlaySound(SoundType.HITTING, 1f);

                if (_punchesBlocked == 2)
                {
                    OnBlockBroken?.Invoke();
                    _punchesBlocked = 0;
                }

            }else
            if(_isBlocking && OpponentAI.damageTypeEnemy == 1)
            {
                StartStun();
                playerStaminaController.RemoveStamina();
            }
            else
            {
                int takeDamage = 0;
                switch (OpponentAI.damageTypeEnemy)
                {
                    case 0:
                        takeDamage = _normalAttackDamage;
                        break;
                    case 1:
                        takeDamage = _heavyAttackDamage;
                        break;
                }

                OnPlayerLosingHealth?.Invoke(takeDamage);
            }
        }
    }
    public IEnumerator PerformBlock(int attackIndex)
    {
        bodyCollider.enabled = false;
        blockingCollider.enabled = true;
        _isBlocking = true;
        _animator.Play(attackAnimations[attackIndex]);
        SoundManager.PlaySound(SoundType.BLOCK, 1f);
        yield return new WaitForSeconds(1.25f);
        bodyCollider.enabled = true;
        blockingCollider.enabled = false;
        _isBlocking = false;
    }

    public void StartStun()
    {
        StartCoroutine(GetStunned());
    }

    public IEnumerator GetStunned()
    {
        _isStunned = true;
        _animator.Play("Stunned");
        SoundManager.PlaySound(SoundType.STUNNED, 1f);
        yield return new WaitForSeconds(2f);
        _isStunned = false;
    }

    public void OnEnable()
    {
        PlayerStaminaController.OnStaminaZero += StartStun;
        OpponentAI.OnEnemyBlockBroken += StartStun;
    }

    public void OnDisable()
    {
        PlayerStaminaController.OnStaminaZero -= StartStun;
        OpponentAI.OnEnemyBlockBroken -= StartStun;
    }

}
