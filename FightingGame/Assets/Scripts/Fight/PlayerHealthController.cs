using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] public Collider punchCollider;
    [SerializeField] public Collider kickCollider;

    public static event Action OnPlayerTakeDamage;

    [Header("Health")]
    public const int PLAYERMAXHEALTH = 10;
    public int playerHealth;
    public HealthBar healthBar;

    public void Start()
    {
        playerHealth = PLAYERMAXHEALTH;
        healthBar.GiveFullHealth(playerHealth);
        _animator = GetComponent<Animator>();
    }

    public void StartTakingDamage(int damage)
    {
        StartCoroutine(TakeDamage(damage));
    }

    public IEnumerator TakeDamage(int takeDamage)
    {
        punchCollider.enabled = false;
        kickCollider.enabled = false;
        playerHealth -= takeDamage;
        healthBar.SetHealth(playerHealth);
        _animator.Play("Take Damage");
        OnPlayerTakeDamage?.Invoke();
        yield return new WaitForSeconds(0.7f);
        punchCollider.enabled = true;
        kickCollider.enabled = true;

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("player dead");
    }

    public void OnEnable()
    {
        FightingController.OnPlayerLosingHealth += StartTakingDamage;
    }
    public void OnDisable()
    {
        FightingController.OnPlayerLosingHealth -= StartTakingDamage;
    }
}
