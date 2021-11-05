using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStones : MonoBehaviour
{
    [SerializeField] private Transform[] stones;
    public bool _isCorrect;

    public void Update() {
        GameManager.Instance._isCorrect = _isCorrect;
        CheckRotation();
    }

    void CheckRotation() {
        if (YRotation(stones[0]) != 180f && YRotation(stones[1]) == 180f && YRotation(stones[2]) != 180f && YRotation(stones[3]) != 180f) {
            _isCorrect = true;
        }
    }

    float YRotation(Transform _object) {
        //return UnityEditor.TransformUtils.GetInspectorRotation(_object).y;
        return _object.localRotation.eulerAngles.y;
    }

}
