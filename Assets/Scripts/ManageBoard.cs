using UnityEngine;
using System.Collections;

public class ManageBoard : MonoBehaviour {

    public enum BallColor { Red, Green, Blue, Empty};
    public int numberOfColors;
    public BallColor[,] grid;
    public GameObject[] balls;
    public Material[] ColorMaterials;

	// Use this for initialization
	void Start () {
        numberOfColors = 3;
        grid = new BallColor[4, 4];
        GenerateRandomGrid();
        SetupBallColors();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            GenerateRandomGrid();
            SetupBallColors();
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            CheckForMatch();
        }
    }

    void SetupBallColors() {
        int ballCount = 0;
        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                balls[ballCount].GetComponent<MeshRenderer>().material = ColorMaterials[(int)grid[i, j]];
                ballCount++;
            }
        }
    }

    void GenerateRandomGrid() {
        for(int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                grid[i, j] = (BallColor)Random.Range(0, numberOfColors);
            }
        }
    }

    void CheckForMatch() {
        CheckHorizontal();
        CheckVertical();
    }

    void CheckHorizontal() {
        BallColor horizontalColor = BallColor.Empty;
        bool match = true;

        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                if (j == 0) {
                    horizontalColor = grid[i, j];
                    match = true;
                }
                if (grid[i, j] != horizontalColor) {
                    match = false;
                    break;
                }
            }
            if(match) {
                DestroyLine(i, true);
            }
        }
    }

    void CheckVertical() {
        BallColor verticalColor = BallColor.Empty;
        bool match = true;

        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++) {
                if (j == 0) {
                    verticalColor = grid[j, i];
                    match = true;
                }
                if (grid[j, i] != verticalColor) {
                    match = false;
                    break;
                }
            }
            if (match) {
                DestroyLine(i, false);
            }
        }
    }

    // bool horizontal determines if it should destroy a vertical or a horizontal line
    void DestroyLine(int number, bool horizontal) {
        for (int i = 0; i < grid.GetLength(0); i++) {
            if(horizontal) {
                grid[number, i] = BallColor.Empty;
                balls[number * 4 + i].GetComponent<MeshRenderer>().enabled = false;
            }
            else {
                grid[i, number] = BallColor.Empty;
                balls[i * 4 + number].GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
