using UnityEngine;
using UnityEngine.Rendering;

public class FightingController : MonoBehaviour
{
    public Transform enemyPos;
    private float _horizontalMove;
    private float _verticalMove;
    private Vector3 _playerPosition;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        this.gameObject.transform.LookAt(enemyPos);

        MoveVertical();
        MoveHorizontal();
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
            if (_radius > 1.1) 
            {
                _radius -= _speed;
                transform.Translate(Vector3.forward * _speed * _horizontalMove);
                _animator.SetBool("Walk Forward", true);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if(_radius < 8)
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


}


