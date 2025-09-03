using UnityEngine;

public class FightingController : MonoBehaviour
{
    public Transform enemyPos;
    private float _horizontalMove;
    private float _verticalMove;
    private Vector3 _playerPosition;
    private Vector3 _playerPositionX;
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;

    private float _maxRadius;

    private bool _moveTowards = true;

    private float x;


    public void Start()
    {
        _maxRadius = _radius;
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
        }
        else if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = _playerPosition;
            transform.RotateAround(enemyPos.position, Vector3.down, -_verticalMove);
        }
    }

    private void MoveHorizontal()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * _speed * _horizontalMove);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * _speed * -_horizontalMove);
        }


    }


    /*     void ExecuteRotationMovement()
    {
        _direction = _moveClockWise ? -1f : 1f;
        _angle += Time.deltaTime * _direction * speed;
        float x = enemyPos.position.x + Mathf.Cos(_angle) * radius;
        float z = enemyPos.position.z + Mathf.Sin(_angle) * radius;

        this.transform.position = new Vector3(x, 0f, z);

    }

    public void ExecuteHorizontalMovement()
    {
        _radius = Mathf.Clamp(radius, 2f, _maxRadius - 1);
        float radiusValue = _moveTowards ? -0.2f : 0.2f;
        radius += radiusValue;

        x = this.transform.position.x + Mathf.Cos(_angle) * radius;

        this.transform.position = new Vector3(x, 0f, 0f);
        /*_direction = _moveTowards ? 1f : -1f;
        float horizontal = Input.GetAxis("Horizontal");

        this.transform.position = new Vector3 (this.transform.position.x + horizontal * _direction * speed, 0f, 0f);
        */


}


