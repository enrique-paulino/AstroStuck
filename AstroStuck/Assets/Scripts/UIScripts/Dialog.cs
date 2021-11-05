using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private GameObject _bar;
    [SerializeField] private float _typingSpeed;
    [SerializeField] private Transform _dialogCanvas;
    public GameObject _continueButton;
    public GameObject _settingsButton;
    public bool _canMove;
    public string[] _sentences;
    private int _index;
    

    

    void Start() {
        if (_sentences.Length == 0) {
            _canMove = true;
            _dialogCanvas.gameObject.SetActive(false);            
        }
        else {
            _canMove = false;
            _settingsButton.gameObject.SetActive(false);
            StartCoroutine(Type());
        }
    }

    void Update() {
        if (_sentences.Length != 0) { 
            if (_textDisplay.text == _sentences[_index]) {
                _continueButton.SetActive(true);
            }
        }
    }

    IEnumerator Type() {
        foreach(char letter in _sentences[_index].ToCharArray()) {
            _textDisplay.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }
    }

    public void NextSentence() {
        _continueButton.SetActive(false);
        if (_index < _sentences.Length - 1) {
            _index++;
            _textDisplay.text = "";
            StartCoroutine(Type());
        }
        else {
            _textDisplay.text = "";
            _bar.SetActive(false); _continueButton.SetActive(false);
            _settingsButton.gameObject.SetActive(true);
            _canMove = true;
        }
    }

}
