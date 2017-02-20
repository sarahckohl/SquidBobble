using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private HexGrid _currentBoard = new HexGrid();
    public  HexGrid currentBoard
    {
        get {
            return _currentBoard;
        }
    }



    void printCurrentBoard() {

        Debug.Log(_currentBoard);
    }

	// Use this for initialization
	void Start () {

        //this will be adapted into level selecting logic
        

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            printCurrentBoard();
        }
		
	}
}
