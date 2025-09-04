using UnityEngine;

public class FightingController : MonoBehaviour
{
    [SerializeField] private float _attackCoolDown = 0.5f;
    private int _normalAttackDamage = 1;
    private int _heavyAttackDamage = 2;
    private string[] attackAnimations = { "Punch", "Charge Punch" };
    private float _lastTimeAttack;

    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void PerfromAttack(int attackIndex)
    {
        if (Time.deltaTime - _lastTimeAttack > _attackCoolDown)
        {
            _animator.Play(attackAnimations[attackIndex]);

            int damage = _normalAttackDamage;

            _lastTimeAttack = Time.deltaTime;
        }
        else
        {

        }
    }
}
