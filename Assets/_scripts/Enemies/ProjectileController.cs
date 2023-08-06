using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public delegate void Deflect(bool deflected);
    public static event Deflect OnDeflect;

    private Rigidbody2D _rigidBody;

    private SpriteRenderer _projectileSpriteRenderer;


    [SerializeField] private float _speed = 5;

    private Vector2 _movementDirection = Vector2.left;

    private bool _deflected = false;
    private bool _parryPerformed = false;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;

    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _projectileSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void PlayerParry_OnParryActive(bool parryPerformed)
    {
        print(parryPerformed);
        _parryPerformed = parryPerformed;
    }

    void Start()
    {
        _rigidBody.AddForce(_movementDirection * _speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        //this.transform.Translate(_movementDirection * _speed * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Player") && _parryPerformed)
        {
            //_movementDirection = Vector2.zero;
            _projectileSpriteRenderer.color = Color.blue;

            _deflected = true;
            OnDeflect?.Invoke(_deflected);

            Destroy(gameObject, 3f);

        }
        else if (collision.gameObject.CompareTag("Player") && !_parryPerformed)
        {
            _deflected = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_deflected)
        {
            return;
        }
        else if(collision.gameObject.CompareTag("Enemy") && _deflected)
        {

            Destroy(gameObject);
            Destroy(collision.transform.parent.gameObject);

        }
    }
}
