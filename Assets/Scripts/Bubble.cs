using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {


    public enum BubbleColor { Red, Orange, Yellow, Green, Blue, Violet, Black, White };
    public BubbleColor color {
        get;
        private set;
    }
    

    bool isSnapped = false;
    static float bubbleSpeed = 15f;
    //once bubble prefab exists, can use code below
    private static Object _prefab;
    //private bool snapped;
    public static Object prefab {
        get {

            return _prefab ?? initializePrefab();
        }
    }

    static Object initializePrefab() {

        _prefab = Resources.Load("Prefabs/Bubble");
        return _prefab;
    }


    //might want to modularize this into a "HexGrid" class
    private Vector2 calculateSnapCoordinates()
    {

        //float ypos = Mathf.Round(gameObject.transform.position.y);
        //float xpos;

        //if (ypos % 2 == 0)
        //    xpos = roundToHalf(gameObject.transform.position.x);
        //else
        //    xpos = Mathf.Round(gameObject.transform.position.x);

        //if (xpos < -1*GameController.StageWidth/2+1)
        //{
        //    xpos += 1f;
        //}
        //if (xpos > GameController.StageWidth/2)
        //    xpos -= 1f;


        //return new Vector2(xpos, ypos);

        float xcor;
        float ycor;
        HexGrid.getGridCoordinates(gameObject.transform.position, out xcor, out ycor);

        Debug.Log(xcor+", "+ycor);


        return new Vector2(xcor,ycor);

    }


    //for now I just want to test gridsnapping.  Eventually this would better be done by either removing the rigidbody and adding a collider,
    //or else deleting self and create a "static" bubble object (with a collider and no rigidbody) in it's place
    
    //instead of destroying this, let's just pool it
    private void deActivate() {
        //maybe grid snapping function should best be defined here, access grid parameters through public variables
        Vector2 snapped = calculateSnapCoordinates();

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().transform.position = snapped;
        gameObject.transform.position = snapped;
        isSnapped = true;
        snapToGrid();
        Debug.Log(GameController.currentBoard);
    }


    private void snapToGrid() {
      
    }

    public static Bubble Create(Transform transform, BubbleColor newcolor) {
     
        GameObject bubbleInstance = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        Bubble newBubble = bubbleInstance.GetComponent<Bubble>();

        //similar logic for screentap angle probably
        //float xspeed = bubbleSpeed * Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.z);
        //float yspeed = bubbleSpeed * -1f * Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.z);

        //newBubble.GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed, yspeed);


        newBubble.GetComponent<Rigidbody2D>().velocity = transform.right*bubbleSpeed;
        newBubble.color = newcolor;

        return newBubble;
    }

    // Use this for initialization
    void Start (){


    }

    void Awake() {
 
    }


    private bool outOfBounds() {

        //maybe later use collider
        if (gameObject.transform.position.x - HexGrid.tileRadius < -1 * HexGrid.StageRadius)
        {
            return true;
        }
        else if(gameObject.transform.position.x + HexGrid.tileRadius > HexGrid.StageRadius){
            return true;
        }
        else {
            return false;
        }

    }

    // Update is called once per frame


    void FixedUpdate() {


        if (outOfBounds())
        {
            Vector2 newVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            newVelocity.x *= -1;
            gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity;
        }

    }

    void Update () {

        //this will be substituted with collision and likely moved to FixedUpdate
        if (gameObject.transform.position.y > 8 - HexGrid.tileRadius && !isSnapped)
        {
            deActivate();
        }

        //this will be substituted with collision and likely moved to FixedUpdate
        if (Input.GetKeyDown(KeyCode.S))
            {
            deActivate();
        }
		
	}

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Triggered ;_;");
        deActivate();
    }

}
