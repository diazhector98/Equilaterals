using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class P_Collide : MonoBehaviour {

	public static int tokens;
	public GameObject cam;

	public GameObject token;
	public GameObject tokenUI;


	public GameObject canvasOverlay;

	public GameObject hearts;
	public static int health = 3;
	public AudioClip collectToken;
	public AudioClip collectBigToken;
	public AudioClip heartAudio;



	public AudioClip pain;

	private bool canTakeDamage = true;
	private bool isInvincible = false;

	public GameObject background;
	//public AudioClip collectToken;
	// Update is called once per frame
	public string[] hurtLog;

	void Update() {

	
	
	}

	void OnTriggerEnter2D(Collider2D trig){

		if (trig.gameObject.tag == "token") {
			GetComponent<AudioSource>().PlayOneShot(collectToken, 0.5f);
			tokens++;
			DataManagement.datamanager.totalCollected++;
			tokenUI.GetComponent<Text>().text = (tokens.ToString());
			Destroy (trig.gameObject);
					//get token
		}

		if (trig.gameObject.tag == "bigToken") {
			GetComponent<AudioSource>().PlayOneShot(collectBigToken, 0.5f);
			tokens = tokens + 10;
			Destroy (trig.gameObject);
		
		}

		if (trig.gameObject.tag == "log") {
			getHurt();
		}

		if (trig.gameObject.tag == "bigLog") {
			getBigHurt ();
		}

		if (trig.gameObject.tag == "heart") {
			gainHeart ();
			GetComponent<AudioSource>().PlayOneShot(heartAudio, 0.5f);
			Destroy (trig.gameObject);

		}
		if (trig.gameObject.tag == "gap") {
			StartCoroutine (SpawnExplosionTokens());
		}

		if (trig.gameObject.tag == "invincibility") {
			Destroy (trig.gameObject);
			StartCoroutine (invincibility());

		}





	}

	void getHurt(){

		if (canTakeDamage == true) {
			iTween.ShakePosition (cam, new Vector3 (0.2f, 0.2f, 0.2f), 1);
			GetComponent<AudioSource> ().PlayOneShot (pain, 0.5f);
			health--;
			if (health <= 0) {
				StartCoroutine (GameOver ());
			} else {
				StartCoroutine (cantGetHurt ());

			}

		}
	}

	void 
	getBigHurt(){
		if (canTakeDamage == true) {
			iTween.ShakePosition (cam, new Vector3 (0.4f, 0.4f, 0.4f), 1);
			GetComponent<AudioSource> ().PlayOneShot (pain, 0.5f);
			//For polish make different audio
			health = health - 3;
			StartCoroutine (GameOver ());

		}
	}

	void gainHeart(){
		if (health < 3) {
			health++;
		}
	
	}

	public void ReloadScene(){
		Time.timeScale = 1.00f;
		SceneManager.LoadScene ("main");
	}

	IEnumerator SpawnExplosionTokens(){
		yield return new WaitForSeconds (0.3f);
		int tokenExplosion = Random.Range(5,10);
		for (int i = 0; i < tokenExplosion; i++) {

			GameObject t = Instantiate (token, new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y + 1.5f), Quaternion.identity) as GameObject;
			t.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Random.Range (5.0f, 10.0f), ForceMode2D.Impulse);
			int r = Random.Range (0, 2);
			if (r > 0) {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * Random.Range (3.0f, 6.0f), ForceMode2D.Impulse);

			} else {
				t.GetComponent<Rigidbody2D> ().AddForce (Vector2.left * Random.Range (3.0f, 6.0f), ForceMode2D.Impulse);

			}

		}

		yield return new WaitForSeconds (1);
	}

	IEnumerator invincibility(){
		canTakeDamage = false;
		isInvincible = true;
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 0.3f);
		gameObject.GetComponent<AudioSource> ().pitch = 2.0f;
		yield return new WaitForSeconds (12);

		for (int i = 0; i < 10; i++) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 1.0f);
			yield return new WaitForSeconds (0.25f);
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 0.3f);
			yield return new WaitForSeconds (0.25f);
			 
		}
		gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
		canTakeDamage = true;
		isInvincible = false;
	}

	IEnumerator cantGetHurt(){
		canTakeDamage = false;
		for (int i = 0; i < 10; i++) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 1.0f);
			yield return new WaitForSeconds (0.10f);
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 0.5f);
			yield return new WaitForSeconds (0.10f);

		}
		if (isInvincible == false) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0.0f, 0.0f, 0.0f, 1.0f);
			gameObject.GetComponent<AudioSource> ().pitch = 1.0f;
			canTakeDamage = true;
		}
	}
	IEnumerator GameOver(){
		if (tokens > DataManagement.datamanager.highScore) {
			DataManagement.datamanager.highScore = tokens;
		}
		DataManagement.datamanager.SaveData ();
		Time.timeScale = 0.25f;
		canvasOverlay.SetActive (true);
		hearts.SetActive (false);
		gameObject.GetComponent<P_Move> ().enabled = false;
		gameObject.GetComponent<Collider2D> ().enabled = false;
		yield return new WaitForSeconds (1);

	}
}
