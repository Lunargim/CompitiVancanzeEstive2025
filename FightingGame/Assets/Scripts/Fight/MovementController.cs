using UnityEngine;
using UnityEngine.Rendering;

public class MovementController : MonoBehaviour
{
    public Transform enemyPos;
    private float _horizontalMove;
    private float _verticalMove;
    private Vector3 _playerPosition;
    [SerializeField] public static float _radius = 12;
    [SerializeField] private float _speed;
    [SerializeField] private float _radiusMax = 12f;
    [SerializeField] private float _radiusMin = 1.1f;
    private Animator _animator;

    private bool _blockMovement = false;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        this.gameObject.transform.LookAt(enemyPos);

        if (!_blockMovement)
        {
            MoveVertical();
            MoveHorizontal();
        }
    }
    private void MoveVertical()
    {
        _playerPosition = _radius * Vector3.Normalize(this.transform.position - enemyPos.position) + enemyPos.position;
        _verticalMove = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = _playerPosition;
            transform.RotateAround(enemyPos.position, Vector3.up, _verticalMove);
            _animator.SetBool("Walk Side", true);
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = _playerPosition;
            transform.RotateAround(enemyPos.position, Vector3.down, -_verticalMove);
            _animator.SetBool("Walk Side", true);
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            _animator.SetBool("Walk Side", false);
        }
    }

    private void MoveHorizontal()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.D))
        {
            if (_radius > _radiusMin) 
            {
                _radius -= _speed;
                transform.Translate(Vector3.forward * _speed * _horizontalMove);
                _animator.SetBool("Walk Forward", true);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if(_radius < _radiusMax)
            {
                _radius += _speed;
                transform.Translate(Vector3.back * _speed * -_horizontalMove);
                _animator.SetBool("Walk Backward", true);
            }
        }

        if(!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            _animator.SetBool("Walk Forward", false);
            _animator.SetBool("Walk Backward", false);
        }
    }

    public void BlockMovement()
    {
        _blockMovement = !_blockMovement;
    }

    public void OnEnable()
    {
        BlockMovementEvent.OnAttack += BlockMovement;
    }

    public void OnDisable()
    {
        BlockMovementEvent.OnAttack -= BlockMovement;
    }

}


