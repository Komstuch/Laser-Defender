using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

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
        StartCoroutine(WaitAndLoad("Win Screen", 2));

    }
    public IEnumerator WaitAndLoad(string name, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(name);
    }

    private void LoadStartScene()
    {
        StartCoroutine(WaitAndLoad("Start Menu", 3));
    }
}
