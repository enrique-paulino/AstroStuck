using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    private Image _fuelBar;
    public float _currentFuel;
    private float _maxFuel = 4;
    PlayerController Player;

    private void Start() {
        _fuelBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerController>();
    }

    void Update() {
        _currentFuel = Player._currentFuel;
        _maxFuel = Player._maxFuel;
        _fuelBar.fillAmount = _currentFuel / _maxFuel;
    }

}
