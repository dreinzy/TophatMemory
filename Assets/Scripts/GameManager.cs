using UnityEngine;
using System.Collections.Generic;

public enum BallColor {Red, Green, Blue, Yellow };

public class GameManager : MonoBehaviour {

    public static GameManager Singleton;
    List<int> colorList;
    public int gridSize = 4;
    public int numberOfColors = 4;
    public GameObject ballPrefab;
    public Material[] ballMaterials;

    public Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow };
    public int[] timesUsed = new int[4];

    [HideInInspector]
    public GameObject selectedBall;

    private int[,] logicalGrid;
    private GameObject[,] coloredBall;

	// Use this for initialization
	void Start () {
        Singleton = this;
        coloredBall = new GameObject[gridSize, gridSize];
        logicalGrid = new int[gridSize, gridSize];
        selectedBall = null;
        colorList = new List<int>();
        SetupColorList();
        CreateBalls();

        
	}
	
    void SetupColorList() {
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                colorList.Add(j);
            }
        }
    }


	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) {
            CheckForMatch();
        }
	}

    void OnMouseDown() {
        ResetSelection();
    }

    void CreateBalls() {
        
        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                coloredBall[i, j] = Instantiate(ballPrefab, new Vector3(j, -i, 0), Quaternion.identity) as GameObject;
                coloredBall[i, j].GetComponent<ColoredBall>().SetRowColumn(i, j);

                BallColor randomBallColor = RandomBallColor();
                coloredBall[i, j].GetComponent<ColoredBall>().SetBallColor(randomBallColor);
            }
        }
        


        /*
        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                
                coloredBall[i, j] = Instantiate(ballPrefab, new Vector3(j, -i, 0), Quaternion.identity) as GameObject;
                coloredBall[i, j].GetComponent<ColoredBall>().SetRowColumn(i, j);

                coloredBall[i, j].GetComponent<ColoredBall>().SetBallColor((BallColor)grid[i,j]);
                /*
                BallColor randomBallColor = RandomBallColor();
                logicalGrid[i, j] = (int)randomBallColor;
                coloredBall[i, j].GetComponent<ColoredBall>().SetBallColor(randomBallColor);
                

            }
        }
        */

    }

    bool match(int[] types) {
        int firstType = types[0];

        for( int i = 1; i < types.Length; i++ )
            if (types[i] != firstType)
                return false;
        return true;
    }

    BallColor RandomBallColor() {
        int listIndex = Random.Range(0, colorList.Count - 1);
        int color = colorList[listIndex];
        colorList.RemoveAt(listIndex);
        return (BallColor)color;
    }

    void SwapBalls(int r1, int c1, int r2, int c2) {
        // Check if they are close
        if(ValidMovement(r1, c1, r2, c2)) {

            Vector3 firstPosition = coloredBall[r1, c1].transform.position;
            coloredBall[r1, c1].transform.position = coloredBall[r2, c2].transform.position;
            coloredBall[r2, c2].transform.position = firstPosition;

            int rowFirst = coloredBall[r1, c1].GetComponent<ColoredBall>().row;
            int columnFirst = coloredBall[r1, c1].GetComponent<ColoredBall>().column;
            coloredBall[r1, c1].GetComponent<ColoredBall>().SetRowColumn(coloredBall[r2, c2].GetComponent<ColoredBall>().row, coloredBall[r2, c2].GetComponent<ColoredBall>().column);
            coloredBall[r2, c2].GetComponent<ColoredBall>().SetRowColumn(rowFirst, columnFirst);

            GameObject tempBall = coloredBall[r1, c1];
            coloredBall[r1, c1] = coloredBall[r2, c2];
            coloredBall[r2, c2] = tempBall;

            CheckForMatch();
        }
    }

    bool ValidMovement(int r1, int c1, int r2, int c2) {
        if (r1 != r2 &&
            c1 != c2)
            return false;

        int distanceRow = Mathf.Abs(r1- r2);
        if (distanceRow > 1f)
            return false;

        int distanceColumn = Mathf.Abs(c1 - c2);
        if (distanceColumn > 1f)
            return false;

        return true;
    }

    public void SelectBall(GameObject whichBall) {
        if(selectedBall == null) {
            selectedBall = whichBall;
        }
        else {
            SwapBalls(selectedBall.GetComponent<ColoredBall>().row, selectedBall.GetComponent<ColoredBall>().column,
                      whichBall.GetComponent<ColoredBall>().row, whichBall.GetComponent<ColoredBall>().column);
            ResetSelection();
        }
    }

    void ResetSelection() {
        selectedBall = null;
        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                coloredBall[i, j].GetComponent<ColoredBall>().ResetColor(); 
            }
        }
    }
    void CheckForMatch() {
        CheckHorizontal();
        CheckVertical();
    }

    void CheckHorizontal() {
        BallColor horizontalColor = BallColor.Red;
        bool match = true;

        for (int i = 0; i < coloredBall.GetLength(0); i++) {
            for (int j = 0; j < coloredBall.GetLength(1); j++) {
                if (j == 0) {
                    horizontalColor = coloredBall[i, j].GetComponent<ColoredBall>().ballColor;
                    match = true;
                }
                if (coloredBall[i, j].GetComponent<ColoredBall>().ballColor != horizontalColor) {
                    match = false;
                    break;
                }
            }
            if (match) {
                DestroyLine(i, true);
            }
        }
    }

    void CheckVertical() {
        BallColor verticalColor = BallColor.Red;
        bool match = true;

        for (int i = 0; i < coloredBall.GetLength(0); i++) {
            for (int j = 0; j < coloredBall.GetLength(1); j++) {
                if (j == 0) {
                    verticalColor = coloredBall[j, i].GetComponent<ColoredBall>().ballColor;
                    match = true;
                }
                if (coloredBall[j, i].GetComponent<ColoredBall>().ballColor != verticalColor
                    || coloredBall[j, i].GetComponent<ColoredBall>().destroyed ) {
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
        for (int i = 0; i < coloredBall.GetLength(0); i++) {
            if (horizontal) {
                //coloredBall[number, i].GetComponent<MeshRenderer>().enabled = false;
                coloredBall[number, i].GetComponent<ColoredBall>().hatMesh.GetComponent<MeshRenderer>().enabled = false;
                coloredBall[number, i].GetComponent<ColoredBall>().ShowSmoke();
                coloredBall[number, i].GetComponent<ColoredBall>().destroyed = true;
                CheckGameOver();
            }
            else {
                //coloredBall[i, number].GetComponent<MeshRenderer>().enabled = false;
                coloredBall[i, number].GetComponent<ColoredBall>().hatMesh.GetComponent<MeshRenderer>().enabled = false;
                coloredBall[i, number].GetComponent<ColoredBall>().ShowSmoke();
                coloredBall[i, number].GetComponent<ColoredBall>().destroyed = true;
                CheckGameOver();
            }
        }
    }

    public void DropAllHats() {
        for (int i = 0; i < coloredBall.GetLength(0); i++) {
            for (int j = 0; j < coloredBall.GetLength(1); j++) {
                coloredBall[i, j].GetComponent<ColoredBall>().DropHat();
            }
        }
    }

    public void CheckGameOver() {
        bool over = true;
        for (int i = 0; i < coloredBall.GetLength(0); i++) {
            for (int j = 0; j < coloredBall.GetLength(1); j++) {
                if (coloredBall[i, j].GetComponent<ColoredBall>().destroyed == false)
                    over = false;
            }
        }
        if(over) {
            Debug.Log("Game Over");
            GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>().gameOver = true;
        }
    }
}
