using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    int _savedBuildIndex;
    [SerializeField] Dialog dialog;
    [SerializeField] PlayerSkills skills;
    [SerializeField] Transform confirmation;
    bool temp1, temp2;

    public Animator transition;
    bool fullscreen;

    public void StopMoving() {
        temp1 = skills._attach;
        temp2 = skills._rotateStones;
        Debug.Log(temp1);
        Debug.Log(temp2);
        dialog._canMove = false;
        skills._attach = false; skills._rotateStones = false;
    }
    public void StartMoving() {
        dialog._canMove = true;
        skills._attach = temp1; skills._rotateStones = temp2;
    }

    public void ContinueGame() {
        StartCoroutine(LoadLevel());
        if (!PlayerPrefs.HasKey("SavedInteger")) {
            NewGame();
        }
        else {
            SceneManager.LoadScene(PlayerPrefs.GetInt("SavedInteger"));
        }
    }

    public void NewGame() {
        PlayerPrefs.DeleteAll();
        StartCoroutine(LoadLevel());
        SceneManager.LoadScene(1);
    }

    public void ToggleMax() {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SaveGame() {
        PlayerPrefs.SetInt("SavedInteger", (SceneManager.GetActiveScene().buildIndex));
        StartCoroutine(SaveImage(confirmation));
    }

    public void Quit() {
        Application.Quit();
    }
    
    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
    }

    IEnumerator SaveImage(Transform confirmation) {
        confirmation.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        confirmation.gameObject.SetActive(false);
    }

}
