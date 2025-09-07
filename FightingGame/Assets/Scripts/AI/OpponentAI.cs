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

    public Animator animator;
    public static int damageTypeEnemy;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        CreateRandomNumber();
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
    }
    void MoveTowardsPlayer()
    {
        MovementController._radius -= _speed;
        transform.Translate(_speed * Vector3.forward);
        animator.SetBool("Walk Forward Enemy", true);
    }
    void CreateRandomNumber()
    {
        _randomNumber = Random.Range(0, attackAnimations.Length);
        damageTypeEnemy = _randomNumber;
    }

    void PerformAttack(int attackIndex)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Punch" || other.tag == "Kick")
        {
            Debug.Log("AAAAAAAAAAAA");
            int takeDamage = 0;
            switch (FightingController.attackTypePlayer)
            {
                case 1:
                    takeDamage = _normalAttackDamage;
                    break;
                case 2:
                    takeDamage = _heavyAttackDamage;
                    break;
                default:
                    takeDamage = 0;
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
        _blockMovement = !_blockMovement;
    }
    public void ResetBlockMovement()
    {
        _blockMovement = false;
    }

    public IEnumerator TakeDamage(int takeDamage)
    {
        yield return new WaitForSeconds(0.1f);

        //play hit sound;
        //
        animator.Play("Take Damage");
        ResetBlockMovement();
    }

    public void OnEnable()
    {
        BlockMovementEvent.OnAttackEnemy += BlockMovement;
    }

    public void OnDisable()
    {
        BlockMovementEvent.OnAttackEnemy -= BlockMovement;
    }


}
