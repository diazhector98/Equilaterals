using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject player;
	public GameObject token;
	public GameObject bigToken;
	public GameObject cam;
	public GameObject[] trees;
	public GameObject bigTree;
	public GameObject[] debrees;

	//PowerUps
	public GameObject invincibilityToken;
	public GameObject heartToken;
	private float invincibilityTimer;
	private float heartTimer;

	public GameObject gapEmpty;
	private float treeTimer;
	private float tokenTimer ;
	private float bigTreeTimer;
	private float gapTreesTimer;
	private float bigTokenTimer ;
	public AudioClip bigTreeFalling;

	public AudioClip treePop;

	void Start(){
	
		bigTreeTimer = Random.Range (15.0f, 40.0f);
		treeTimer = Random.Range (2.0f, 5.0f);
		tokenTimer = Random.Range (0.5f, 1.0f);
		gapTreesTimer = Random.Range (25.0f, 75.0f); //25, 75
		bigTokenTimer = Random.Range (10.0f, 40.0f);
		invincibilityTimer = Random.Range (15.0f, 45.0f);
		heartTimer = Random.Range (20.0f, 70.0f);

	}
	void Update () {
		if (GameInIt.gameStarter == true) {
		
			treeTimer -= Time.deltaTime;
			tokenTimer -= Time.deltaTime;
			bigTreeTimer -= Time.deltaTime;
			gapTreesTimer -= Time.deltaTime;

			bigTokenTimer -= Time.deltaTime;
			heartTimer -= Time.deltaTime;

			invincibilityTimer -= Time.deltaTime;
			if (treeTimer < 0) {
				SpawnTree();
			}
			if (tokenTimer < 0) {
				SpawnToken();
			}

			if (invincibilityTimer < 0) {
				SpawnInvincibility();
			}

			if (heartTimer < 0) {
				SpawnHeart();
			}
			if (bigTokenTimer < 0) {
				SpawnBigToken();
			}

			if (bigTreeTimer < 0) {
				StartCoroutine (SpawnBigTree ());
			}

			if (gapTreesTimer < 0) {
				StartCoroutine (SpawnGapTrees ());
			}
		}

	}

	void SpawnToken(){
		GameObject tok = Instantiate (token, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity) as GameObject;
		tokenTimer = Random.Range (0.5f, 1.0f);
	}

	void SpawnBigToken(){
	
		Instantiate (bigToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity);
		bigTokenTimer = Random.Range (30.0f, 60.0f);
	
	}

	void SpawnInvincibility(){

		Instantiate (invincibilityToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity);
		invincibilityTimer = Random.Range (15.0f, 45.0f);
	
	}

	void SpawnHeart(){
	
		Instantiate (heartToken, new Vector2 (Random.Range (-6, 6), 6), Quaternion.identity);
		heartTimer = Random.Range (20.0f, 80.0f);

	
	}

	void SpawnTree(){
		GameObject tre = Instantiate (trees[Random.Range(0,trees.Length)], new Vector2 (Random.Range (-6, 6), 10), Quaternion.identity) as GameObject;

		if (P_Collide.tokens < 10) {
			treeTimer = Random.Range (2.0f, 5.0f);
		}
		if (P_Collide.tokens > 9 && P_Collide.tokens < 25) {
			treeTimer = Random.Range (0.5f, 2.5f);
		}

		if (P_Collide.tokens > 24 && P_Collide.tokens < 50 ) {
			treeTimer = Random.Range (0.3f, 2.0f);
		}
		if (P_Collide.tokens > 49 && P_Collide.tokens < 75) {
			treeTimer = Random.Range (0.1f, 1.5f);
		}
		if (P_Collide.tokens > 74 && P_Collide.tokens < 100) {
			treeTimer = Random.Range (0.1f, 1.0f);
		}

		if (P_Collide.tokens > 99 && P_Collide.tokens < 150) {
			treeTimer = Random.Range (0.05f, 0.5f);
		}

		if (P_Collide.tokens > 149 && P_Collide.tokens < 200) {
			treeTimer = Random.Range (0.05f, 0.3f);
		}
			

		tre.GetComponent<Rigidbody2D> ().gravityScale = Random.Range (1, 3);

	}

	IEnumerator SpawnBigTree(){
		bigTreeTimer = 1000;
		Vector2 spawnSpot;
		int debreeTimer = (Random.Range (20, 30));
		int spwnPercnt = Random.Range (0, 100);
		if (spwnPercnt < 50) {
			spawnSpot = new Vector2 (Random.Range (-6, 6), 20);
		
		} else {
			spawnSpot = new Vector2 (player.transform.position.x, 20);
		}

		while (debreeTimer > 0) {
			debreeTimer--;
			float t = Random.Range (0.005f, 0.1f);
			yield return new WaitForSeconds (t);
			GameObject debree = Instantiate (debrees [Random.Range (0, debrees.Length)], new Vector2 (spawnSpot.x + Random.Range (-1.0f, 1.0f), spawnSpot.y - 1f), Quaternion.identity) as GameObject;
		}

		iTween.ShakePosition (cam, new Vector3 (0.1f, 0.1f, 0.1f), 2);
		yield return new WaitForSeconds (1);
		Debug.Log ("Big Tree about to fall");
		Instantiate (bigTree, spawnSpot, Quaternion.identity);
		Debug.Log ("Big Tree has fallen");

		player.GetComponent<AudioSource>().PlayOneShot(bigTreeFalling, 0.5f);
		bigTreeTimer = Random.Range (15.0f, 40.0f);
	}

	IEnumerator SpawnGapTrees (){

		bigTreeTimer = 1000f;
		treeTimer = 1000f;
		tokenTimer = 1000f;
		gapTreesTimer = 1000f;
		int numTreeSets = Random.Range (1, 5);
		for (int i = 0; i < numTreeSets; i++) {

			yield return new WaitForSeconds (1);
			int xPos = -10;
			int GapXPos = Random.Range (-6, 6);

			GameObject gapTreeGroup = new GameObject ();

			gapTreeGroup.name = "Gap Tree Group";
			gapTreeGroup.AddComponent<AudioSource> ();
			gapTreeGroup.GetComponent<AudioSource> ().pitch = 0.5f;

			for( int v = 0; v < 20; v++){
				GameObject tre = Instantiate (trees[Random.Range(0,trees.Length)], new Vector2 (xPos, 5), Quaternion.identity) as GameObject;
				// add audio
				gapTreeGroup.GetComponent<AudioSource>().PlayOneShot(treePop, 0.5f);
				gapTreeGroup.GetComponent<AudioSource> ().pitch = gapTreeGroup.GetComponent<AudioSource> ().pitch + 0.1f;
				tre.GetComponent<Rigidbody2D>().gravityScale = 0;


				if (xPos != GapXPos) {
					xPos++;
				} 

				else {
					GameObject em = Instantiate (gapEmpty, new Vector2 (xPos + 1, 5), Quaternion.identity) as GameObject;
					xPos = xPos + 2;
					em.gameObject.transform.parent = gapTreeGroup.gameObject.transform;
				}

				tre.gameObject.transform.parent = gapTreeGroup.gameObject.transform;
				yield return new WaitForSeconds (0.03f);

			}

			/////---------------------------------------------			float randomTime = Random.Range (0.5, 3.0f);

			float randomTime = Random.Range (0.5f, 1.0f);
			iTween.ShakePosition (gapTreeGroup, new Vector2 (0.2f, 0.2f), randomTime);
			gapTreeGroup.GetComponent<AudioSource> ().pitch = 1.0f;
			yield return new WaitForSeconds (randomTime);
			foreach (Rigidbody2D rb in gapTreeGroup.GetComponentsInChildren<Rigidbody2D>(true)) {
				Destroy (rb.GetComponent<Rigidbody2D> ());
			}

			gapTreeGroup.AddComponent<Rigidbody2D> ();
			gapTreeGroup.GetComponent<Rigidbody2D> ().gravityScale = 10;
			gapTreeGroup.gameObject.layer = 9;
			gapTreeGroup.gameObject.tag = "log";
			gapTreeGroup.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezePositionX;
			gapTreeGroup.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			gapTreeGroup.AddComponent<AudioSource> ();

		}

		bigTreeTimer = Random.Range (15.0f, 45.0f);
		treeTimer = Random.Range (2.0f, 5.0f);
		tokenTimer = Random.Range (0.5f, 1.0f);
		gapTreesTimer = Random.Range (25.0f, 75.0f); // 25, 75


	}

}
