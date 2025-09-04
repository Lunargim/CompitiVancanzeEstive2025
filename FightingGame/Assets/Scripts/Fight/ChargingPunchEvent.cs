using UnityEngine;

public class ChargingPunchEvent : MonoBehaviour
{
    [SerializeField] public float heavyAttackChargeTime = 2f;
    private Animator _animator;
    private float _timer;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _timer = heavyAttackChargeTime;
    }
    public void OnChargingPunch()
    {
        while(_timer > 0)
        {
            Debug.Log("AAAAAAAAAAA");
            _animator.speed = 0;
            _timer -= Time.deltaTime;   
        }

        _animator.speed = 1;
    }
}
