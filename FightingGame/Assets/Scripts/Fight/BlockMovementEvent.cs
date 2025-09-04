using System;
using UnityEngine;

public class BlockMovementEvent : MonoBehaviour
{
    private Animator _animator;

    public static event Action OnAttack;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void UnblockMovement()
    {
        OnAttack?.Invoke();
    }
}
