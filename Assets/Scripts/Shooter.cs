using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        	
	}

    void Awake() {

    }
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, 0, -1*Input.GetAxisRaw("Horizontal"));

        if (Input.GetButtonDown("Shoot"))
        {
            Bubble.Create(gameObject.transform, Bubble.BubbleColor.Red);
        }
		
	}
}
