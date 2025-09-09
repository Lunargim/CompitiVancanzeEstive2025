using System;
using UnityEngine;

public class PlayerStaminaController : MonoBehaviour
{
    [Header("Stamina")]
    public const int PLAYERMAXSTAMINA = 5;
    public int playerStamina;
    public StaminaBar staminaBar;

    private float _timer=0;

    public static event Action OnStaminaZero;

    public void Start()
    {
        playerStamina = PLAYERMAXSTAMINA;
        staminaBar.GiveFullStamina(playerStamina);
    }

    public void Update()
    {
        RegenStamina();
        CheckStunned();
    }

    public void RegenStamina()
    {
        if (playerStamina < PLAYERMAXSTAMINA)
        {
            if (_timer >= 3f)
            {
                playerStamina++;
                staminaBar.SetStamina(playerStamina);
                _timer = 0;
            }
            _timer += Time.deltaTime;
        }
    }

    public void CheckStunned()
    {
        if(playerStamina <= 0)
        {
            OnStaminaZero?.Invoke();
        }

    }
    public void RemoveStamina()
    {
        Debug.Log("STAMINA");
        playerStamina--;
        staminaBar.SetStamina(playerStamina);
    }

}
