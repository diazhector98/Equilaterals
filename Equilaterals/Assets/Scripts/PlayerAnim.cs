using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {


	public static float t = 1.0f;
	// Update is called once per frame
	void Update () {
		Debug.Log ("Should animate");
		t += Time.deltaTime;
		StartCoroutine (AnimatePlayer ());
	}

	IEnumerator AnimatePlayer(){
		while (t > 1) {
			
			gameObject.transform.localScale = Vector2.Lerp (gameObject.transform.localScale, new Vector2 (0.3f, 0.3f), 1.0f);
			yield return new WaitForSeconds (1.0f);

			gameObject.transform.localScale = Vector2.Lerp (gameObject.transform.localScale, new Vector2 (0.35f, 0.35f), 1.0f);
			yield return new WaitForSeconds (1.0f);
			t = 0.0f;

		}
	
	
	}
}
