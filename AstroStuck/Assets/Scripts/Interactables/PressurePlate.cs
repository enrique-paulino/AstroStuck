using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressurePlate : MonoBehaviour
{

    public bool _isCorrect;
    [SerializeField] private int _num;
    [SerializeField] private int _numMax;
    [SerializeField] private TMP_Text _numMaxText;
    [SerializeField] private Renderer rend;


    private void Start() {
        rend = transform.GetComponent<Renderer>();
        _numMaxText.text = _numMax.ToString();
    }
    private void Update() {
        Color32 green = new Color(0f, 1f, 0f, 0.3f);
        Color32 red = new Color(1f, 0f, 0f, 0.3f);
        GameManager.Instance.changeColour(green, red, transform);

        GameManager.Instance._isCorrect = _isCorrect;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Object")) {
            _numMax -= 1;
            _numMaxText.text = _numMax.ToString();
            if (_numMax == 0) _isCorrect = true; 
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Object")) {
            _numMax += 1;
            _numMaxText.text = _numMax.ToString();
            if (_numMax != 0) _isCorrect = false;
        }
    }

    


}
