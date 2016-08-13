using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public void StartGame(){
		SceneManager.LoadScene (0);
	}

	public void QuitGame(){
		Application.Quit ();
	}
}
