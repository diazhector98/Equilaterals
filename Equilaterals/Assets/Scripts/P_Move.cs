using UnityEngine;
using System.Collections;

public class P_Move : MonoBehaviour {

	private int xPos;
	private float time = 0.1f;

	public bool isTouch;
	// Update is called once per frame
	void Update () {

		if (isTouch == true) {

			//IOS

			if (Input.touchCount > 0 && GameInIt.gameStarter == true) {


				if (Input.GetTouch (0).position.x > Screen.width / 2 && time > 0.075f && xPos < 8) {
					xPos++;
					time = 0.0f;
				}

				if (Input.GetTouch (0).position.x < Screen.width / 2 && time > 0.075f && xPos > -8) {
					xPos--;
					time = 0.0f;
				}

				time += Time.deltaTime;


			}
		}
		else {
			//Keyboard

			if(Input.GetButton("Horizontal") && GameInIt.gameStarter == true){
				if(Input.GetAxis("Horizontal") > 0 && time > 0.075f && xPos < 8){
					xPos++;
					time = 0.0f;
				}

				if(Input.GetAxis("Horizontal") < 0 && time > 0.075f && xPos > -8){
					xPos--;
					time = 0.0f;
				}

				time += Time.deltaTime;
			}

		
		}
			
	
		MovePlayer ();
	
	}

	void MovePlayer(){
		Vector2 playerPos = gameObject.transform.position;
		playerPos.x = xPos;
		gameObject.transform.position = Vector2.Lerp (gameObject.transform.position, playerPos, 10 * Time.deltaTime);
	}
}
