using UnityEngine;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(-100)]

public class Player : MonoBehaviour
{

    #region Don't move - Start & Var
    private static readonly int FallAnimString = Animator.StringToHash("Fall");
    private static readonly int JumpAnimString = Animator.StringToHash("Jump");
    
    public float speed; // Velocidad de Movimiento
    public float acceleration; // Aceleracion
    public float deceleration;
    private Transform _playerTransform; // Componente posicion del jugador
    
    private PlayerInput _playerInput; // Componente Input
    private Vector2 _playerMove; // Valor extraido del Input Movimeinto

    private float _tempSpeed;

    private bool _playerJump; // Valor extraido del Input Salto
    public float jumpForce;
    private bool _onGround; 
    
    private Rigidbody2D _rigidbody2D;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;
    
    
    
    void Start()
    {
        _playerInput = this.gameObject.GetComponent<PlayerInput>(); //Conseguir el componente de NEW INPUT SYSTEM 
        _rigidbody2D  = this.gameObject.GetComponent<Rigidbody2D>(); //Conseguir el componente de RB (Fisica)
        
        _animator = this.gameObject.GetComponent<Animator>();
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        _capsuleCollider2D = this.gameObject.GetComponent<CapsuleCollider2D>();
    }
    #endregion
    
    void Update()
    {
        //Detectar Inputs (No errors)
        Move();
        Jump();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground ")) //Arreglar error de Nombre
        {
            _onGround = true;
        }
        else
        {
            Debug.LogWarning("Object, Doesn't have Ground TAG");
        }
    }

    #region  Don't Move - WarningHelper
    public bool GetSpeed()
    {
        return (speed != 0f);
    }

    public bool GetJumpSpeed()
    {
        return (jumpForce != 0f);
    }

    public Rigidbody2D GetRbComponent()
    {
        return _rigidbody2D;
    }

    public bool GetAcceleration()
    {
        return (acceleration != 0);
    }

    public bool GetDeceleration()
    {
        return (deceleration != 0);
    }
    #endregion

    #region Don't move - No Errors
    void Move()
    {
        //Detectar input con NEW INPUT SYSTEM
        _playerMove = _playerInput.actions["MOVE"].ReadValue<Vector2>();
       
        // Horizontal movement
        if (_playerMove.x != 0f)
        {
            _tempSpeed = _rigidbody2D.linearVelocity.x;
            _tempSpeed += (_playerMove.x *  acceleration * Time.deltaTime);
            _tempSpeed = Mathf.Clamp(_tempSpeed, -speed, speed);
            
            _rigidbody2D.linearVelocity = new Vector2(_tempSpeed, _rigidbody2D.linearVelocity.y);
        }
        else
        {
            _tempSpeed = _rigidbody2D.linearVelocity.x;
            
            if (_tempSpeed > 0.2f)
            {
                _tempSpeed -= (deceleration  * Time.deltaTime);
            }
            else if (_tempSpeed < -0.2f)
            {
                _tempSpeed += (deceleration * Time.deltaTime);
            }
            else
            {
                _tempSpeed = 0f;
            }
            _rigidbody2D.linearVelocity = new Vector2(_tempSpeed, _rigidbody2D.linearVelocity.y);
        }

        if (_playerMove.y < 0f)
        {
            _capsuleCollider2D.size = new Vector2(0.8f, 1.2f);
        }
        else
        {
            _capsuleCollider2D.size = new Vector2(0.8f, 1.5f);
        }

        if (_playerMove.x < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        
        SetSpeedX(_playerMove.x);
        SetSpeedY(_playerMove.y);
        
        //Debug.Log(_playerMove);
    }

    void Jump()
    {
        _animator.SetBool(FallAnimString, !_onGround);
        if (!_onGround) //Si no se puede saltar, dejar de ejecutar (Salir)
        {
            return;
        }
            
        _playerJump = _playerInput.actions["JUMP"].ReadValue<float>() > 0;
        _animator.SetBool(JumpAnimString, _playerJump);

        if (_playerJump)
        {
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, jumpForce);
            _onGround = false;
        }
        
        
        //Debug.Log(_playerJump);
    }
    #endregion

    #region Don't move - Animator

    private void SetSpeedX(float value)
    {
        _animator.SetFloat("BlendX", value);
    }

    private void SetSpeedY(float value)
    {
        _animator.SetFloat("BlendY", value);
    }

    #endregion
}
