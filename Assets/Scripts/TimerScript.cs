using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	public float timer;
	public float score;
	Text label;
    public Text gameOverText;
	public bool gameOver;
	// Use this for initialization
	void Start () {
		label = this.GetComponentInParent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		try {
			if (Time.timeSinceLevelLoad >=0 && !gameOver) {
			timer = Time.timeSinceLevelLoad;	
			}
			else if (gameOver) {
				score = 1.0f / timer * 2500;
				label.text += "\n" + score;
				label.alignment = TextAnchor.MiddleCenter;
                gameOverText.text = "Game Over!\nScore: " + score;
                gameOverText.enabled = true;

			}

		if (label != null) {
			label.text = timer.ToString ().Substring(0,4);	
		}
		} catch (System.Exception ex) {
			//Don't care, stay out of my console!
			print(ex.Message);
		}
	}
}
