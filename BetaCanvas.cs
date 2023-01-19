using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetaCanvas : MonoBehaviour{
    
    [SerializeField] Animator introUIAnimator;

    void Start() {

        StartCoroutine(StartScene());
    }

    public void GoToGame(){

        StartCoroutine(StartGame());
    }

    public void PlayAgain(){
        SceneManager.LoadScene("Main");
    }

    public void Exit(){
        Application.Quit();
    }

    public void ItchioPage(string enlace){
        Application.OpenURL(enlace);
    }

    public void ShowTransition(){

        introUIAnimator.SetTrigger("Show");
    }

    public void HideTransition(){

        introUIAnimator.SetTrigger("Hide");
    }

    IEnumerator StartGame(){

        yield return new WaitForSeconds(0.05f);
        PlayAgain();
    }

    IEnumerator StartScene(){

        yield return new WaitForSeconds(0.5f);
        introUIAnimator.SetTrigger("Show");
    }
}