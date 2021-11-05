using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{

    [Header("Skills")]
    public bool _attach;
    public bool _rotateStones;

    [Header("General")]
    [SerializeField] private Vector2 _mousePosition;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private Transform _finalCube;

    

    [Header("Attach Feature")]
    [SerializeField] private Transform _objectToGrab;
    [SerializeField] private Transform _hand;
    [SerializeField] private float _maxDistance;
    [SerializeField] private bool _isConnected;
    private LineRenderer _line;
    private SpringJoint _spring;
    Vector3 pointB;

    [Header("Parkour Feature")]
    int index = 0;
    public Transform[] _blocks;
    [SerializeField] private AudioSource _dropNoise;

    void Start() {
        _line = GetComponent<LineRenderer>();
        _spring = GetComponent<SpringJoint>();

        foreach (Transform block in _blocks) {
            block.gameObject.SetActive(false);
        }
        _blocks[0].gameObject.SetActive(true);

    }

    void Update() {
        GetMousePosition();
        if (_attach) PlayerAttach();
        if (_rotateStones) RotateStonePuzzle();
        if (index == 5) GameManager.Instance._isCorrect = true;
    }

    void GetMousePosition() {
        _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    void PlayerAttach() {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Grab"))) {
                if (Input.GetButton("Fire1") && Vector3.Distance(transform.position, hit.point) < _maxDistance) {
                    _isConnected = true;
                    _objectToGrab = hit.transform;
                }
            } 
        
        if (_isConnected && Input.GetButton("Fire2")) {
            _objectToGrab.GetComponent<Rigidbody>().isKinematic = false;
            _spring.spring = 0;
            _spring.connectedBody = null; _objectToGrab = null;
            _line.enabled = false;
            _isConnected = false;
        }
        else if (_isConnected) {

            if (!_controller._isGrounded) _objectToGrab.GetComponent<Rigidbody>().isKinematic = true;
            else _objectToGrab.GetComponent<Rigidbody>().isKinematic = false;

            _spring.connectedBody = _objectToGrab.GetComponent<Rigidbody>();
            _spring.spring = 10;
            _line.enabled = true;

            Vector3 _playerMiddle = transform.position; _playerMiddle.y = transform.position.y + 1.75f;
            _line.SetPosition(0, _hand.position);
            _line.SetPosition(1, _objectToGrab.GetComponent<Renderer>().bounds.center);
        }

        else {
            _spring.spring = 0;
            _spring.connectedBody = null;
            _line.enabled = false;
            _isConnected = false;
        }
    }

    void RotateStonePuzzle() {

        if (GameManager.Instance._isCorrect) _rotateStones = false;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        _spring.spring = 0;

        if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Rotate"))) {

            if (Input.GetButtonDown("Fire1") && Vector3.Distance(transform.position, hit.point) < _maxDistance) {
                hit.transform.Rotate(0f, 180f, 0f);
            }
        }
    }

    void Parkour(Collision collision) {
        if (transform.position.y > collision.transform.position.y) {
            if (collision.transform == _blocks[index]) {
                Transform hit = collision.transform;
                index++;
                _dropNoise.Play();
                _blocks[index].gameObject.SetActive(true);
            }
        }
    }

    void Final(Collider collision) {
        if (collision.transform == _finalCube) GameManager.Instance._isCorrect = true;
    }

    void OnTriggerEnter(Collider other) {
        Final(other);
    }

    void OnCollisionEnter(Collision collision) {
        Parkour(collision);
        
    }


}
