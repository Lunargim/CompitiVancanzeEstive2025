using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class BlockMovementEvent : MonoBehaviour
{
    public static event Action OnAttack;
    public static event Action OnAttackEnemy;

    public static event Action OnEndAttack;
    public static event Action OnEndEnemyAttack;

    public void UnBlockMovement()
    {
        if(this.tag == "Player")
        {
            OnAttack?.Invoke();
        }
        else
        {
            OnAttackEnemy?.Invoke();
        }
    }

    public void ResetMovement()
    {
        if (this.tag == "Player")
        {
            OnEndAttack?.Invoke();
        }
        else
        {
            OnEndEnemyAttack?.Invoke();
        }
    }
}
