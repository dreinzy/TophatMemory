using UnityEngine;
using System.Collections;

public class ColoredBall : MonoBehaviour
{

    public int row, column;
    public float colorChangeFactor = 0.5f;
    public bool destroyed;
    public BallColor ballColor;
    public GameObject hatMesh;
    public GameObject hat;
    public GameObject smoke;
    [HideInInspector]
    public bool clicked = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    Color normalColor;

    void Awake()
    {
        hat = this.transform.Find("tophat").gameObject;
        hatMesh = hat.transform.Find("SketchUp").gameObject;
        hatMesh = hatMesh.transform.Find("group_0").gameObject;
    }
    // Use this for initialization
    void Start()
    {
        destroyed = false;
        
    }
	
    // Update is called once per frame
    void Update()
    {
	    
    }

    void OnMouseDown()
    {
        clicked = true;
        hatMesh.GetComponent<MeshRenderer>().material.color = new Color(normalColor.r + colorChangeFactor, normalColor.g + colorChangeFactor, normalColor.b + colorChangeFactor);
        GameManager.Singleton.SelectBall(this.gameObject);

        //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        //offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    /*
    void OnMouseDrag() {
        if(clicked) {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    */

    public void SetRowColumn(int r, int c)
    {
        row = r;
        column = c;
    }

    public void SetBallColor(BallColor ballColor)
    {
        this.ballColor = ballColor;
        this.GetComponent<MeshRenderer>().material = GameManager.Singleton.ballMaterials[(int)ballColor];
        normalColor = hatMesh.GetComponent<MeshRenderer>().material.color;
    }

    public void ResetColor()
    {
        hatMesh.GetComponent<MeshRenderer>().material.color = normalColor;
    }

    public void DropHat()
    {
        hat.GetComponent<Animator>().SetTrigger("DropHat");
    }

    public void ShowSmoke()
    {
        Instantiate(smoke, this.transform.position, Quaternion.identity);
    }
}
