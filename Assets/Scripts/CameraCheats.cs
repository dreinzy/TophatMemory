using UnityEngine;
using System.Collections;

public class CameraCheats : MonoBehaviour {

    public Transform cheatPosition;
    bool cheat = false;
    Vector3 startPos;
    Quaternion startRot;

	// Use this for initialization
	void Start () {
        startPos = this.transform.position;
        startRot = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (!cheat) {
                cheat = true;
                this.transform.position = cheatPosition.position;
                this.transform.rotation = cheatPosition.rotation;
            }
            else {
                cheat = false;
                this.transform.position = startPos;
                this.transform.rotation = startRot;
            }
        }
	}
}
