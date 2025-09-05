using System;
using UnityEngine;

public class BlockMovementEvent : MonoBehaviour
{
    public static event Action OnAttack;
    public static event Action OnAttackEnemy;

    public void UnblockMovement()
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
}
