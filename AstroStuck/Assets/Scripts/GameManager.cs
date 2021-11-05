using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    PlayerController _controller;
    public int _currentScene;
    public bool _isCorrect;

    private void Awake() {
        Instance = this;
        _currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void changeColour(Color32 _colour1, Color32 _colour2, Transform _object) {
        var rend = _object.GetComponent<Renderer>();
        if (_isCorrect) {
            rend.material.SetColor("_Color", _colour1);
        }
        else {
            rend.material.SetColor("_Color", _colour2);
        }
    }

    public void ChangeScene() {
        if (_isCorrect) SceneManager.LoadScene(_currentScene + 1); 
    }

}
