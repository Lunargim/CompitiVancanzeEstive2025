using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public GameObject _player;
    [SerializeField] private float _speed;

    private bool _blockMovement = false;

    [Header("Fighting")]
    [SerializeField] private float _attackCoolDown;
    private int _normalAttackDamage = 1;
    private int _heavyAttackDamage = 2;
    public string[] attackAnimations = { "Punch", "Heavy Kick", "Block" };
    private float _lastTimeAttack = 0;
    public int _randomNumber;
    [SerializeField] public float attackRadius = 2f;

    [Header("Health")]
    public const int ENEMYMAXHEALTH = 10;
    public int enemyHealth;
    public HealthBar enemyHealthBar;

    [Header("Stamina")]
    public const int ENEMYMAXSTAMINA = 5;
    public int enemyStamina;
    public StaminaBar staminaBar;
    private float _timer = 0;
    private bool _isStunned = false;

    public Animator animator;
    public static int damageTypeEnemy;

    public static event Action OnEnemyBlocking;

    [SerializeField] public Collider blockingCollider;
    [SerializeField] public Collider punchCollider;
    [SerializeField] public Collider kickCollider;

    private void Awake()
    {
        enemyStamina = ENEMYMAXSTAMINA;
        enemyHealth = ENEMYMAXHEALTH;
        animator = GetComponent<Animator>();
        CreateRandomNumber();
    }

    private void Start()
    {
        staminaBar.GiveFullStamina(enemyStamina);
        enemyHealthBar.GiveFullHealth(enemyHealth);
    }

    private void Update()
    {
        this.gameObject.transform.LookAt(_player.transform);
        float distance = Vector3.Distance(this.transform.position, _player.transform.position);
        if (distance > 2)
        {
            if (!_blockMovement)
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            _lastTimeAttack += Time.deltaTime;
            animator.SetBool("Walk Forward Enemy", false);

            if (_lastTimeAttack > _attackCoolDown)
            {
                CreateRandomNumber();
                PerformAttack(_randomNumber);
                _lastTimeAttack = 0;
            }
        }
        RegenStamina();
        CheckStunned();

    }
    void MoveTowardsPlayer()
    {
        MovementController._radius -= _speed;
        transform.Translate(_speed * Vector3.forward);
        animator.SetBool("Walk Forward Enemy", true);
    }
    void CreateRandomNumber()
    {
        _randomNumber = UnityEngine.Random.Range(0, attackAnimations.Length);
        damageTypeEnemy = _randomNumber;
    }

    void PerformAttack(int attackIndex)
    {
        if (!_isStunned)
        {
            animator.SetBool("Walk Forward Enemy", false);
            animator.Play(attackAnimations[attackIndex]);
            int damage = 0;

            switch (attackIndex)
            {
                case 0:
                    damage = _normalAttackDamage;
                    break;
                case 1:
                    damage = _heavyAttackDamage;
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Punch" || other.tag == "Kick")
        {
            int takeDamage = 0;
            switch (FightingController.attackTypePlayer)
            {
                case 0:
                    takeDamage = _normalAttackDamage;
                    break;
                case 1:
                    takeDamage = _heavyAttackDamage;
                    break;
            }

           StartCoroutine(TakeDamage(takeDamage));
        }
 
    }

    void PerformBlock(int attackIndex)
    {
        animator.Play(attackAnimations[attackIndex]);
    }

    public void BlockMovement()
    {
        _blockMovement = true;
    }
    public void ResetBlockMovement()
    {
        _blockMovement = false;
    }

    public IEnumerator TakeDamage(int takeDamage)
    {

        yield return new WaitForSeconds(0.1f);

        //play hit sound;
        enemyHealth -= takeDamage;
        enemyHealthBar.SetHealth(enemyHealth);
        animator.Play("Take Damage");
        ResetBlockMovement();

        if (enemyHealth < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("enemy dead");
    }

    public void RegenStamina()
    {
        if (enemyStamina < ENEMYMAXSTAMINA)
        {
            if (_timer >= 3f)
            {
                enemyStamina++;
                staminaBar.SetStamina(enemyStamina);
                _timer = 0;
            }
            _timer += Time.deltaTime;
        }
    }

    public IEnumerator CheckStunned()
    {
        if (enemyStamina <= 0)
        {
            BlockMovement();
            //Play stunned animation
            _isStunned = true;
            yield return new WaitForSeconds(2);
            ResetBlockMovement();
            enemyStamina = 3;
            _isStunned = false;
        }
    }
    public void RemoveStamina()
    {
        enemyStamina--;
        staminaBar.SetStamina(enemyStamina);
    }



    public void OnEnable()
    {
        BlockMovementEvent.OnAttackEnemy += BlockMovement;
        BlockMovementEvent.OnEndEnemyAttack += ResetBlockMovement;
    }

    public void OnDisable()
    {
        BlockMovementEvent.OnAttackEnemy -= BlockMovement;
        BlockMovementEvent.OnEndEnemyAttack -= ResetBlockMovement;
    }


}
