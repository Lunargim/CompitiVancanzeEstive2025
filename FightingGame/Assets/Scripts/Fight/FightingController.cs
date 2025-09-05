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
    private OpponentAI _opponentAI;
    public int attackType;

    private Animator _animator;

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
        attackType = attackIndex;
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

    }

    private void OnTriggerEnter(Collider other)
    {
        int takeDamage = 0;
        switch (_opponentAI._randomNumber)
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
        StartCoroutine(PlayDamageAnimator(takeDamage));
    }

    void PerformBlock(int attackIndex)
    {
        _animator.Play(attackAnimations[attackIndex]);
    }

    public IEnumerator PlayDamageAnimator(int takeDamage)
    {
        yield return new WaitForSeconds(0.5f);

        //play hit sound;
        //
        _animator.Play("Take Damage");
    }

}
