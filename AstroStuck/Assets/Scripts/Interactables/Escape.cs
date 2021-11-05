using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{

    [SerializeField] private GameObject _portalParticle;
    [SerializeField] private AudioSource _portalOpened;
    [SerializeField] private bool _hasPlayed;

    private void Start() {
        _portalParticle.SetActive(false);
    }

    private void Update() {
        OnComplete();
    }

    private void OnComplete() {
        Color32 open = new Color(0.8f, 0.2f, 0.4f, .5f);
        Color32 closed = new Color(1f, 1f, 1f, .5f);
        GameManager.Instance.changeColour(open, closed, transform);

        if (GameManager.Instance._isCorrect) {
            _portalParticle.SetActive(true);
            if (!_hasPlayed) {
                _portalOpened.Play();
                _hasPlayed = true;
            }
        } else {
            _portalParticle.SetActive(false);
            _hasPlayed = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && GameManager.Instance._isCorrect) {
            GameManager.Instance.ChangeScene();
            Debug.Log("Level Completed");

        }

    }

}
