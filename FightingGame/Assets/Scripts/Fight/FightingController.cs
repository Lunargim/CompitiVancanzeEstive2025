using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FightingController : MonoBehaviour
{
    [SerializeField] private float _attackCoolDown = 0.5f;
    private int _normalAttackDamage = 1;
    private int _heavyAttackDamage = 2;
    public string[] attackAnimations = {"Punch","Heavy Kick","Block"};
    private float _lastTimeAttack;
    public static int attackTypePlayer;

    private Animator _animator;

    public static event Action OnPlayerTakeDamage;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Update()
    {
       _lastTimeAttack += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J))
        {
            PerformAttack(0);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            PerformAttack(1);    
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PerformBlock(2);
        }
    }
    void PerformAttack(int attackIndex)
    {
        attackTypePlayer = attackIndex;
        if (_lastTimeAttack > _attackCoolDown)
        {
            _animator.Play(attackAnimations[attackIndex]);
            int damage = 0;

            switch(attackIndex)
            {
               case 0:
                    damage = _normalAttackDamage;
                    break;
               case 1:
                    damage = _heavyAttackDamage;
                    break;
            }

            _lastTimeAttack = 0;
        }
        OnPlayerTakeDamage?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Punch" || other.tag == "Kick") //other.tag != "Player" && other.tag != "Boundaries" && other.tag != "Enemy"
        {
            int takeDamage = 0;
            switch (OpponentAI.damageTypeEnemy)
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
        _animator.Play(attackAnimations[attackIndex]);
    }

    public IEnumerator TakeDamage(int takeDamage)
    {
        yield return new WaitForSeconds(0.1f);

        //play hit sound;
        //
        _animator.Play("Take Damage");
        OnPlayerTakeDamage?.Invoke();
    }

}
