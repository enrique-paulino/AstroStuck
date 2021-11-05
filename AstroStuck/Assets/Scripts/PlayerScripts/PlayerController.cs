using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] public Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] public Quaternion _direction;
    public Vector3 _input;
    public bool _canMove;
    public Dialog dialog;
    public Transform _minimap;

    [Header("Jetpack")]
    public float _maxFuel = 4;
    public float _currentFuel;
    [SerializeField] private float _thrustForce = 0.5f;
    [SerializeField] private Transform _grounded;
    public bool _isGrounded;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private AudioSource _jetpackNoise;
    [SerializeField] private AudioSource _playerWalk;
    public Transform _maxHeight;
    public bool _jetpackOff;

    [Header("Animations")]
    [SerializeField] private Animator anim;

    void Update() {
        _canMove = dialog._canMove;

        Look();
        GetInput();
        Animations();
        Audio();
    }

    void FixedUpdate() {
        if (_canMove) {
            if (GameManager.Instance._currentScene != 5) {
                Move();
                Jetpack();
            }
            else {
                Move(); Jetpack();
                _currentFuel = 0;

                _minimap.gameObject.SetActive(true);
                
            }
        }
        
    }

    void GetInput() {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look() {
        if(_input != Vector3.zero) {

            var relative = (transform.position + _input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            _direction = rot;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
    }

    void Move() {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude).normalized * _speed * Time.deltaTime);
    }

    void Jetpack() {
        _isGrounded = Physics.CheckSphere(_grounded.transform.position, 0.3f, LayerMask.GetMask("Ground"));
        //_isGrounded = Physics.Raycast(_grounded.transform.position, Vector3.down, 0.05f, LayerMask.GetMask("Ground"));
        if (Input.GetButton("Jump") && _currentFuel > 0f && transform.position.y <= 15) {
            _maxHeight.gameObject.SetActive(false);
            _currentFuel -= Time.deltaTime;
            _rb.AddForce(_rb.transform.up * _thrustForce, ForceMode.Impulse);
            effect.Play(); 
            if (!_jetpackNoise.isPlaying) _jetpackNoise.Play();
        }
        else if (_isGrounded && _currentFuel < _maxFuel) {
            _maxHeight.gameObject.SetActive(false);
            _currentFuel += Time.deltaTime;
            effect.Stop(); _jetpackNoise.Stop();
        }
        else if (transform.position.y >= 15) _maxHeight.gameObject.SetActive(true);
        else {
            effect.Stop(); _jetpackNoise.Stop();
            _maxHeight.gameObject.SetActive(false);
        }
        
    }

    void Animations() {
        if (_input != Vector3.zero) {
            anim.SetBool("isRunning", true);
        } 
        else {
            anim.SetBool("isRunning", false); 
        }

        if (_isGrounded) {
            anim.SetBool("isGrounded", true);
        } else {
            anim.SetBool("isGrounded", false);
        }
    }

    void Audio() {
        if (_input != Vector3.zero && _isGrounded) {
            if (!_playerWalk.isPlaying) _playerWalk.Play();
        }
        else {
            _playerWalk.Stop();
        }
    }


}
