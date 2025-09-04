using System;
using UnityEngine;

public class FightingController : MonoBehaviour
{
    [SerializeField] private float _attackCoolDown = 0.5f;
    [SerializeField] private float _heavyAttackChargeTime = 10f;
    private int _normalAttackDamage = 1;
    private int _heavyAttackDamage = 2;
    public string[] attackAnimations = {"Punch","Heavy Kick","Block"};
    private float _lastTimeAttack;

    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Update()
    {
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
        if (Time.time - _lastTimeAttack > _attackCoolDown)
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

            _lastTimeAttack = Time.deltaTime;
        }
        else
        {

        }
    }

    void PerformBlock(int attackIndex)
    {
        _animator.Play(attackAnimations[attackIndex]);
    }

}
