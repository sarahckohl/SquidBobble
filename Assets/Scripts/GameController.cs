using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static HexGrid _currentBoard = null;
    public  static HexGrid currentBoard
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
        _currentBoard = (HexGrid)ScriptableObject.CreateInstance("HexGrid");
        

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
            printCurrentBoard();
        }
		
	}
}
