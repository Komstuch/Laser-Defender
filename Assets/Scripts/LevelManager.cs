using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [SerializeField] float delayInSeconds = 2f;


    public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
        SceneManager.LoadScene(name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());

    }
    public IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Win Screen");
    }
}
