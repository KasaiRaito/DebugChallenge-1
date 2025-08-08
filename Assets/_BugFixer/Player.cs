using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool useRb;
    
    public float speed; // Velocidad de Movimiento
    private Transform _playerTransform; // Componente posicion del jugador
    
    private PlayerInput _playerInput; // Componente Input
    private Vector2 _playerMove; // Valor extraido del Input Movimeinto
    private Vector3 _playerDelta; // Input Movimiento Escalado a la Velocidad establecida

    private bool _playerJump; // Valor extraido del Input Salto
    public float jumpForce;
    private bool _onGround = false; 
    
    public float gravityScale;
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    { 
        _playerInput = this.gameObject.GetComponent<PlayerInput>(); //Conseguir el componente de NEW INPUT SYSTEM 

        if (useRb)
        {
            _rigidbody2D  = this.gameObject.GetComponent<Rigidbody2D>(); //Conseguir el componente de RB (Fisica)
        }
        else
        {
            _playerTransform = this.gameObject.GetComponent<Transform>(); //Conseguir el componente de posicion de un objeto.
        }
    }
    
    void FixedUpdate()
    { 
        //Calcular Fisicas
        CalculateGravity();
        
        //Detectar Inputs
        Move(); 
        Jump();
        
        
        if (useRb)
        {
            _rigidbody2D.AddForce(_playerDelta, ForceMode2D.Impulse);
        }
        else
        {
            _playerTransform.position += _playerDelta;
        }
        
        Debug.Log(_playerDelta);
    }

    void Move()
    {
        //Detectar input con NEW INPUT SYSTEM
        _playerMove = _playerInput.actions["MOVE"].ReadValue<Vector2>();
        
        _playerDelta.x = (_playerMove.x * (speed * Time.deltaTime));
        
        //Debug.Log(_playerDelta);
    }

    void Jump()
    {
        if(!_onGround) //Si no se puede saltar, dejar de ejecutar (Salir)
            return;
            
        _playerJump = _playerInput.actions["JUMP"].ReadValue<float>() > 0;

        if (_playerJump)
        {
            _playerDelta.y += jumpForce;
            _onGround = false;
        }
        
        //Debug.Log(_playerJump);
    }

    void CalculateGravity()
    {
        if (!_onGround)
        {
            _playerDelta.y -= (gravityScale * Time.deltaTime);
            _playerDelta.y = Mathf.Min(_playerDelta.y, -gravityScale); // Limitar velocidad de caida
            //Debug.LogWarning("Falling");
        }
        else
        {
            _playerDelta.y = 0;
            //Debug.Log("OnGround");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _onGround = true;
        }
    }
}
