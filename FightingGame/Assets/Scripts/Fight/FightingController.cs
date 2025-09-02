using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class FightingController : MonoBehaviour
{

    [SerializeField] public Transform enemyPos;
    [SerializeField] public float radius = 1f;
    [SerializeField] public float speed = 2f;
    private bool _moveClockWise = true;
    private bool _moveTowards = true;
    private float _angle = 0f;
    private float _direction = -1f;
    private float _maxRadius;

    public void Start()
    {
        _maxRadius = radius;
    }

    public void Update()
    {
        this.gameObject.transform.LookAt(enemyPos);

        if (Input.GetKey(KeyCode.W))
        {
            _moveClockWise = true;
            ExecuteRotationMovement();

        }else if (Input.GetKey(KeyCode.S))
        {
            _moveClockWise = false;
            ExecuteRotationMovement();
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (this.gameObject.transform.position.x >= 0f)
            {
                _moveTowards = true;
            }
            else
            {
                _moveTowards = false;
            }
            ExecuteHorizontalMovement();
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (this.gameObject.transform.position.x <= 0f)
            {
                _moveTowards = true;
            }
            else
            {
                _moveTowards = false;
            }
            ExecuteHorizontalMovement();
        }
    }


    void ExecuteRotationMovement()
    {
        _direction = _moveClockWise ? -1f : 1f;
        _angle += Time.deltaTime * _direction * speed;
        float x= enemyPos.position.x + Mathf.Cos(_angle) * radius;
        float z = enemyPos.position.z + Mathf.Sin(_angle) * radius;

        this.transform.position = new Vector3(x, 0f, z);


        //float horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(-verticalInput, 0f, horizontalInput);

        /*if (Input.GetKey(KeyCode.W))
        {
            _playerX = enemyPos.position.x + Mathf.Cos(angle) * radius;
            _playerZ = enemyPos.position.z + Mathf.Sin(angle) * radius;

            _angle += speed * Time.deltaTime;

            transform.position = new Vector3(_playerX, 0, _playerZ);

            if(angle>= 360)
            {
                angle = 180;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            _playerX = enemyPos.position.x - Mathf.Cos(angle) * radius;
            _playerZ = enemyPos.position.z - Mathf.Sin(angle) * radius;

            angle += speed * Time.deltaTime;

            transform.position = new Vector3(_playerX, 0, _playerZ);

            if (angle <= 0)
            {
                angle = 180;
            }
        }*/

    }

    public void ExecuteHorizontalMovement()
    {
        radius  = Mathf.Clamp(radius, 2f, _maxRadius-1);
        float radiusValue = _moveTowards ? -0.2f : 0.2f;
        radius += radiusValue;

        float x = this.transform.position.x + Mathf.Cos(_angle);

        this.transform.position = new Vector3(x, 0f, 0f);
        /*_direction = _moveTowards ? 1f : -1f;
        float horizontal = Input.GetAxis("Horizontal");

        this.transform.position = new Vector3 (this.transform.position.x + horizontal * _direction * speed, 0f, 0f);
        */
    }

}
