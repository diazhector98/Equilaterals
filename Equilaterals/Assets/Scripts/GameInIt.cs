using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GameInIt : MonoBehaviour {

	public static bool gameStarter;

	public bool canStartGame = false;
	public GameObject cam;

	public GameObject hearts;
	public GameObject crownRangingLogo;
	public GameObject mainLogo;
	public GameObject speechBuble;

	public GameObject background;

	public GameObject menuBottomText;
	private bool isScoreShowing = true;

	private string leaderBoard1ID = "Equilaterals_HighScore_1ID";
		// Use this for initialization
	void Awake () {
		DataManagement.datamanager.LoadData ();
		Application.targetFrameRate = 60;
		P_Collide.health = 3;
		P_Collide.tokens = 0;
		Time.timeScale = 1.0f;
		gameStarter = false;
	
	}

	void Start(){
		Social.localUser.Authenticate (ProcessAuthentication);
		menuBottomText.GetComponent<Text> ().text = "High Score: " +  DataManagement.datamanager.highScore;
		speechBuble.SetActive(false);
		hearts.SetActive(false);
		crownRangingLogo.SetActive(true);
		mainLogo.SetActive(true);

	
	}
	// Update is called once per frame
	void Update () {

	




		if (canStartGame == true && (Input.touchCount > 0 || Input.GetButton ("Horizontal"))) {
			gameStarter = true;

		
		}

		if (gameStarter == true) {
		
			speechBuble.SetActive (false);
			cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (0, 0, -10), 2 * Time.deltaTime);
			cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 5, 5 * Time.deltaTime);
		
		}
		//IOS

		if (Input.touchCount > 0) {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2 (worldPos.x, worldPos.y);

			if (gameStarter = false || mainLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
			
				StartCoroutine (StartGame ());
			}
				
			if (gameStarter = false && crownRangingLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPos)) {
				//Load GameCenter
				Social.ReportScore((long) DataManagement.datamanager.highScore, leaderBoard1ID, HighScoreCheck);
				Social.ShowLeaderboardUI ();
			}
				


		
		}


		//PC

		if (Input.GetMouseButtonDown(0)) {


		
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePos = new Vector2 (worldPos.x, worldPos.y);

		
			if (gameStarter = false || mainLogo.GetComponent<Collider2D>() == Physics2D.OverlapPoint (mousePos)) {

				StartCoroutine (StartGame ());
			}
				
			if (gameStarter = false && crownRangingLogo.GetComponent<Collider2D> () == Physics2D.OverlapPoint (mousePos)) {
				//Load GameCenter
				Social.ReportScore((long) DataManagement.datamanager.highScore, leaderBoard1ID, HighScoreCheck);
				Social.ShowLeaderboardUI ();
			}



		}
	
	}

	//Gamecenter

	void ProcessAuthentication ( bool success ){
		if (success == true) {
			Debug.Log ("Authenticated checking achievements");
			Social.LoadAchievements (ProcessLoadedAchievements);
		} else {
			Debug.Log ("Failed authentication");

		
		}
	
	}

	void ProcessLoadedAchievements(IAchievement[] achievements){
		if (achievements.Length == 0) {
			Debug.Log ("Error: no achievements found");
		
		} else {
			Debug.Log ("Got " + achievements.Length + " achievements");
		}
	}

	public static void HighScoreCheck(bool results){
		if (results == true) {
			Debug.Log ("Score submission succesful");

		} else {
			Debug.Log ("Score submission failed");
		}
	
	}

	public void swapBottomMenuText(){
		if (isScoreShowing == true) {
			menuBottomText.GetComponent<Text> ().text = "Total Tokens Collected: " + DataManagement.datamanager.totalCollected;
			isScoreShowing = false;

			 
		} else {
			menuBottomText.GetComponent<Text> ().text = "High Score: " +  DataManagement.datamanager.highScore;
			isScoreShowing = true;
		}
	
	}
	IEnumerator StartGame(){

		if (DataManagement.datamanager.highScore < 20) {
			float t = 0;
			while (t < 1) {

				t += Time.deltaTime;
				cam.gameObject.transform.position = Vector3.Lerp (cam.gameObject.transform.position, new Vector3 (1.8f, -1.45f, -10), 0.1f);
				cam.GetComponent<Camera> ().orthographicSize = Mathf.Lerp (cam.GetComponent<Camera> ().orthographicSize, 2, 0.1f);
				speechBuble.SetActive (true);
				menuBottomText.SetActive (false);
				mainLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (0, 15), 0.1f);
				crownRangingLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (0, 15), 0.1f);
				yield return new WaitForSeconds (0.02f);
			}
		} else {
			float t = 0;
			while (t < 1) {
				t += Time.deltaTime;
				menuBottomText.SetActive (false);
				hearts.SetActive (true);
				canStartGame = true;
				mainLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (0, 15), 0.1f);
				crownRangingLogo.transform.position = Vector2.Lerp (mainLogo.transform.position, new Vector2 (0, 15), 0.1f);
				yield return new WaitForSeconds (0.02f);

			}
		}
		Destroy (mainLogo);
		Destroy (crownRangingLogo);
		hearts.SetActive (true);
		canStartGame = true;


	}
}